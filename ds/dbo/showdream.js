/**
 * Created by wangd on 2016/2/25.
 */
var mysql = require('../lib/mysql');
var db = mysql.db;

var showdream = {};

showdream.insert = function(obj){
    return new Promise(function(resolve,reject){
        obj.time = new Date().getTime();
        var o = db.genTableObject(obj,['uid','sid','title','content','imagelist','time']);
        db.insert('showdream',o,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);//插入失败，已有相同数据
            resolve(res);
        });
    })
}

showdream.update = function(obj){
    var where = db.genTableObject(obj,['sdid']);
    obj.edittime = new Date().getTime();
    var o = db.genTableObject(obj,['uid','sid','title','content','imagelist','edittime']);
    return new Promise(function(resolve,reject){
        db.update('showdream',o,where,function(err,res){
            if(err) return reject(err);
            if(!res.changedRows) return resolve(null);//插入失败，已有相同数据
            resolve(res);
        });
    })
}

showdream.delete = function(obj){
    return new Promise(function(resolve,reject){
        var where = db.genTableObject(obj,['sdid']);
        db.delete('showdream',where,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);//插入失败，已有相同数据
            resolve(res);
        });
    })
}

showdream.list = function(obj){
    return new Promise(function(resolve,reject){
        var where = db.genTableObject(obj,['uid']);
        db.findPage('showdream',where,' time desc',obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}
//批量删除，批量添加
module.exports = showdream;