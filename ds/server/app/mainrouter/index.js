/**
 * Created by wangd on 2016/2/15.
 */

var util = require('../../util');
var errorcode = require('../../errorcode');
var redis = require('../../../redis');
var Router = require('koa-router');
var router = new Router();
var urltable = {};

//权限验证
router.use('/',function (ctx,next){
    if (!(util.checkAuth(ctx.url, ctx.header, ctx.req.body, urltable))) {//路由表权限判断失败，缺少字段
        ctx.body = util.errPackage(300009, errorcode);
    } else {
        if (ctx.header['token']){//判断token是否有效
            return new Promise(function(resolve,reject){
                util.getVByKRedis(redis.token, ctx.header['token']).then(
                    function(res){
                        if(!res){
                            resolve(ctx.body = util.errPackage(300003, errorcode));
                        }else{
                            ctx.user = JSON.parse(res) || {};//当前使用token登陆的用户信息
                            resolve(next());
                        }

                    },function(){
                        resolve(ctx.body = util.errPackage(300003, errorcode));
                    }
                )
            })
        }else {
            return next();
        }
    }
})

router.use('/consigneeaddress',require('./consigneeaddress').router.routes());//收货地址
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./consigneeaddress').urltable,'/consigneeaddress'));

router.use('/showdream',require('./showdream').router.routes());//晒图路由
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./showdream').urltable,'/showdream'));

router.use('/consume',require('./consume').router.routes());//中奖信息
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./consume').urltable,'/consume'));

router.use('/shopcar',require('./shopcar').router.routes());//购物车路由
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./shopcar').urltable,'/shopcar'));

router.use('/single',require('./single').router.routes());//上架商品路由
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./single').urltable,'/single'));

router.use('/banner',require('./banner').router.routes());//banner路由
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./banner').urltable,'/banner'));

router.use('/order',require('./order').router.routes());//banner路由
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./order').urltable,'/order'));

router.use('/user',require('./user').router.routes());//个人信息相关服务
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./user').urltable,'/user'));

router.use('/util',require('./util').router.routes());//服务路由
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./util').urltable,'/util'));

router.use('/sms',require('./sms').router.routes());//短信服务
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./sms').urltable,'/sms'));

router.use('/win',require('./win').router.routes());//中奖信息
urltable = Object.assign(urltable,util.addObjectKeyPrefix(require('./win').urltable,'/win'));

urltable = Object.assign(urltable,util.addObjectKeyPrefix(urltable,'/app'));

module.exports = router;
