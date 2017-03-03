/**
 * Created by wangd on 2015/11/19.
 */

var qiniu = require('qiniu');
var config = require('../../config/qiniu');
if( !config ) {
    throw "mysql need config infomation!";
}
qiniu.conf.ACCESS_KEY = config.ACCESS_KEY
qiniu.conf.SECRET_KEY = config.SECRET_KEY;

function PutPolicy(scope, callbackUrl, callbackBody, returnUrl, returnBody,
                   asyncOps, endUser, expires) {
    this.scope = scope || null;
    this.callbackUrl = callbackUrl || null;
    this.callbackBody = callbackBody || null;
    this.returnUrl = returnUrl || null;
    this.returnBody = returnBody || null;
    this.asyncOps = asyncOps || null;
    this.endUser = endUser || null;
    this.expires = expires || 3600;
}

function uptoken(bucketname,callbackUrl,callbackBody) {
    var putPolicy = new qiniu.rs.PutPolicy(bucketname);
    putPolicy.callbackUrl = callbackUrl;
    putPolicy.callbackBody = callbackBody;
    //putPolicy.returnUrl = returnUrl;
    //putPolicy.returnBody = returnBody;
    //putPolicy.asyncOps = asyncOps;
    //putPolicy.expires = expires;

    return putPolicy.token();
}

function uploadBuf(body, key, uptoken) {
    return new Promise(function(resolve,reject) {
        var extra = new qiniu.io.PutExtra();
        //extra.params = params;
        //extra.mimeType = mimeType;
        //extra.crc32 = crc32;
        //extra.checkCrc = checkCrc;
        qiniu.io.put(uptoken, key, body, extra, function (err, ret) {
            if (!err) {
                // 上传成功， 处理返回值
                //console.log(ret.key, ret.hash);
                resolve(ret)
            } else {
                // 上传失败， 处理返回代码
                reject(err)
                throw err;
                // http://developer.qiniu.com/docs/v6/api/reference/codes.html
            }
        });
    });
}

function downloadUrl(domain, key) {
    var baseUrl = qiniu.rs.makeBaseUrl(domain, key);
    var policy = new qiniu.rs.GetPolicy();
    return policy.makeRequest(baseUrl);
}

//初始化上传策略
PutPolicy('');

module.exports = {
    uploadBuf:function(filesrc,filename,callbackUrl,callbackBody){
        //生成token
        var token = uptoken(config.bucketname || '',callbackUrl,callbackBody);
        return uploadBuf(filesrc,filename,token);
    },
    genToken:(callbackUrl,callbackBody) => uptoken(config.bucketname || '',callbackUrl,callbackBody),
    downloadUrl:downloadUrl,
}