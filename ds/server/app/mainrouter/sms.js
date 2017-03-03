/**
 * Created by wangd on 2016/2/19.
 */
var util = require('../../util');
var dbo = require('../../../dbo');
var errorcode = require('../../errorcode');
var redis = require('../../../redis');
var sms = require('../../../lib/sms');
var Router = require('koa-router');
var router = new Router();
var config = require('../../../config');
var urltable = {};
//权限检查，同一用户1天只能使用10次sms服务(所有sms相关路由,这个条件只适用于已经注册的用户即有uid的，对于未注册用户，比如使用)

//获取手机注册验证码,mobile
urltable['/mobile/register/icode'] = {header:[],post:['mobile']};
router.post('/mobile/register/icode', function (ctx) {
    if( !util.checkMobile(ctx.req.body.mobile) ) {//mobile is error
        ctx.body = util.errPackage(300018,errorcode);
    }else{
        var promiseA =  new Promise(function(resolve,reject){
            dbo.user.find({mobile:ctx.req.body.mobile}).then(//判断该手机号是否注册过
                function (row) {
                    if (row[0]) {
                        return Promise.reject(util.errPackage(300019, errorcode));//手机号已经注册
                    } else {
                        return util.getVByKRedis(redis.code, ctx.req.body.mobile + 'count');
                    }
                }, function (err) {
                    return Promise.reject(util.checkErrFormat(err)||util.errPackage(300010, errorcode));//数据库操作错误
                }
            ).then(//发送短信
                function (res) {
                    if (res && res >= 5) {//获取该手机号已经使用短信服务的次数
                        return Promise.reject(util.errPackage(300021, errorcode));
                    } else {
                        var mcode = util.genNumber(4);
                        return sms.sendMessage(ctx.req.body.mobile, mcode);
                    }
                }, function (err) {
                    return Promise.reject(util.checkErrFormat(err)||util.errPackage(300020, errorcode));
                }
            ).then(
                function (res) {
                    redis.code.set(ctx.req.body.mobile+config.code.register, res);
                    redis.code.expire(ctx.req.body.mobile+config.code.register, 30 * 60 * 1000);//30分钟有效期
                    redis.code.incr(ctx.req.body.mobile + 'count');//记录发送验证码次数
                    (function(mobile){setTimeout(function () {
                        redis.code.decr(mobile + 'count');
                    }, 24 * 60 * 60 * 1000)})(ctx.req.body.mobile);//每增加一条记录就在24小时候减去他
                    return Promise.resolve();
                }, function (err) {
                    return Promise.reject(util.checkErrFormat(err)||util.errPackage(300020, errorcode));
                }
            ).then(
                function(res){
                    resolve(ctx.body = util.successPackage('注册验证码发送成功'));
                },function(err){
                    resolve(ctx.body = util.checkErrFormat(err)||util.errPackage(300020, errorcode));
                }
            );
        });
        return promiseA;
    }
})
//mobile,icode
//验证手机验证码
urltable['/mobile/register/icode/check'] = {header:[],post:['mobile','icode']};
router.post('/mobile/register/icode/check',function(ctx){
    if( !util.checkMobile(ctx.req.body.mobile) ) {//mobile is error
        ctx.body = util.errPackage(300018,errorcode);
    }else{
        return new Promise(function(resolve,reject){
            util.getVByKRedis(redis.code,ctx.req.body.mobile+config.code.register).then(
                function(res){
                    if(res && res == ctx.req.body.icode) {
                        resolve(ctx.body = util.successPackage('短信验证码正确'));
                    }
                    else resolve(ctx.body = util.errPackage(300015, errorcode));
                },function(err){
                    resolve(ctx.body = util.checkErrFormat(err)||util.errPackage(300022, errorcode));
                }
            )
        })
    }
})
module .exports = {
    router:router,
    urltable:urltable,
};