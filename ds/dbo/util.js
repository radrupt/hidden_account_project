/**
 * Created by wangd on 2016/3/4.
 */
var mysql = require('../lib/mysql');
var db = mysql.db;
var dbt = mysql.dbt;
var Bmob = require("bmob").Bmob;
var config = require('../config');
var dbo = require('../dbo');

var util = {};

util.priceCalculato = function(shopcars){//shopcars:[{sid:xxx,number:xxx}]
    return new Promise(function(resolve,reject){
        //目前只有一种模式，即按照商品当前价钱处理
        var tatal_fee = 0;
        for(var v of shopcars){
            tatal_fee+=v.number;
        }
        resolve(tatal_fee);
    })
}
util.pay = function(out_trade_no){
    return new Promise(function(resolve,reject){
        Bmob.initialize(config.bmob.app_id, config.bmob.rest_api_key);
        Bmob.Pay.queryOrder(out_trade_no).then(function(obj) {
            if(obj.trade_state == 'SUCCESS'){
                db.findOne('order',{out_trade_no:out_trade_no},function(err,res){
                    if(err) return reject(err);
                    if(!res.length) return reject("不存在交易号："+out_trade_no);
                    var order = res[0];
                    dbt.trans(function(err,dbt) {
                        if (err) {
                            reject(err);
                            return;
                        }
                        dbt.find('shopcar',{oid:order.oid},'',function(err,logs){
                            if(err) return reject(err);
                            else{
                                dbt.update('order',{status:2,log:JSON.stringify(logs),trade_no:obj.transaction_id,pay_type:obj.pay_type},{oid:order.oid},function(err,res){
                                    if(err) return reject(err);
                                    if(!res.changedRows) {
                                        dbt.rollback(function (rollbackerr) {
                                            reject(rollbackerr || "更新失败");
                                        })
                                    }else {
                                        dbt.delete('shopcar', {oid: order.oid}, function (err, res) {
                                            if (err) return reject(err);
                                            dbt.commit(function (err) {
                                                if (err) {
                                                    reject(err);
                                                } else {
                                                    resolve('交易成功')
                                                }
                                            })
                                        })
                                    }
                                })
                            }
                        })
                    })
                    //事务
                    //更新订单状态
                    //将临时购物车的相关数据转移到log字段里
                    //删除购物车
                })

            }else{
                db.findOne('order',{out_trade_no:out_trade_no},function(err,res) {
                    if (err) return reject(err);
                    if (!res.length) return reject("不存在交易号：" + out_trade_no);
                    var order = res[0];
                    dbt.trans(function (err, dbt) {
                        if (err) {
                            reject(err);
                            return;
                        }
                        dbt.find('shopcar', {oid: order.oid}, '', function (err, shopcars) {
                            if (err) return reject(err);
                            else {
                                var sql = ' update single set restinvolve = case sid ';
                                var where  = ' where sid in (';
                                for(var v of shopcars){
                                    sql += 'when ? then case when restinvolve + ? <= price  then restinvolve + ? else price end ';
                                    sql = db.format(sql,[v.sid,v.number,v.number]);
                                    where += '?,';
                                    where = db.format(where,[v.sid]);
                                }
                                sql+=' end ';
                                where = where.replace(/,$/,'');
                                where+=')';
                                sql +=where;
                                dbt.once(sql, function (err, res) {
                                    if (err) {
                                        dbt.rollback(function (rollbackerr) {
                                            reject(rollbackerr || err);
                                        })
                                    } else if (res.changedRows!= shopcars.length) {//商品数量不足
                                        dbt.rollback(function (rollbackerr) {
                                            resolve(rollbackerr || {code: 333,sid:v.sid});
                                        })
                                    }else {
                                        dbt.update('shopcar', {status: 1, oid: 0}, {oid:order.oid},function(err,res){
                                            if (err) return reject(err);
                                            dbt.commit(function (err) {
                                                if (err) {
                                                    reject(err);
                                                } else {
                                                    resolve('交易失败，恢复数据');
                                                }
                                            })
                                        })
                                    }
                                })
                            }
                        })
                    })
                    //事务
                    //恢复购物车数据
                    //恢复商品库存数据
                })
            }
        }, function(err){
            reject("发送失败:"+err);
        });
    })
}
module.exports = util;