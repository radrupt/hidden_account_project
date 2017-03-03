/**
 * Created by wangd on 2016/2/24.
 */
var util = require('../../util');
var dbo = require('../../../dbo');
var errorcode = require('../../errorcode');
var Router = require('koa-router');
var router = new Router();
var urltable = {};

//banner image,type:1:home banner
urltable['/list'] = {post:['type']};
router.post('/list',function(ctx){
    return new Promise(function(resolve,reject){
        dbo.banner.list(ctx.req.body).then(
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