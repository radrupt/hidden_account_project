/**
 * Created by wangd on 2016/2/18.
 */
var mysql = require('../lib/mysql');
var db = mysql.db;

var user = {};

//return newvalue || false
user.update = function(obj){
    return new Promise(function(resolve,reject){
        var o = db.genTableObject(obj,['mobile','username','email','regtime','headurl','signature']);
        var where = {uid:obj.uid};
        db.update('user',o,where,function(err,res){
            if(err) reject(err);
            else if(!res.changedRows) resolve(false);
            else resolve(obj);
        })
    })
}

//return object || null
user.find = function(obj){
    var where = db.genTableObject(obj,['mobile','username','email','regtime','headurl','signature']);
    return new Promise(function(resolve,reject){
        db.find('user',where,'',function(err,row){
            if(err)reject(err);
            else resolve(row);
        })
    })
}
//return object || null
user.login = function(username,password){//support mobile or email or username login
    return new Promise(function(resolve,reject){
        if(!username || !password) return resolve(null);
        var sql = 'select * from user where (mobile = ? or username = ? or email = ?) and password = ?';
        sql = db.format(sql,[username,username,username,password]);
        db.once(sql,function(err,res){
            if(err)reject(err);
            else resolve(res[0])
        })
    })
}
//return true or false;
user.mobileRegist = function(mobile,password){
    return new Promise(function(resolve,reject){
        if(!mobile || !password) return resolve(false);
        var sql = 'insert into user (mobile,password,regtime) values(?,?,?) ';
        sql = db.format(sql,[mobile,password,new Date().getTime()]);
        db.once(sql,function(err,res){
            if(err) reject(err);
            else if(!res.affectedRows) resolve(false);
            else resolve(true);
        })
    })
}
//return newpassword or false;
user.updatePassword = function(uid,oldpassword, newpassword){
    return new Promise(function(resolve,reject){
        if(!uid || !oldpassword || !newpassword) return resolve(false);
        var sql = " update user set password=? where uid = ? and password = ?";
        sql = db.format(sql,[newpassword,uid,oldpassword]);
        db.once(sql,function(err,res){
            if(err) reject(err);
            else if(!res.changedRows) resolve(false);
            else resolve(newpassword);
        })
    })
}

module.exports = user;