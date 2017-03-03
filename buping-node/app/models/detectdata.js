// load the things we need
var config = require('../../config');

var mongoose = require(config.nmpath + '/mongoose')

var db = require(config.base + '/server').db;;

// define the schema for our user model
var detectdataSchema = new mongoose.Schema({

    testdate        :   String,   //测试日期
    provider        :   String, //供应商简称
    descript        :   String, //物品描述
    ean             :   String, //物品编号
    workcurve       :   String, //工作曲线
    cr              :   String, //
    cd              :   String, //
    hg              :   String, //
    pb              :   String, //
    br              :   String, //
    resu1           :   String, //检测结果
    exceptionhandle :   String, //异常处理
    finalresult     :   String, //最终结果
    providercode    :   String, //供应商代码  !!!!!注意providercode 目前对应的品名查询条件
    nopassreason    :   String,  //不合格原因
    pinming         :   String
});

//var detectdataModel =  mongoose.model('detectdata', detectdataSchema);
module.exports =  db.model('Detectdata', detectdataSchema,'detectdatas');


// generating a hash
//detectdataModel.methods.hasEAN = function(ean) {
//        return detectdataModel.find();
//};


