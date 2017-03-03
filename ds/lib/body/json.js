/**
 * Created by wangd on 2016/2/16.
 */

var fslimit = 3*1024*1024;//default filesize is 3M
module.exports = function(req){
    return new Promise(function(resolve,reject){
        var list=[];
        req.on('data',ondata);
        req.on('end',onend);
        req.on('error',error);
        req.on('close',cleanup);
        function ondata(d){
            list.push(d);
        }
        function onend(){
            var data = Buffer.concat(list);

            if(data.length > fslimit){//data is too big
                reject("data is too big");
            }else {
                data = data.toString();
                try {
                    data = JSON.parse(data);
                } catch (e) {
                    if(/Content-Disposition:/.test(data)){
                        //data = decodeURI(data);
                        var tem = data.match(/name="[^"]*"\r\n\r\n[^\r]*/g);
                        var o = {};
                        for(var v of tem){
                            var key = decodeURI(v.replace(/^[^"]*"/,'').replace(/"\r\n\r\n.*$/,''));
                            var value = v.replace(/name="[^"]*"\r\n\r\n/,"");
                            o[key] = value;
                        }
                        data = o;

                    }else{
                        var tem = data.split(/&/);
                        var  o = {};
                        for(var v of tem){
                            o[v.replace(/=.*$/,'')] = v.replace(/^[^=]*=/,'');
                        }
                        data = o;
                    }
                }
                resolve(data);
            }
            cleanup();
        }
        function error(){
            reject("data transfer error");
            cleanup();
        }
        function cleanup(){
            req.removeListener('data',ondata);
            req.removeListener('end',onend);
            req.removeListener('error',error);
            req.removeListener('close',cleanup);
        }
    })
}