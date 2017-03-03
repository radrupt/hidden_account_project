/**
 * Created by wangd on 2016/2/18.
 */
var mysql      = require('mysql');
var db          = require('./db');
var config = require('../../config/mysql');

if( !config ) {
    throw "mysql need config infomation!";
}

var pool = mysql.createPool(config);

var obj = {};
function trans(conn){
    if(!conn) {
        throw 'conn is null';
        return null;
    }
    obj.once = function(sql,cb){//����db�ķ���
        conn.query(sql,function(err,result){
            if(err){
                conn.rollback(function () {
                    try {

                    } catch (err) {
                        throw err;
                    }
                    if(cb) cb(err);
                });
            }
            if(cb) {
                cb(null,result);
            }
        })
    };
    obj.commit = function(cb){
        conn.commit(function (err) {
            if (err) {
                conn.rollback(function () {
                    try {
                        conn.release();
                    } catch (err) {
                        throw err;
                    }
                    if(cb)cb(err);
                });
            }else{
                try {
                    conn.release();
                } catch (err) {
                    throw err;
                }
                if(cb)cb(null);
            }
        });
    }
    obj.rollback = function(cb) {
        conn.rollback(function () {
            try{
                conn.release();
            } catch(err) {
                throw err;
            }
            if(cb)cb();
        });
    }
    obj.format = mysql.format;
    obj = Object.assign(db,obj);
    return obj;
}

pool.trans = function(cb){
    pool.getConnection(function(err,con){
        if(err){
            throw err;
        }else{
            if(con){
                con.beginTransaction(function (err) {
                    if (err) {
                        cb(err);
                    }else{
                        cb(null,trans(con));
                    }
                })
            }else{
                throw 'mysqlsql:Failed to getConnection!';
            }
        }
    });
};
module.exports = pool;