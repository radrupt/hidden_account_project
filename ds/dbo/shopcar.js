/**
 * Created by wangd on 2016/2/24.
 */
var mysql = require('../lib/mysql');
var db = mysql.db;

var shopcar = {};

shopcar.insert = function(obj){
    return new Promise(function(resolve,reject){
        var o = db.genTableObject(obj,['sid','uid','number']);
        db.insert('shopcar',o,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);//插入失败，已有相同数据
            resolve(res);
        });
    })
}

shopcar.update = function(obj){
    var where = db.genTableObject(obj,['scid']);
    var o = db.genTableObject(obj,['sid','uid','number','oid','status']);
    return new Promise(function(resolve,reject){
        db.update('shopcar',o,where,function(err,res){
            if(err) return reject(err);
            if(!res.changedRows) return resolve(null);//插入失败，已有相同数据
            resolve(res);
        });
    })
}

shopcar.delete = function(obj){
    return new Promise(function(resolve,reject){
        var where = db.genTableObject(obj,['scid']);
        db.delete('shopcar',where,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);//插入失败，已有相同数据
            resolve(res);
        });
    })
}

shopcar.list = function(obj){
    return new Promise(function(resolve,reject){
        db.findPage('shopcar',{uid:obj.uid,status:1},'',obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}
shopcar.listUsed = function(obj){//已使用的购物车数据
    return new Promise(function(resolve,reject){
        db.findPage('shopcar',{uid:obj.uid,status:2},'',obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}
shopcar.listByscids = function(scids){//scids:array
    return new Promise(function(resolve,reject){
        if(!(scids instanceof Array)) scids = [scids];
        var where  = '';
        if(scids.length) where  = ' where';
        for(var v of scids) where += ' scid = ? or';
        where = where.replace(/or$/,'');
        where = where.replace(/where$/,'');
        where = db.format(where,scids);
        db.find('shopcar',where,'',function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}
module.exports = shopcar;