/**
 * Created by wangd on 2016/2/29.
 */
var redis = require('../../redis').single;

var _delete = function(sid){
    var multi = redis.multi();//开启事务,将下文所有的操作缓存起来
    multi.del("single_"+sid);
    redis.lrange('single_id',0,-1,function(err,row){
        if(err) return;
        multi.del("single_id");
        for(var v of row){
            if(v != sid) multi.lpush("single_id",v);
        }
        multi.exec();//开始执行缓存中得所有操作。
    });
}

module .exports = _delete;