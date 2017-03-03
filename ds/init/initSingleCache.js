/**
 * Created by wangd on 2016/2/29.
 */

var single = require('../cache/single');
var redis = require('../redis');
var dbo = require('../dbo');
var initSingle = function(){
    redis.single.flushdb();//clear single redis
    dbo.single.list({state:1,page:1,pagesize:10000000}).then(
        function(row){
            for(var v of row){
                single.insert(v);
            }
        },function(err){
            console.log("fail to init single");
        }
    )
}
initSingle();