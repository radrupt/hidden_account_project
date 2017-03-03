/**
 * Created by wangd on 2016/2/24.
 */
var mysql = require('../lib/mysql');
var cache = require('../cache');
var db = mysql.db;
var dbt = mysql.dbt;

var single = {};//由于这里的数据是高频使用的数据，所以使用redis创建中间层

single.list = function(obj){
    return new Promise(function(resolve,reject){
        var order = '';
        var where = db.genTableObject(obj,['state','kind','type','tag','gid']);
        if(!obj.sortkey) order = '';
        else if(obj.sortkey == 'hot') order = ' period desc ';
        else if(obj.sortkey == 'new') order = ' starttime desc ';
        else if(obj.sortkey == 'progress') order = ' percent desc ';///percent字段是写在视图里的
        db.findPage('v_goods_single',where,order,obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}
single.historyWinList = function(obj){
    return new Promise(function(resolve,reject){
        var order = ' period desc';
        var where = db.genTableObject(obj,['gid']);
        db.findPage('v_user_goods_single_win',where,order,obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}
single.insolveList = function(obj){
    return new Promise(function(resolve,reject){
        var order = ' time desc';
        var where = db.genTableObject(obj,['sid']);
        db.findPage('v_user_consume',where,order,obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}
single.info = function(obj){
    return new Promise(function(resolve,reject){
        cache.single.findOne(obj.sid).then(
            function(res){
                if(res){
                    resolve(res);
                }else{
                    db.findOne('v_goods_single',{sid:obj.sid},function(err,res){
                        if(err)reject(err);
                        else resolve(res[0]);
                        if(res[0] && res[0].state == 1)//商品未结束
                        cache.single.insert(res[0]);
                    })
                }
            },function(err){
                reject(err);
            }
        )
    })
}
single.insert = function(obj){
    var o = db.genTableObject(obj,['gid','period','kind','starttime','endtime','restinvolve','state','price']);
    return new Promise(function(resolve,reject){
        db.insert('single',o,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);
            resolve(res);
        });
    })
}
single.settlement = function(shopcars){//结算,errcode:333--数量不足
    return new Promise(function(resolve,reject){
        dbt.trans(function(err,dbt){
            if(err) {
                reject(err);
                return;
            }
            var sql = ' update single set restinvolve = case sid ';
            var where  = ' where sid in (';
            for(var v of shopcars){
                sql += 'when ? then case when restinvolve >= ? then restinvolve - ? else restinvolve end ';
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
                    dbt.commit(function (err) {
                        if (err) {
                            reject(err);
                        } else {
                            resolve(shopcars)
                        }
                    })
                }
            })
        })
    })
}
module.exports = single;