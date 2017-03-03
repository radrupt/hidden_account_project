/**
 * Created by wangd on 2016/2/29.
 */
var redis = require('../../redis').single;

var findOne = function(id){
    return new Promise(function(resolve,reject){
        redis.hgetall('single_'+id,function(err,res){
            if(err) reject(err);
            else resolve(res);
        })
    })
}
module .exports = findOne;