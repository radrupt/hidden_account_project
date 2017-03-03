/**
 * Created by wangd on 2016/2/25.
 */
var mysql = require('../lib/mysql');
var db = mysql.db;

var consume = {};

consume.list = function(obj){
    return new Promise(function(resolve,reject){
        db.findPage('consume',{uid:obj.uid},' time desc',obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}

consume.insert = function(obj){
    obj.time = new Date().getTime();
    var o = db.genTableObject(obj,['uid','sid','paytype','price','time','ip','address','transactionno','description','kind']);
    return new Promise(function(resolve,reject){
        db.insert('consume',o,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);//insert fail, has same data
            resolve(res);
        });
    })
}

consume.league = function(obj){
    return new Promise(function(resolve,reject){
        db.find('v_consume_league',function(err,res){
            if(err) return reject(err);
            resolve(res);
        });
    })
}

module.exports = consume;