/**
 * Created by wangd on 2016/2/24.
 */
var util = require('../../util');
var dbo = require('../../../dbo');
var cache = require('../../../cache');
var errorcode = require('../../errorcode');
var Router = require('koa-router');
var router = new Router();
var urltable = {};

//获取single里的商品信息列表
urltable['/list'] = {header:[],post:[]};
router.post('/list',function(ctx){
    return new Promise(function(resolve,reject){
        cache.single.list(ctx.req.body).then(
            function(row){
                resolve(ctx.body = util.successPackage({count:0,rows:row}));
            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
})

//获取single里的商品详情
urltable['/info'] = {header:[],post:[]};
router.post('/info',function(ctx){
    return new Promise(function(resolve,reject){
        dbo.single.info(ctx.req.body).then(
            function(res){
                if(!res){
                    resolve(ctx.body = util.errPackage(300031, errorcode));
                }else{
                    resolve(ctx.body = util.successPackage(res));
                }

            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
})

//商品往期中奖记录
urltable['/historywinlist'] = {header:[],post:['gid']};
router.post('/historywinlist',function(ctx){
    return new Promise(function(resolve,reject){
        dbo.single.historyWinList(ctx.req.body).then(
            function(row){
                resolve(ctx.body = util.successPackage({count:0,rows:row}));
            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
})

//参与众筹列表
urltable['/insolvelist'] = {header:[],post:['sid']};
router.post('/insolvelist',function(ctx){
    return new Promise(function(resolve,reject){
        dbo.single.insolveList(ctx.req.body).then(
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