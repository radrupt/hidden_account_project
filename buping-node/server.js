// server.js

// set up ======================================================================
// get all the tools we need
var config = require('./config');

var express  = require(config.nmpath + '/express');
var app      = express();

var port     = process.env.PORT || config.port;
var mongoose = require(config.nmpath + '/mongoose');

var passport = require(config.nmpath + '/passport');
var flash    = require(config.nmpath + '/connect-flash');

var morgan       = require(config.nmpath + '/morgan');
var cookieParser = require(config.nmpath + '/cookie-parser');
var bodyParser   = require(config.nmpath + '/body-parser');
var session      = require(config.nmpath + '/express-session');

// configuration ===============================================================
// connect to our database
var db = mongoose.createConnection();
module.exports = {
    'db'    :   db
};
var options = {
    db: { native_parser: true },
    server: { poolSize: 5 },
    user: config.user,
    pass: config.pass
}

db.open(config.dbhost, config.dbname, config.dbport, options);
db.on('error', function (err) {
    console.log("connect error :" + err);
    //监听BAE mongodb异常后关闭闲置连接
    db.close();
});
//监听db close event并重新连接
db.on('close', function () {
    console.log("connect close retry connect ");
    db.open(config.dbhost, config.dbname, config.dbport, options);
});
//mongoose.connect(configDB.url,options);

require('./controller/passport')(passport); // pass passport for configuration

// set up our express application
app.use(morgan('dev')); // log every request to the console
app.use(cookieParser()); // read cookies (needed for auth)
app.use(bodyParser.json()); // get information from html forms
app.use(bodyParser.urlencoded({ extended: true }));

app.set('view engine', 'ejs'); // set up ejs for templating

// required for passport
app.use(session({ secret: 'ilovescotchscotchyscotchscotch' })); // session secret
app.use(passport.initialize());
app.use(passport.session()); // persistent login sessions
app.use(flash()); // use connect-flash for flash messages stored in session

// routes ======================================================================
require('./app/routes.js')(app, passport); // load our routes and pass in our app and fully configured passport

// launch ======================================================================
app.listen(port);
console.log('The magic happens on port ' + port);


