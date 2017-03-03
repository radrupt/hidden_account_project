/**
 * Created by wangd on 2016/2/15.
 */
module.exports = {
    checkErrFormat:     function(err){
        if(err.errcode&&err.errmessage&&err.status)  return err;
        return null;
    },
    errPackage :        function(code,errc){
        return {
            errcode:code == 'err' ? '311111' : code.toString(),
            errmessage:errc[code],
            status:'fail'
        };
    },
    successPackage :    function(data){
        return {
            data:data || {},
            status:'success'
        };
    },
    checkAuth :         function(url,header,params,table){//ĿǰȨ���жϵ��ֶζ�Ĭ��������ͷ�������ϸ�������
        if( !table.hasOwnProperty(url) ) return true;//��·�ɲ���·�ɱ���
        var headerkeys = table[url].header || [];
        for( var v of headerkeys ){
            if( !header[v] ) return false;//����ͻ�������ͷ�ﲻ������Ҫ����ֶΣ���Ȩ��ʧ��
        }
        var postkeys = table[url].post || [];
        for( var v of postkeys ){
            if( !params[v] ) return false;//����ͻ���post�����ﲻ������Ҫ����ֶΣ���Ȩ��ʧ��
        }
        return true ;
    },
    checkMobile :       function(mobile) {//����ֻ���ʽ�Ƿ���ȷ
        var mobilereg = /^(13|15|17|18|14)[0-9]{9}$/;
        if( !mobile || !mobilereg.test(mobile) ) {
            return false;
        }
        return true;
    },
    httpGetPromise :    function(href) {
        var http = require('http');
        return new Promise(function(resolve,reject) {
            http.get(href, function(res) {
                var size = 0;
                var chunks = [];
                res.on('data', function(chunk) {
                    size += chunk.length;
                    chunks.push(chunk);
                });
                res.on('end', function() {
                    var data = Buffer.concat(chunks, size);
                    resolve(data);
                });
            }).on('error', function(e){ reject(e.message)});
        });
    },
    pbkdf2Sync :        function(text,salt,iterations,keylen){
        return new Promise(function(resolve,reject){
            var crypto = require('crypto');
            crypto.pbkdf2(text,salt,iterations,keylen,function(err,key){
                if( err ){
                    reject(err);
                }else{
                    resolve(key.toString());
                }
            })
        })
    },
    md5 :               function(s){
        var crypto = require('crypto');
        return crypto.createHash('md5').update(new Date().getTime() + s.toString(), 'utf-8').digest('hex').toUpperCase();
    },
    readFile :          function(path) {
        return new Promise(function(resolve,reject){
            var fs = require('fs');
            fs.readFile(path, function (err, data) {
                if (err) reject(err);
                resolve(data)
            });
        })
    },
    writeFile :         function(path,data) {
        return new Promise(function(resolve,reject){
            var fs = require('fs');
            fs.writeFile(path,data, function (err, data) {
                if (err) {
                    reject(err);
                }else resolve(data)
            });
        })
    },
    logger :            function(main,filepath,filename,line) {
        var CRLF = '\r\n';
        var data = "filename: "+filename+", line number: "+line;
        data += CRLF;
        data += "TIME:" + new Date();
        data += CRLF;
        data += main.stack || main;
        data += CRLF;
        data += CRLF;
        data += CRLF;
        var fs = require('fs');
        fs.appendFile(filepath,data.toString(),'utf-8',function(err){
            if(err) console.log(err);
        });
    },
    undefined2String :  function(s) {
        if( s == undefined ) s ='';
        if( typeof s == 'object' ) {
            for( var k in s ) if( s[k] == undefined ) s[k] = '';
        }
        return s;
    },
    existRedisKey   :   function(redis,key) {
        return new Promise(function(resolve,reject) {
            redis.get(key,function(err,res){
                if(err) reject(err);
                else if(!res) reject();
                else resolve();
            })
        })
    },
    getVByKRedis  :     function(redis,key){
        return new Promise(function(resolve,reject) {
            redis.get(key,function(err,res){
                if(err) reject(err);
                else resolve(res);
            })
        })
    },
    genNumber   :       function(len){
        len = len || 4;
        var s1 = '123456789';
        var s2 = '0123456789';
        var code = '';
        code += s1.substr(parseInt(Math.random() * 9), 1);
        for (var i = 0; i < len-1; i++) {
            code += s2.substr(parseInt(Math.random() * 10), 1);
        }
        return code;
    },
    updateToken:      function(redis,uid,token,value){
        return new Promise(function(resolve,reject){
            redis.get(uid,function(err,res){
                if(err) reject(err);
                else{
                    redis.del(res);
                    redis.set(uid,token);
                    redis.set(token,value);
                    resolve();
                }
            });
        })
    },
    addObjectKeyPrefix: function(obj,prefix){
        var o = {};
        for(var k in obj){
            o[prefix+k] = obj[k];
        }
        return o;
    }
}