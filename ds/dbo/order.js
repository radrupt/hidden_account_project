/**
 * Created by wangd on 2016/3/3.
 */
var mysql = require('../lib/mysql');
var db = mysql.db;
var dbt = mysql.dbt;

var order = {};

order.list = function(obj){
    return new Promise(function(resolve,reject){
        db.findPage('order',{type:obj.type ||1},'',obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}

order.update = function(obj){//在这里检查订单支付是否已经成功，成功，这里也做处理
    var where = db.genTableObject(obj,['oid']);
    var o = db.genTableObject(obj,['trade_no','out_trade_no','status','log','pay_type']);
    return new Promise(function(resolve,reject){
        db.update('order',o,where,function(err,res){
            if(err) return reject(err);
            if(!res.changedRows) return resolve(null);//插入失败，已有相同数据
            resolve(res);
        });
    })
}

order.produceOrderTem = function(obj,shopcars){
    var o = db.genTableObject(obj,['uid','total_fee','subject','body']);
    return new Promise(function(resolve,reject){
        dbt.trans(function(err,dbt){
            if(err) {
                reject(err);
            }else{
                dbt.insert('order',o,function(err,res){
                    if(err) {
                        dbt.rollback(function (rollbackerr) {
                            reject(rollbackerr || err);
                        })
                    }else if(!res.affectedRows) {//insert fail, has same data
                        dbt.rollback(function (err) {
                            if(err) reject(err);
                            else return resolve(null)
                        })
                    }else{
                        var order = Object.assign({oid:res.insertId},obj);
                        var sql = 'update shopcar set status = 2,oid = ? where  ';
                        sql = db.format(sql,order.oid);
                        for(var v of shopcars){
                            sql+=" scid = ? or";
                            sql = db.format(sql,v.scid);
                        }
                        sql = sql.replace(/or$/,'');
                        dbt.once(sql,function(err,res){
                            if(err) {
                                dbt.rollback(function (rollbackerr) {
                                    reject(callbackerr || err);
                                })
                            }
                            else{
                                dbt.commit(function (err) {
                                    if (err) {
                                        reject(err);
                                    } else {
                                        resolve(order)
                                    }
                                })
                            }
                        })
                    }
                })
            }
        })
    })
}

module.exports = order;