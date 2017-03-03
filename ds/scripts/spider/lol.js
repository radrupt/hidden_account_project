/**
 * Created by wangd on 2016/2/24.
 */

var dbo = require('../../dbo');
var stdin = require('../../lib/std').stdin;

var goodsarray = require('./data');//暂时用以前爬的数据

var goodsSpide = function(){
    for(var v of goodsarray) dbo.goods.insert(v).then();
    console.log("goods info inserting!");
};

stdin.add("spide",'lol',goodsSpide);//cmd: spide--lol
