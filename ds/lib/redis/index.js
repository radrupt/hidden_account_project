/**
 * Created by wangd on 2016/2/15.
 */
var Redis = require('redis');
var config = require('../../config/redis');
if( !config ) {
    throw "redis need config infomation!";
}

module.exports = function(index){
    var cli = '';//redisclient

    cli = Redis.createClient(config.port,config.host,{auth_pass:config.auth_pass});
    cli.select(index || 0);
    cli.on('error', function(err) {
        console.log('redis Error');
        throw err;
    });
    return cli;
};