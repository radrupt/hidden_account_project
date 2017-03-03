/**
 * Created by wangd on 2016/2/15.
 */

require('../redis');//preload redis sever
require('../lib/sms');//preload sms server
require('../lib/qiniu');//preload qiniu server
require('../scripts');//load scripts,hotupdate:update--update--scripts,

require('../init');//init data

require('../server/app');//start app main server
require('../server/pay');//start app pay server