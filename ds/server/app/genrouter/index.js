/**
 * Created by wangd on 2016/2/15.
 */
//func:

var util = require('../../util');
var errorcode = require('../../errorcode');
var body_json = require('../../../lib/body/json');
var Router = require('koa-router');
var router = new Router();

router.use(function (ctx, next) {//solve post request
    return new Promise(function(resolve,reject){
        body_json(ctx.req).then(
            function(result){
                ctx.req.body = result;
                resolve(next());
            },function(err){
                resolve(ctx.body = util.errPackage(31001,errorcode));
            }
        )
    })
});
router.get('91cc4e53af6329184b8162e0e26833a3.txt',function (ctx) {//used by Pressure measurement
    return new Promise(function(resolve,reject){
        util.readFile('/91cc4e53af6329184b8162e0e26833a3.txt').then(
            function(res){
                resolve(ctx.body = res);
            },function(err){
                resolve(ctx.body = 'error');
            }
        );
    })
});
module.exports = router;