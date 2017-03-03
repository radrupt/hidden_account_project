/**
 * Created by wangd on 2016/2/25.
 */
var mysql = require('../lib/mysql');
var db = mysql.db;

var win = {};

win.list = function(obj){
    return new Promise(function(resolve,reject){
        db.findPage('win',{uid:obj.uid},' time desc',obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}

win.insert = function(obj){
    obj.time = new Date().getTime();
    var o = db.genTableObject(obj,['uid','sid','lucknumber','involvenumber','time','kind']);
    return new Promise(function(resolve,reject){
        db.insert('win',o,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);//insert fail, has same data
            resolve(res);
        });
    })
}

win.update = function(obj){
    var where = db.genTableObject(obj,['wid']);
    var o = db.genTableObject(obj,['qq','name','area','status','pickuptime','sendtime']);
    return new Promise(function(resolve,reject){
        db.update('win',o,where,function(err,res){
            if(err) return reject(err);
            if(!res.changedRows) return resolve(null);//插入失败，已有相同数据
            resolve(res);
        });
    })
}

win.league = function(obj){
    return new Promise(function(resolve,reject){
        db.find('v_luck_league',function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);//插入失败，已有相同数据
            resolve(res);
        });
    })
}

module.exports = win;