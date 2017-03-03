/**
 * Created by wangd on 2016/2/26.
 */
const https = require('https');
const fs = require('fs');

var Koa = require('koa');
var alipayserver = new Koa();

var config = require('../../config').app;

alipayserver.use(require('./alipayrouter').routes());

//const options = {
//    key: fs.readFileSync('xxx/keys/agent2-key.pem'),
//    cert: fs.readFileSync('xxx/keys/agent2-cert.pem')
//};

//alipayserver.listen = function(){//replace http with https
//    const server = https.createServer(options,this.callback());
//    return server.listen.apply(server, arguments);
//}
alipayserver.listen(config.alipayport);