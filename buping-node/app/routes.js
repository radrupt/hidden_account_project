var path = require("path");
var fs = require("fs");

var config = require('../config');

var formidable = require(config.nmpath + '/formidable');

module.exports = function(app, passport) {

    // 加载静态资源
    app.use( function(req, res, next) {
        var pathname = req.path;
        var ext = path.extname(pathname);
        var localPath = ''; //本地相对路径
        if (ext.length > 0) {
            localPath = '.' + pathname;
            staticResHandler(localPath,ext,res);
        }else  next();

    });

    //验证用户是否登录
    app.use(isLoggedIn, function(req, res, next) {

        //judge weather user loggedin

        // continue doing what we were doing and go to the route
        next();
    });

// normal routes ===============================================================

    // show the home page (will also have our login links)
    app.get('/', function(req, res) {
        res.render('index.ejs', {
            user : req.user,
            message: req.flash('excelMessage')
        });
    });



    // ajax excel import
    app.post('/upload', function(req, res) {
        console.log("Request handler 'upload' was called.");
        var form = new formidable.IncomingForm();
        form.uploadDir="tmp";
        console.log("about to parse");
        form.parse(req, function(error, fields, files){//这边要做错误判断，先放着，之后做,这个error还不知怎么用
            console.log("about to parse1");
            require("../controller/importDB")(files, res);
            console.log("about to parse2");
            var filepath = files.upload.path;
            fs.unlink(filepath, function(err){
                if(err){
                    throw err;
                }
                console.log('文件:'+filepath+'删除成功！');
            })
        });
    });

    // ajax query
    app.post('/query', function(req, res) {
        require("../controller/query")(req, res, 0);
    });
    // ajax view no pass data
    app.post('/modify', function(req, res) {
        require("../controller/query")(req, res, 2); // 模式2表示查询所有no pass  的数据, 以后用全局替换
    });
    // ajax make no pass into pass
    app.post('/modifypost', function(req, res) {

        require("../app/models/detectdata").update(
                {_id:req.body._id},
                {$set:{exceptionhandle:req.body.exceptionhandle,
                       finalresult:req.user.email + ": " + req.body.finalresult,
                       resu1:"PASS"}},function(err){
            if( err ) res.json({success: false, message: "更新数据失败"});
            else res.json({success: true});
        });
        //此时才能用Model操作，否则报错
    });

    // LOGOUT ==============================
    app.get('/logout', function(req, res) {
        req.logout();
        res.redirect('/');
    });

// =============================================================================
// AUTHENTICATE (FIRST LOGIN) ==================================================
// =============================================================================

    // locally --------------------------------
    // LOGIN ===============================
    // show the login form
    app.get('/login', function(req, res) {
        res.render('login.ejs', { message: req.flash('loginMessage') });
    });

    // process the login form
    app.post('/login', passport.authenticate('local-login', {
        successRedirect : '/', // redirect to the secure profile section
        failureRedirect : '/login', // redirect back to the signup page if there is an error
        failureFlash : true // allow flash messages
    }));

    // SIGNUP =================================
    // show the signup form
    app.get('/signup', function(req, res) {
        res.send("注册功能已关闭");
        //res.render('signup.ejs', { message: req.flash('signupMessage') });
    });

    // process the signup form
    //app.post('/signup', passport.authenticate('local-signup', {
    //    successRedirect : '/', // redirect to the secure profile section
    //    failureRedirect : '/signup', // redirect back to the signup page if there is an error
    //    failureFlash : true // allow flash messages
    //}));

};

// route middleware to ensure user is logged in
function isLoggedIn(req, res, next) {

    if ( req.isAuthenticated() ) {
        if( req.path == "/login" || req.path == "/signup" ) {
            return res.redirect('/');
        }
        return next();
    }else{
        if( req.path == "/" || req.path == "/login" || req.path == "/signup" || req.path == "/query" ) {
            return next();
        }
        res.redirect("/login");
    }

}
//处理静态资源如css,js
function staticResHandler(localPath, ext, response) {
    fs.readFile(localPath, "binary", function (error, file) {
        if (error) {
            response.writeHead(500, { "Content-Type": "text/plain" });
            response.end("Server Error:" + error);
        } else {
            //response.send(file, "binary");
            response.writeHead(200, { "Content-Type": getContentTypeByExt(ext) });
            response.end(file, "binary");
        }
    });
}

//得到ContentType
function getContentTypeByExt(ext) {
    ext = ext.toLowerCase();
    if (ext === '.htm' || ext === '.html')
        return 'text/html';
    else if (ext === '.js')
        return 'application/x-javascript';
    else if (ext === '.css')
        return 'text/css';
    else if (ext === '.jpe' || ext === '.jpeg' || ext === '.jpg')
        return 'image/jpeg';
    else if (ext === '.png')
        return 'image/png';
    else if (ext === '.ico')
        return 'image/x-icon';
    else if (ext === '.zip')
        return 'application/zip';
    else if (ext === '.doc')
        return 'application/msword';
    else
        return 'text/plain';
}
