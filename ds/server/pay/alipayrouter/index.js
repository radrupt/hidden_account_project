/**
 * Created by wangd on 2016/2/26.
 */

var util = require('../../util');
var errorcode = require('../../errorcode');
var redis = require('../../../redis');
var body_json = require('../../../lib/body/json');
var Router = require('koa-router');
var router = new Router();
var config = require('../../../config/alipay');
var cache = require('../../../cache');
var redis = require('../../../redis');
var dbo = require('../../../dbo');


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

//权限验证
router.use('/',function (ctx,next){
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
})


//支付宝支付完成，回调路由
router.post('/payment',function(ctx){

    //判断支付状态，成功，删除购物车。更新order状态
    //失败，更新order状态，根据shopcar恢复single的库存
    dbo.util.pay(ctx.req.body.out_trade_no).then(
        function(res){console.log(res);}
        ,function(err){console.log(err);}
    );
    ////
    //{ trade_status: '1',
    //    out_trade_no: '5cbbeb6a423f26a473a6f6e23abacc28',
    //    trade_no: '2016030421001004270245736035' }
    //根据返回的out_trade_no查询订单，查看状态，才能判断支付是否成功
    //支付成功有对应处理
    //支付失败有对应处理
    ctx.body = 'success';
})
module.exports = router;