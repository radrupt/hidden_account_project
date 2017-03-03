/**
 * Created by wangd on 2015/12/10.
 */
//this is very dangerous, so use with caution
//这个功能非常非常危险，除非你能确定你要更新的内容是不被任何其它模块依赖的，那么你就可以放心的对其使用热更新
var stdin = require('../std').stdin;
var path = require('path');
var fs = require('fs');
var updatelist = {};
var getAllFiles = function (dir) {
    var filesArr = [];
    var dirArr = [];
    dirArr.push(dir);
    for(var v of dirArr){
        var arr = fs.readdirSync(v);
        for( j of arr ){
            var fpath = path.join(v,j);
            if(fs.statSync(fpath).isDirectory()){
                dirArr.push(fpath);
            }else{
                filesArr.push(fpath);
            }
        }
    }
    return filesArr;
}
var update = function (params){//es6
    if( !params.length ) {
        console.log(" need a param ");
        return;
    }
    if( updatelist[params[0]] ) {
        //删除该地址所在文件夹里的所有缓存
        //获取该地址所在的文件夹
        var dir = path.dirname(updatelist[params[0]]);
        var filesArr = getAllFiles(dir);
        for(var v of filesArr ) delete require.cache[require.resolve(v)];
        require(updatelist[params[0]]);
        console.log('success update!');
    }else{
        console.log('fail to update, because it is not belong updatelist');
    }
}
stdin.add('update','update',update);
module.exports = {
    add:function(key,path){
        updatelist[key] = path;
    }
}