/**
 * Created by wangd on 2016/2/15.
 */
var Koa = require('koa');
var server = new Koa();
var Router = require('koa-router');
var router = new Router();
var config = require('../../config').app;

router.use('/', require('./genrouter').routes());//gengeral route
router.use('/app', require('./mainrouter').routes());//mobile app route

server.use(router.routes());

server.listen(config.mainport);