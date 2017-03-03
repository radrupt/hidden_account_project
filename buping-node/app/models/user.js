// load the things we need

var config = require('../../config');

var mongoose = require(config.nmpath + '/mongoose');

var db = require('../../server').db;

var bcrypt   = require(config.nmpath + '/bcrypt-nodejs');

// define the schema for our user model
var userSchema = mongoose.Schema({
        email        : String,
        password     : String
});

// generating a hash
userSchema.methods.generateHash = function(password) {
    return bcrypt.hashSync(password, bcrypt.genSaltSync(8), null);
};

// checking if password is valid
userSchema.methods.validPassword = function(password) {
    return bcrypt.compareSync(password, this.password);
};

// create the model for users and expose it to our app
//module.exports = mongoose.model('User', userSchema);
//console.log(config.base + '/server');
module.exports = db.model('User', userSchema, 'users')

