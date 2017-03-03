/**
 * Created by wangd on 2016/2/25.
 */

var util = require('../../util');
var dbo = require('../../../dbo');
var errorcode = require('../../errorcode');
var Router = require('koa-router');
var router = new Router();
var urltable = {};

//消费记录
urltable['/list'] = {header:["token"],post:[]};
router.post('/list',function(ctx){
    return new Promise(function(resolve,reject){
        ctx.req.body.uid = ctx.user.uid;
        dbo.consume.list(ctx.req.body).then(
            function(row){
                resolve(ctx.body = util.successPackage({count:0,rows:row}));
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

