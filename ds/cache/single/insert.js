/**
 * Created by wangd on 2016/2/29.
 */
var redis = require('../../redis').single;

var insert = function(single){
    redis.hmset("single_"+single.sid,single);
    redis.lpush('single_id',single.sid);
};

module.exports = insert;