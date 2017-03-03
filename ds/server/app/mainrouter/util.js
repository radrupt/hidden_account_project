/**
 * Created by wangd on 2016/2/25.
 */
var util = require('../../util');
var dbo = require('../../../dbo');
var errorcode = require('../../errorcode');
var qiniu = require('../../../lib/qiniu');
var config = require('../../../config');
var uuid = require('node-uuid');
var Router = require('koa-router');
var router = new Router();
var urltable = {};

//base64去头数据-data,文件格式-ext
urltable['/upload'] = {header:[],post:['data','ext']};
router.post('/upload',function(ctx){
    if(!ctx.user.uid){
        ctx.body = util.errPackage(300003, errorcode);//该token没有对用用户信息
    }else{
        return new Promise(function(resolve,rejecdt){
            var filename = util.md5(uuid.v1().toString())+'.'+ctx.req.body.ext;
            //util.writeFile(config.fs.basedir+config.static.upload+'/'+filename,new Buffer(ctx.req.body.data,'base64')).then(//本地保存
            //    function(res){
            //        return Promise.resolve(config.fs.url + config.static.head + '/' + filename);
            //    },function(err){
            //        return Promise.reject(util.errPackage(300029,errorcode));
            //    }
            //)
            util.writeFile(config.fs.basedir+config.static.upload+'/'+filename,new Buffer(ctx.req.body.data,'base64')).then();//本地保存
            qiniu.uploadBuf(new Buffer(ctx.req.body.data,'base64'),filename ,'','').then(//qiniu云保存
                function(res){
                    var url = config.qiniu.url + '/' + res.key;
                    resolve(ctx.body = util.successPackage({url: url}));
                },function(err){
                    resolve(ctx.body = util.errPackage(300034,errorcode));
                }
            )
        })
    }
})

//大V排行榜
//type:1 土豪
//type:2 幸运
urltable['/league'] = {header:[],post:['type']};
router.post('/league',function(ctx){
    return new Promise(function(resolve,reject) {
        if (ctx.req.body.type == '1') {
            dbo.consume.league().then(
                function(row){
                    resolve(ctx.body = util.successPackage({count:0,rows:row}));
                },function(err){
                    resolve(ctx.body = util.errPackage(300010, errorcode));
                }
            )
        } else if (ctx.req.body.type == '2') {
            dbo.win.league().then(
                function(row){
                    resolve(ctx.body = util.successPackage({count:0,rows:row}));
                },function(err){
                    resolve(ctx.body = util.errPackage(300010, errorcode));
                }
            )
        } else {
            resolve(ctx.body = util.successPackage({count: 0, rows: []}));
        }
    })
})

module .exports = {
    router:router,
    urltable:urltable,
};