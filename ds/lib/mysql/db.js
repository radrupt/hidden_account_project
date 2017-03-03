/**
 * Created by wangd on 2016/2/18.
 */
var mysql      = require('mysql');
var config = require('../../config/mysql');

if( !config ) {
    throw "mysql need config infomation!";
}

var pool = mysql.createPool(config);

var db = {};

db.format = mysql.format;
db.err = function(code,errcode){
    var object = {};
    if(!errcode[code]) return {999:"dbo operate error"};
    object[code] = errcode[code];
    return object;
}
db.objectLength = function(object){
    var array = [];
    object = object || {};
    for(var k in object){array.push(k)};
    return array.length;
}
db.produceSet = function(object){
    var stri = " set";
    for(var i = 0; i < db.objectLength(object); i++){
        stri += " ?? = ?,";
    }
    stri = stri.replace(/,$/,'');
    stri = stri.replace(/set$/,'');
    stri = db.format(stri,db.object2Array(object));
    return stri;
}
db.produceWhere = function(where){
    var stri = " where";
    for(var i = 0; i < db.objectLength(where); i++){
        stri += " ?? = ? and";
    }
    stri = stri.replace(/and$/,'');
    stri = stri.replace(/where$/,'');
    stri = db.format(stri,db.object2Array(where));
    return stri;
}
db.produceOrder = function(orderby){
    return orderby && (" order by "+orderby) || '';
}
db.produceLimit = function(limit){
    limit = limit || {};
    limit.page = parseInt(limit.page) || 1;
    limit.pagesize = parseInt(limit.pagesize) || 15;
    var stri = " limit ?,? ";
    stri = stri.replace(/,$/,'');
    stri = db.format(stri,[(limit.page-1)*limit.pagesize,limit.pagesize]);
    return stri;
}
db.object2Array = function(object){
    var array = [];
    object = object || {};
    for(var k in object){
        array.push(k);
        array.push(object[k]);
    }
    return array;
}
db.genTableObject = function(obj,keyarr){
    var o = {};
    for(var v of keyarr){
        if(obj.hasOwnProperty(v)) o[v] = obj[v];
    }
    return o;
}

//where:object,orderby:string,limit:{page:x,pagesize:x}
db.findPage = function(tablename,where,orderby,limit,cb){
    if(!tablename) return cb("tablename is null");
    var sql = " select * from ?? ";
    sql = db.format(sql,[tablename]);
    sql += typeof where == 'string' ? where : db.produceWhere(where);
    sql += db.produceOrder(orderby);
    sql += db.produceLimit(limit);
    db.once(sql,cb);
};
db.findOne = function(tablename,where,cb){
    if(!tablename) return cb("tablename is null");
    var sql = " select * from ?? ";
    sql = db.format(sql,[tablename]);
    sql += db.produceWhere(where);
    sql += ' limit 1';
    db.once(sql,cb);
};
db.find = function(tablename,where,orderby,cb){
    if(!tablename) return cb("tablename is null");
    var sql = " select * from ?? ";
    sql = db.format(sql,[tablename]);
    sql += typeof where == 'string' ? where : db.produceWhere(where);
    sql += db.produceOrder(orderby);
    db.once(sql,cb);
};
db.insert = function(tablename,obj,cb){
    if(!tablename) return cb("tablename is null");
    var sql = " insert into ?? ";
    sql = db.format(sql,[tablename]);
    sql += db.produceSet(obj);
    db.once(sql,cb);
}
db.update = function(tablename,obj,where,cb){
    if(!tablename) return cb("tablename is null");
    var sql = " update ?? ";
    sql = db.format(sql,[tablename]);
    sql += db.produceSet(obj);
    sql += db.produceWhere(where);
    db.once(sql,cb);
}
db.delete = function(tablename,where,cb){
    if(!tablename) return cb("tablename is null");
    var sql = " delete from ?? ";
    sql = db.format(sql,[tablename]);
    sql += db.produceWhere(where);
    db.once(sql,cb);
}
db.once = function(sql,cb){
    pool.getConnection(function(err,con){
        if(err){
            throw err;
        }else{
            if(con){
                con.query(sql,function(err,result){
                    con.release();
                    if(err){
                        cb(err);
                    }else if(cb){
                        cb(null,result);
                    }
                })
            }else{
                throw 'mysqlsql:Failed to getConnection!';
            }
        }
    });
};
module.exports = db;