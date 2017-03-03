/**
 * Created by wangd on 2015/12/10.
 */

var process = require('process');

var stdin = process.stdin;
process.stdin.setEncoding('utf8');


var cmd = {};//{type:{funcname:func}}

process.stdin.on('readable', function() {
    var chunk = process.stdin.read();
    if( !chunk ) return;
    var bash = (chunk.toString()).substr(0, (chunk.toString()).length - 1);
    var array = bash.split('--');
    var type = array[0];
    var funcname = array[1];
    if( cmd[type] && cmd[type][funcname] ){
        array.splice(0,2);
        cmd[type][funcname](array);
    }

});

module.exports = {
    add:function(type,key,value){
        if( cmd[type] ) cmd[type][key] = value;
        else {
            cmd[type] = {};
            cmd[type][key] = value;
        }
    }
};