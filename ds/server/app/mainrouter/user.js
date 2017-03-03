/**
 * Created by wangd on 2016/2/25.
 */
var util = require('../../util');
var dbo = require('../../../dbo');
var errorcode = require('../../errorcode');
var redis = require('../../../redis');
var sms = require('../../../lib/sms');
var qiniu = require('../../../lib/qiniu');
var Router = require('koa-router');
var router = new Router();
var config = require('../../../config');
var uuid = require('node-uuid');
var urltable = {};

//mobile,password,icode,手机号注册路由
urltable['/register/mobile'] = {header:[],post:['mobile','password','icode']};
router.post('/register/mobile',function(ctx){
    if( !util.checkMobile(ctx.req.body.mobile) ) {//mobile is error
        ctx.body = util.errPackage(300018,errorcode);
    }else{
        return new Promise(function(resolve,reject){
            util.getVByKRedis(redis.code,ctx.req.body.mobile+config.code.register).then(//判断验证码是否正确
                function(res){
                    if(res && res == ctx.req.body.icode) {
                        return dbo.user.findByMobile(ctx.req.body.mobile);
                    }
                    else return Promise.reject(util.errPackage(300015, errorcode));
                },function(err){
                    return Promise.reject(util.errPackage(300022, errorcode));//redis错误
                }
            ).then(//判断该手机号是否注册过
                function (res) {
                    if (res) {
                        return Promise.reject(util.errPackage(300019, errorcode));//手机号已经注册
                    } else {
                        redis.code.del(ctx.req.body.mobile+config.code.register);//注册成功删除验证码
                        return util.pbkdf2Sync(ctx.req.body.password, config.pbkdf2.salt, config.pbkdf2.iterations, config.pbkdf2.keylen);
                    }
                }, function (err) {
                    return Promise.reject(util.checkErrFormat(err)||util.errPackage(300010, errorcode));//数据库操作错误
                }
            ).then(
                function(password){
                    return dbo.user.mobileRegist(ctx.req.body.mobile,password);
                },function(err){
                    return Promise.reject(util.checkErrFormat(err)||util.errPackage(31000, errorcode));//加密出错
                }
            ).then(//注册成功获取用户信息
                function(succ){
                    if(succ){
                        return dbo.user.findByMobile(ctx.req.body.mobile);
                    }else{
                        return Promise.reject(util.errPackage(300019, errorcode));//手机号已注册
                    }
                },function(err){
                    return Promise.reject(util.checkErrFormat(err)||util.errPackage(300010, errorcode));//手机号注册数据库操作出错
                }
            ).then(//添加token
                function(res){
                    var token = util.md5(uuid.v1().toString());
                    util.updateToken(redis.token,res.uid,token,JSON.stringify(res)).then();//通过uid关联该用户的token，方便token变换时候的处理,不判错
                    res.token = token;
                    delete res.password;
                    resolve(ctx.body = util.successPackage(res));
                },function(err){
                    resolve(ctx.body = util.checkErrFormat(err)||util.errPackage(300010, errorcode));//数据库操作出错
                }
            )
        })
    }
})

//登陆，username(mobil或email或username),password
urltable['/login'] = {header:[],post:['username','password']};
router.post('/login',function(ctx){
    return new Promise(function(resolve,reject){
        util.pbkdf2Sync(ctx.req.body.password, config.pbkdf2.salt, config.pbkdf2.iterations, config.pbkdf2.keylen).then(
            function(password){
                return dbo.user.login(ctx.req.body.username,password);
            },function(err){
                return Promise.reject(util.errPackage(31000, errorcode));
            }
        ).then(
            function(res){
                if(!res){
                    resolve(ctx.body = util.errPackage(300023, errorcode));//用户名密码错误
                }else{
                    var token = util.md5(uuid.v1().toString());
                    util.updateToken(redis.token,res.uid,token,JSON.stringify(res)).then();//通过uid关联该用户的token，方便token变换时候的处理,不判错
                    res.token = token;
                    delete res.password;
                    resolve(ctx.body = util.successPackage(res));
                }
            },function(err){
                resolve(ctx.body = util.checkErrFormat(err)||util.errPackage(300010, errorcode));//数据库操作出错
            }
        )
    })
})

//更改密码,newpassword,password
urltable['/update/password'] = {header:['token'],post:['password','newpassword']};
router.post('/update/password', function (ctx) {
    if(!ctx.user.uid){
        ctx.body = util.errPackage(300003, errorcode);
    }else{
        return new Promise(function(resolve,reject){
            util.pbkdf2Sync(ctx.req.body.password, config.pbkdf2.salt, config.pbkdf2.iterations, config.pbkdf2.keylen).then(
                function(password){
                    if(password != ctx.user.password){//老密码输入错误
                        resolve(ctx.body = util.errPackage(300024, errorcode));
                    }else {
                        util.pbkdf2Sync(ctx.req.body.newpassword, config.pbkdf2.salt, config.pbkdf2.iterations, config.pbkdf2.keylen).then(
                            function (newpassword) {
                                return dbo.user.updatePassword(ctx.user.uid, password, newpassword);
                            }, function (err) {
                                return Promise.reject(util.errPackage(31000, errorcode));
                            }
                        ).then(
                            function(res){
                                if(!res){
                                    resolve(ctx.body = util.errPackage(300025, errorcode));//密码更新失败
                                }else {
                                    ctx.user.password = res;
                                    util.updateToken(redis.token, ctx.user.uid, ctx.header.token, JSON.stringify(ctx.user)).then();//通过uid关联该用户的token，方便token变换时候的处理,不判错
                                    resolve(ctx.body = util.successPackage('更新密码成功'));
                                }
                            },function(err){
                                resolve(ctx.body = util.checkErrFormat(err)||util.errPackage(300010, errorcode));//数据库操作出错
                            }
                        )
                    }
                },function(err){
                    resolve(ctx.body = util.errPackage(31000, errorcode));
                }
            )
        });
    }
});

//更改username,username
urltable['/update/username'] = {header:['token'],post:['username']};
router.post('/update/username',function(ctx){
    if(!ctx.user.uid){
        ctx.body = util.errPackage(300003, errorcode);//该token没有对用用户信息
    }else{
        return new Promise(function(resolve,rejecdt){
            dbo.user.update({uid:ctx.user.uid,username:ctx.req.body.username}).then(
                function(res){
                    if(!res){
                        resolve(ctx.body = util.errPackage(300026, errorcode));//用户名冲突
                    }else{
                        ctx.user.username = res.username;
                        util.updateToken(redis.token, ctx.user.uid, ctx.header.token, JSON.stringify(ctx.user)).then();//通过uid关联该用户的token，方便token变换时候的处理,不判错
                        resolve(ctx.body = util.successPackage('更新用户名成功'));
                    }
                },function(err){
                    resolve(ctx.body = util.errPackage(300010, errorcode));
                }
            )
        })
    }
})

//更改mobile,password,newmobile
urltable['/update/mobile'] = {header:['token'],post:['newmobile','password']};
router.post('/update/mobile',function(ctx){
    if(!ctx.user.uid){
        ctx.body = util.errPackage(300003, errorcode);
    }else{
        return new Promise(function(resolve,rejecdt){
            util.pbkdf2Sync(ctx.req.body.password, config.pbkdf2.salt, config.pbkdf2.iterations, config.pbkdf2.keylen).then(
                function(password){
                    if(password != ctx.user.password){//密码验证
                        return Promise.reject(util.errPackage(300008, errorcode));
                    }else {
                        return dbo.user.update({uid:ctx.user.uid,mobile:ctx.req.body.newmobile});
                    }
                },function(err){
                    return Promise.reject(util.errPackage(31000, errorcode));
                }
            ).then(
                function(res){
                    if(!res){
                        resolve(ctx.body = util.errPackage(300002, errorcode));//手机号名冲突
                    }else{
                        ctx.user.mobile = res.mobile;
                        util.updateToken(redis.token, ctx.user.uid, ctx.header.token, JSON.stringify(ctx.user)).then();//通过uid关联该用户的token，方便token变换时候的处理,不判错
                        resolve(ctx.body = util.successPackage('更新手机号成功'));
                    }
                },function(err){
                    resolve(ctx.body = util.checkErrFormat(err)||util.errPackage(300010, errorcode));
                }
            )
        })
    }
})

//更改email，password,newemail
urltable['/update/email'] = {header:['token'],post:['newemail','password']};
router.post('/update/email',function(ctx){
    if(!ctx.user.uid){
        ctx.body = util.errPackage(300003, errorcode);
    }else{
        return new Promise(function(resolve,rejecdt){
            util.pbkdf2Sync(ctx.req.body.password, config.pbkdf2.salt, config.pbkdf2.iterations, config.pbkdf2.keylen).then(
                function(password){
                    if(password != ctx.user.password){//密码验证
                        return Promise.reject(util.errPackage(300008, errorcode));
                    }else {
                        return dbo.user.update({uid:ctx.user.uid,email:ctx.req.body.newemail});
                    }
                },function(err){
                    return Promise.reject(util.errPackage(31000, errorcode));
                }
            ).then(
                function(res){
                    if(!res){
                        resolve(ctx.body = util.errPackage(300002, errorcode));//手机号名冲突
                    }else{
                        ctx.user.email = res.email;
                        util.updateToken(redis.token, ctx.user.uid, ctx.header.token, JSON.stringify(ctx.user)).then();//通过uid关联该用户的token，方便token变换时候的处理,不判错
                        resolve(ctx.body = util.successPackage('更新邮箱成功'));
                    }
                },function(err){
                    resolve(ctx.body = util.checkErrFormat(err)||util.errPackage(300010, errorcode));
                }
            )
        })
    }
})

//更改签名,signature
urltable['/update/signature'] = {header:['token'],post:['signature']};
router.post('/update/signature',function(ctx){
    if(!ctx.user.uid){
        ctx.body = util.errPackage(300003, errorcode);//该token没有对用用户信息
    }else{
        return new Promise(function(resolve,rejecdt){
            dbo.user.update({uid:ctx.user.uid,signature:ctx.req.body.signature}).then(
                function(res){
                    if(!res){
                        resolve(ctx.body = util.errPackage(300028, errorcode));//更新失败
                    }else{
                        ctx.user.signature = res.signature;
                        util.updateToken(redis.token, ctx.user.uid, ctx.header.token, JSON.stringify(ctx.user)).then();//通过uid关联该用户的token，方便token变换时候的处理,不判错
                        resolve(ctx.body = util.successPackage('更新签名成功'));
                    }
                },function(err){
                    resolve(ctx.body = util.errPackage(300010, errorcode));
                }
            )
        })
    }
})

//更新头像，前端上传base64去头图片编码，return headurl
//params:imgdata,imgext
urltable['/update/headimage'] = {header:['token'],post:['imgdata','imgext']};
router.post('/update/headimage',function(ctx){
    if(!ctx.user.uid){
        ctx.body = util.errPackage(300003, errorcode);//该token没有对用用户信息
    }else{
        return new Promise(function(resolve,rejecdt){
            var imagename = util.md5(uuid.v1().toString())+'.'+ctx.req.body.imgext;
            //util.writeFile(config.fs.basedir+config.static.head+'/'+imagename,new Buffer(ctx.req.body.imgdata,'base64')).then(//本地保存
            //    function(res){
            //        return dbo.user.update(ctx.user.uid, 'headurl', config.fs.url + config.static.head + '/' + imagename);
            //    },function(err){
            //        return Promise.reject(util.errPackage(300029,errorcode));//图片保存失败
            //    }
            //)
            util.writeFile(config.fs.basedir+config.static.head+'/'+imagename,new Buffer(ctx.req.body.imgdata,'base64')).then();//本地保存
            qiniu.uploadBuf(new Buffer(ctx.req.body.imgdata,'base64'),imagename ,'','').then(//qiniu云保存
                function(res){
                    var headurl = config.qiniu.url + '/' + res.key;
                    return dbo.user.update({uid:ctx.user.uid, headurl: headurl});
                },function(err){
                    return Promise.reject(util.errPackage(300029,errorcode));//图片保存失败
                }
            ).then(
                function(res){
                    if(!res){
                        resolve(ctx.body = util.errPackage(300028, errorcode));//更新失败
                    }else{
                        ctx.user.headurl = res.headurl;
                        util.updateToken(redis.token, ctx.user.uid, ctx.header.token, JSON.stringify(ctx.user)).then();//通过uid关联该用户的token，方便token变换时候的处理,不判错
                        resolve(ctx.body = util.successPackage({headurl:res}));
                    }
                },function(err){
                    resolve(ctx.body = util.checkErrFormat(err)||util.errPackage(300010, errorcode));
                }
            )
        })
    }
})

//更新个人信息

module .exports = {
    router:router,
    urltable:urltable,
};