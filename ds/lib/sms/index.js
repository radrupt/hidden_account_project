/**
 * Created by wangd on 2015/11/12.
 */
"use strict";
var querystring = require('querystring');
var http = require('http');
var crypto = require('crypto');
var config = require('../../config/aliapp');
var smsconfig = require('../../config/sms');

if( !config ) {
    throw "mysql need config infomation!";
}

class Top{
    constructor(options){
        this.config = {};
        if( options.appKey )       this.config.appKey = options.appKey;
        if( options.appSecret )    this.config.appSecret = options.appSecret;
        if( options.sessionKey )   this.config.sessionKey = options.sessionKey ;
        if( options.restURL )       this.config.restURL = options.restURL || 'http://gw.api.taobao.com/router/rest';
    }
    getAPI(method, params){
        var self = this;
        return new Promise(function(resolve, reject) {
            var p = {
                method: method,
                partner_id: "top-apitools",
                v: '2.0',
                app_key: self.config.appKey,
                format: "xml",
                timestamp: new Date().getTime(),
                sign_method: 'md5'
            };

            Object.assign(p, params);

            p.sign = createSign(p, self.config.appSecret);

            var postdata = querystring.stringify(p);

            httpGetPromise(self.config.restURL + "?" + postdata).then(
                function(body) {
                    resolve(body);
                },function(err) {reject(err);}
            );
        });
    }
    messageSend(params){
        var self = this;
        var method = "alibaba.aliqin.fc.sms.num.send";
        return new Promise((resolve, reject) => {
            self.getAPI(method, params).then(
                function(body) {
                    resolve(body);
                },
                function(err) {
                    reject(err);
                }
            );
        });
    }
}
var createSign = function(params, secret) {
    var s = secret;
    var keys = [];
    for (var key in params) {
        keys.push(key);
    }

    keys = keys.sort();

    for (var i in keys) {
        var key = keys[i];
        if( typeof key ==  'string' ) {
            s += key + params[key];
        }
    }
    s += secret;

    return crypto.createHash('md5').update(s, 'utf-8').digest('hex').toUpperCase();
}
var httpGetPromise  = function(href) {
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
        }).on('error', function(e){reject(e.message)});
    });
};
var client = '';
client = new Top({
    appKey:config.appKey,
    appSecret:config.appSecret,
    restURL: config.restURL,
    sessionKey:config.sessionKey
});
/**
 * 发送短信验证
 */
var sendMessage = function(mobile, code) {
    var mobilereg = /^(13|15|17|18|14)[0-9]{9}$/;
    var params = {
        extend: "",
        sms_type: "normal",
        sms_free_sign_name: smsconfig.sms_free_sign_name,
        sms_param: '',
        rec_num: '',
        sms_template_code: smsconfig.sms_template_code
    };
    var productname = 'dreamshow';
    return new Promise(function(resolve, reject) {
        if (!mobile || !mobilereg.test(mobile)) {

            reject();
            return;
        }
        params.rec_num = mobile;
        params.sms_param = '{"product":' + productname + ',"code":"' + parseInt(code) + '"}',
            client.messageSend(params).then(
                function(res) {
                    resolve(code)
                },
                function(err) {
                    reject();
                    throw err;
                }
            )
    })
}

module.exports = {
    sendMessage:sendMessage,
}
