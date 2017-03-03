/**
 * Created by wangd on 2016/2/24.
 */

var dbo = require('../../dbo');
var stdin = require('../../lib/std').stdin;
var cache = require('../../cache');

var addSingle = function(){//将goods里的新数据添加到single表里去
    dbo.goods.list().then(
        function(row){
            for( var v of row ){
                dbo.single.insert({
                    gid:v.gid,
                    kind:2,
                    price:v.price,
                    starttime:new Date().getTime(),
                    restinvolve:v.price,
                }).then();
            }
            console.log("start to add new single");
        },function(err){
            console.log("sql op error");
        }
    )
};
stdin.add("single",'add',addSingle);//cmd: single--add

var delteSingleCache = function(){
    cache.single.delete(1387);
}

stdin.add("single",'del',delteSingleCache);//cmd: single--del
