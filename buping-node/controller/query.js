/**
 * Created by w.di on 2015/2/5.
 */

var config = require('../config');

var xlsx = require(config.nmpath + '/xlsx');
var moment = require(config.nmpath + '/moment');

var Detectdata = require("../app/models/detectdata");
var Provider = require("../app/models/provider");


module.exports = function(req, res,  flag)
{
    var query = {};
    if (flag != 2) {
        var ean = req.body.ean;
        var pinming = req.body.provider;
        var starttime = req.body.starttime != "" ? new Date( req.body.starttime ) : "";
        var endtime = req.body.endtime != "" ? new Date( req.body.endtime ): "";
        var month = req.body.month;

        if (ean != "") { // user search wu pin bian ma

            Detectdata.findOne({ean: ean}, function (err, detect) {
                if ( err || detect == null )
                    return res.json({success: false, data: "", Message: "不存在物品编码: " + ean});
                if (pinming != "") {
                    var qq={};
                    qq['pinming']=new RegExp(pinming);
                    Detectdata.findOne(qq, function (err, provider) {
                        if ( err || provider == null ) {
                            return res.json({success: false, data: "", Message: "不存在品名: " + pinming});
                        }
                        query = dd(month, starttime, endtime, ean, pinming, query);
                        Detectdatafind(query, res);
                    });
                }else {
                    query = dd(month, starttime, endtime, ean, pinming, query);
                    Detectdatafind(query, res);
                }
            });
        }
        else if (pinming != "") {
            Detectdata.findOne({ pinming: { $regex: pinming, $options: 'i' } }, function (err, provider) {

                if ( err || provider == null ) {
                    return res.json({success: false, data: "", Message: "不存在品名: " + pinming});
                }
                query = dd(month, starttime, endtime, ean, pinming, query);
                Detectdatafind(query, res);
            });
        }else{
            dd(month, starttime, endtime, ean, pinming, query);
            Detectdatafind(query, res);
        }

    }else{
        query.resu1 = "NO";
        Detectdatafind(query, res);
    }

}
function Detectdatafind(query, res){
    Detectdata.find(query,function(err, detects){
        if( err ){
            return handleError(err);
        }
        if( detects.length == 0 ) {
            return res.json({success:false,data:"",Message:" 没有符合条件的数据 "});
        }
        return res.json({success:true,data:detects});
    });
}
//实在是想不到合适这个函数的名字了
//query: 查询条件
//pinming: 品名
//ean: 物品编码
//functions: 将用户的查询条件转为mongodb的查询代码
function dd(month, starttime, endtime, ean, pinming, query){
    if( month != "" ){
        month = new Date(month);
        starttime = new Date(month.setDate( 1 ));
        starttime = new Date(starttime.setHours(0));
        var tem = new Date(starttime);
        endtime = new Date(tem.setMonth(tem.getMonth()+1));
        endtime.setMilliseconds(endtime.getMilliseconds()-1);
    }

    if (ean != "") {                             //若用户设定物品编号
        query.ean = ean;
    }
    if (pinming != "") {                  //若用户设定供应商代码
        query.pinming = new RegExp(pinming);
    }
    if ( starttime != "" && endtime == "" ) {                     //若用户设定开始时间
        query.testdate = { $gte : (new Date(starttime)).getTime() };
    }
    else if ( starttime == "" && endtime != "" ) {                        //若用户设定结束时间
        query.testdate = { $lte : (new Date(endtime)).getTime() };
    }
    else if ( starttime != "" && endtime != "" ) {                        //若用户设定结束时间
        query.testdate = { $gte : (new Date(starttime)).getTime(), $lte : (new Date(endtime)).getTime() };
    }
    query.resu1 = "PASS";
    return query;
}