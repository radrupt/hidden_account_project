/**
 * Created by wangd on 2016/2/24.
 */
var mysql = require('../lib/mysql');
var db = mysql.db;

var goods = {};

goods.insert = function(obj){
    var o = db.genTableObject(obj,['name','smallimage','bigimage','type','tag','price']);
    return new Promise(function(resolve,reject){
        db.insert('goods',o,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);
            resolve(res);
        });
    })
}

goods.list = function(obj){
    return new Promise(function(resolve,reject){
        db.find('goods',{},'',function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}
module.exports = goods;