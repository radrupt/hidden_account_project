/**
 * Created by wangd on 2016/2/22.
 */
var mysql = require('../lib/mysql');
var db = mysql.db;
var dbt = mysql.dbt;

var consigneeaddress = {};
var tablename = 'consigneeaddress';

consigneeaddress.list = function(obj){
    return new Promise(function(resolve,reject){
        db.findPage(tablename,{uid:obj.uid},'caid asc',obj,function(err,row){
            if(err) return reject(err);
            resolve(row);
        });
    })
}
//insert,update,delete,only one isdefault which can be 1
consigneeaddress.insert = function(obj){
    var o = db.genTableObject(obj,['uid','qq','area','name']);
    return new Promise(function(resolve,reject){
        var sql = " insert into consigneeaddress ";
        sql += db.produceSet(o);
        sql += ' , isdefault = case when EXISTS(select * from (select * from consigneeaddress where isdefault = 1) a) then 0 else 1 end';//ͬʱֻ��һ����¼Ϊ1
        db.once(sql,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);
            resolve(res);
        });
    })
}
consigneeaddress.update = function(obj){
    var o = db.genTableObject(obj,['uid','qq','area','name','isdefault']);
    return new Promise(function(resolve,reject){
        var sql = " update consigneeaddress ";
        var wheresql = " where caid = ?";
        wheresql = db.format(wheresql,[obj.caid]);
        sql += db.produceSet(o);
        if( obj.isdefault == 0 ) {
            sql += ' , isdefault = case when EXISTS(select * from (select * from consigneeaddress where isdefault = 1) a) then 0 else 1 end';//ͬʱֻ��һ����¼Ϊ1
            sql += wheresql;
        }else if( obj.isdefault == 1 ) {
            sql += ' , isdefault = case when caid = ? then 0 else 1 end';
            sql = db.format(sql,[obj.caid]);
        }
        db.once(sql,function(err,res){
            if(err) return reject(err);
            if(!res.changedRows) return resolve(null);
            resolve(res);
        });
    })
}
consigneeaddress.delete = function(obj){
    return new Promise(function(resolve,reject){
        if(!obj.caid) return resolve(null);
        var sql = " delete from consigneeaddress ";
        sql+= " where caid = ? ";
        sql = db.format(sql,[obj.caid]);
        db.once(sql,function(err,res){
            if(err) return reject(err);
            if(!res.affectedRows) return resolve(null);
            //删除成功，确保余下的记录有一个是1
            var sql = " update consigneeaddress set isdefault = case when EXISTS(select * from (select * from consigneeaddress where isdefault = 1) a) then isdefault else 1 end  ORDER BY id LIMIT 1 ";
            db.once(sql,function(err,result){
                if(err) return reject(err);
                if(!result.affectedRows) return resolve(null);
                resolve(res);
            })
        })
    })
}
module.exports = consigneeaddress;