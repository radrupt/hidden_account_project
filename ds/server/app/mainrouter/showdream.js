/**
 * Created by wangd on 2016/2/25.
 */
var util = require('../../util');
var dbo = require('../../../dbo');
var errorcode = require('../../errorcode');
var Router = require('koa-router');
var router = new Router();
var urltable = {};

//所有晒图列表
urltable['/listall'] = {post:[]};
router.post('/listall',function(ctx){
    return new Promise(function(resolve,reject){
        delete ctx.req.body.uid;
        dbo.showdream.list(ctx.req.body).then(
            function(row){
                resolve(ctx.body = util.successPackage({count:0,rows:row}));
            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
})

//个人晒图列表
urltable['/list'] = {header:['token'],post:[]};
router.post('/list',function(ctx){
    return new Promise(function(resolve,reject){
        ctx.req.body.uid = ctx.user.uid;
        dbo.showdream.list(ctx.req.body).then(
            function(row){
                resolve(ctx.body = util.successPackage({count:0,rows:row}));
            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
})

//晒图添加
urltable['/add'] = {header:['token'],post:['title','content','imagelist']};
router.post('/add',function(ctx){
    return new Promise(function(resolve,reject){
        ctx.req.body.uid = ctx.user.uid;
        dbo.showdream.insert(ctx.req.body).then(
            function(res){
                if(!res) resolve(ctx.body = util.errPackage(300030, errorcode));//数据不合法
                else resolve(ctx.body = util.successPackage(res));
            },function(err){console.log(err)
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
})

//晒图更新
urltable['/update'] = {header:['token'],post:['sdid','title','content','imagelist']};
router.post('/update',function(ctx){
    return new Promise(function(resolve,reject){
        dbo.showdream.update(ctx.req.body).then(
            function(res){
                if(!res) resolve(ctx.body = util.errPackage(300028, errorcode));//更新失败
                else resolve(ctx.body = util.successPackage("更新成功"));
            },function(err){
                resolve(ctx.body = util.errPackage(300010, errorcode));
            }
        )
    })
})

//晒图删除
urltable['/update'] = {header:['token'],post:['sdid']};
router.post('/delete',function(ctx){
    return new Promise(function(resolve,reject){
        dbo.showdream.delete(ctx.req.body).then(
            function(res){
                if(!res) resolve(ctx.body = util.errPackage(300033, errorcode));//删除失败
                else resolve(ctx.body = util.successPackage("删除成功"));
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