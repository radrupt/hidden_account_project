/**
 * Created by wangd on 2016/2/29.
 */
var redis = require('../../redis').single;

var update = function(single){
    redis.hmset("single_"+single.sid,single);
};

module.exports = update;