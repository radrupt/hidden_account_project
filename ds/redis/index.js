/**
 * Created by wangd on 2016/2/19.
 */
var redis = require('../lib/redis');

module.exports = {
    token:redis(0),//用户登录的token
    code:redis(1),//用户使用验证码服务
    url:redis(2),//路由访问
    single:redis(3),//上架商品信息缓存到redis,数量变化的时候，同步更新redis,    暂时不做这件事情
    shopcar:redis(4),//购物车，暂时还是写在数据库里的
}