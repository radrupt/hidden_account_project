
var dev = false;
//var dev = true;
module.exports = {

    'dev'       : dev,
	
	'port'		: 18080,

    'user'      : dev ?  '' : 'KuKG5HywAYepZFDpgTsg9yce',

    'pass'      : dev ?  '' : 'lTtz4sK8XiaFYjfzs3Vz0v5rr3zWQCyE',

    'dbname'    : dev ?  'buping' : 'EcUkFCPPmuzFWKyXsFNt',

    'dbport'    : dev ?  '8908' : '8908',

    'dbhost'    : dev ?  '127.0.0.1' : 'mongo.duapp.com',

    'dburl'     : dev ? 'mongodb://127.0.0.1:8908/buping' : 'mongodb://mongo.duapp.com:8908/EcUkFCPPmuzFWKyXsFNt',

    'nmpath'    : dev ? __dirname+'/node_modules' : __dirname+'/.bae/node_modules', //node_modules address

    'base'      : __dirname,

    'log'       : function(mess){
        if( dev )   console.log(mess);
    }
};
