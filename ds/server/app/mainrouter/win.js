/**
 * Created by wangd on 2016/2/25.
 */

var util = require('../../util');
var dbo = require('../../../dbo');
var errorcode = require('../../errorcode');
var Router = require('koa-router');
var router = new Router();
var urltable = {};

//中奖记录
urltable['/list'] = {header:["token"],post:[]};
router.post('/list',function(ctx){
    return new Promise(function(resolve,reject){
        ctx.req.body.uid = ctx.user.uid;
        dbo.win.list(ctx.req.body).then(
            function(row){
                resolve(ctx.body = util.successPackage({count:0,rows:row}));
            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
})
//领奖
urltable['/update'] = {header:['token'],post:['wid','qq','area','name']};
router.post('/update',function(ctx){
    return new Promise(function(resolve,reject){
        ctx.req.body.pickuptime = new Date().getTime();
        ctx.req.body.status = 2;//状态2：已领奖
        dbo.win.update(ctx.req.body).then(
            function(res){
                if(!res) resolve(ctx.body = util.errPackage(300035, errorcode));//更新失败
                else resolve(ctx.body = util.successPackage("领奖成功，商品会发放到对应账户"));
            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
})

module .exports = {
    router:router,
    urltable:urltable,
};
