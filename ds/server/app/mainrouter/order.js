/**
 * Created by wangd on 2016/3/4.
 */

var util = require('../../util');
var dbo = require('../../../dbo');
var errorcode = require('../../errorcode');
var Router = require('koa-router');
var router = new Router();
var urltable = {};

urltable['/update'] = {header:["token"],post:[]};
router.post('/update',function(ctx){
    return new Promise(function(resolve,reject){
        dbo.order.update(ctx.req.body).then(
            function(res){
                if(!res) return resolve(ctx.body = util.errPackage(300038,errorcode));
                resolve(ctx.body = util.successPackage('success'));
            },function(err){
                reject(ctx.body = util.errPackage(300010, errorcode));//数据库操作错误
            }
        )
    })
})
//前端支付前，从服务端获取订单信息,那么这个时候数据库里的数据相关数据就应该被锁定了，比如某商品
urltable['/info'] = {header:["token"],post:[]};
router.post('/info',function(ctx){
    //根据前端传回的scid,购物车id,获取当前的购物数据，然后计算金额，生成标题，以即描述内容，订单号，
    //由于当前用的是bmop，暂时只需要传给bmop三个参数，所以
    //1.判断商品数量是否足够
    return new Promise(function(resolve,reject) {
        if (ctx.user.uid) {
            var _shopcars = null;
            dbo.shopcar.listByscids(ctx.req.body.scids).then(//获取相关的购物车数据
                function (shopcars) {
                    if(!shopcars.length) return Promise.reject(util.errPackage(300036, errorcode));//没有购物车
                    return dbo.single.settlement(shopcars);
                }, function (err) {
                    return Promise.reject(util.errPackage(300010, errorcode));//数据库操作错误
                }
            ).then(//库存扣除成功
                function (shopcars) {
                    _shopcars = shopcars;
                    if(!_shopcars.length) {//商品数量不足
                        return Promise.reject(util.errPackage(300037, errorcode))
                    }else{
                        return dbo.util.priceCalculato(shopcars);
                    }
                }, function (err) {
                    return Promise.reject(util.checkErrFormat(err)||util.errPackage(300010, errorcode));//数据库操作错误
                }
            ).then(//计算总价
                function (total_fee) {
                    //生成订单
                    //转移shopcar到shopcartem
                    return dbo.order.produceOrderTem({uid: ctx.user.uid, total_fee: total_fee, subject: "网盘", body: "网盘套餐购买"},_shopcars);
                }, function (err) {
                    return Promise.reject(util.checkErrFormat(err)||util.errPackage(300010, errorcode));//数据库操作错误
                }
            ).then(
                function (order) {
                    resolve(ctx.body = {
                        oid:order.oid,//商户网站唯一订单号
                        subject:order.subject,//商品名称
                        total_fee:order.total_fee,//总金额
                        body:order.body,//描述
                    })
                }, function (err) {
                    resolve(ctx.body = util.checkErrFormat(err)||util.errPackage(300010, errorcode));//数据库操作错误
                }
            )
        } else {
            resolve(ctx.body = util.errPackage(300003, errorcode));
        }
    })

    //ctx.body = {
    //    service:config.service,
    //    partner:config.partner,
    //    _input_charset:config._input_charset,
    //    sign_type:config.sign_type,
    //    sign:config.sign,
    //    seller_id:config.seller_id,//卖家支付宝账号，	xxx@alipay.com
    //    notify_url:config.notify_url,
    //    payment_type:'',//支付类型，默认值为：1（商品购买）。
    //    out_trade_no:'xxxxx',//商户网站唯一订单号
    //    subject:'',//商品名称
    //    total_fee:'0.01',//总金额
    //    body:'测试测试',//描述
    //};
})


module .exports = {
    router:router,
    urltable:urltable,
};