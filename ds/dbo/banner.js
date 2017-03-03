/**
 * Created by wangd on 2016/2/24.
 */
var mysql = require('../lib/mysql');
var db = mysql.db;

var banner = {};

banner.list = function(obj){
    return new Promise(function(resolve,reject){
        db.findPage('banner',{type:obj.type ||1},'',obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}

banner.insert = function(obj){
    var o = db.genTableObject(obj,['time','url','type']);
    return new Promise(function(resolve,reject){
        db.insert('banner',o,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);//insert fail, has same data
            resolve(res);
        });
    })
}

module.exports = banner;