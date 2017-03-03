/**
 * Created by wangd on 2016/2/21.
 */
var util = require('../../util');
var dbo = require('../../../dbo');
var errorcode = require('../../errorcode');
var Router = require('koa-router');
var router = new Router();
var urltable = {};
//收货地址

//收货地址列表
urltable['/list'] = {header:["token"],post:[]};
router.post('/list', function (ctx) {
    return new Promise(function(resolve,reject){
        ctx.req.body.uid = ctx.user.uid;
        dbo.consigneeaddress.list(ctx.req.body).then(
            function(row){
                resolve(ctx.body = util.successPackage({count:0,rows:row}));
            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
});

//添加收货地址
urltable['/add'] = {header:["token"],post:['qq','area','name']};
router.post('/add', function (ctx) {
    return new Promise(function(resolve,reject){
        ctx.req.body.uid = ctx.user.uid;
        dbo.consigneeaddress.insert(ctx.req.body).then(
            function(res){
                if(!res) resolve(ctx.body = util.errPackage(300030, errorcode));//数据重复
                else resolve(ctx.body = util.successPackage(res));
            },function(err){
                if(err.code == 'ER_DUP_ENTRY') return resolve(ctx.body = util.errPackage(300030, errorcode));//数据重复
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
});

//更新收货地址
urltable['/update'] = {header:["token"],post:['caid','qq','area','name','isdefault']};
router.post('/update', function (ctx) {
    return new Promise(function(resolve,reject){
        dbo.consigneeaddress.update(ctx.req.body).then(
            function(res){
                if(!res){
                    resolve(ctx.body = util.errPackage(300028, errorcode));
                }else resolve(ctx.body = util.successPackage("更新成功"));
            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
});

//删除收货地址
urltable['/delete'] = {header:["token"],post:['caid']};
router.post('/delete', function (ctx) {
    return new Promise(function(resolve,reject){
        dbo.consigneeaddress.delete(ctx.req.body).then(
            function(res){
                if(!res){
                    resolve(ctx.body = util.errPackage(300033, errorcode));
                }else resolve(ctx.body = util.successPackage("删除成功"));
            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
});

module .exports = {
    router:router,
    urltable:urltable,
};