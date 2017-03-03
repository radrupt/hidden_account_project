// load the things we need
var config = require('../../config');

var mongoose = require(config.nmpath + '/mongoose');

var db = require(config.base + '/server').db;

// define the schema for our user model
var providerSchema = new mongoose.Schema({

    providercode    :   String, //供应商代码
    provider        :   String //供应商全称

});

// create the model for users and expose it to our app
//module.exports = mongoose.model('Provider', providerSchema);
module.exports = db.model('Provider', providerSchema, 'providers');
