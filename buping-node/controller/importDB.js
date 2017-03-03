/**
 * Created by w.di on 2015/2/4.
 */

var config = require('../config');

var xlsx = require(config.nmpath + '/xlsx');

var DetectdataModel= require("../app/models/detectdata");
var ProviderModel = require("../app/models/provider");

function excelToDB_providercode(filepath, res, keyname, start){
    var workbook = xlsx.readFile(filepath);
    var sheet_name_list = workbook.SheetNames;
    var excel_json = "[";
    var sheetNameList = workbook.SheetNames;
    var worksheet = workbook.Sheets[sheetNameList[0]];
    var currentrow = -1;
    for (z in worksheet) {
        if(z[0] === '!') continue;
        var rowindex = z.match(/[0-9]+$/)[0];
        if( rowindex < start ) continue;
        if( currentrow != z.match(/[0-9]+$/)[0] ){
            excel_json = excel_json.replace(/,$/,"");
            if( currentrow == -1 ) excel_json += '{';
            else  excel_json += '},{';
            currentrow = rowindex;
        }
        if( keyname.hasOwnProperty(z.match(/^[A-Z]{1,2}/)[0]) ){
            excel_json += '' + keyname[z.match(/^[A-Z]{1,2}/)[0]] + ' : ' +  '"' + xlsx.utils.format_cell(worksheet[z],worksheet[z].v) + '",';
        }
    }
    excel_json = excel_json.replace(/,$/,"}]");
    var json_data = eval(excel_json);

    ProviderModel.collection.insert(json_data, function(err, docs){
        if (err) {
            return res.json({success:false});
        } else {
            console.info('%d data were successfully stored.', docs.length);
            return res.json({success:true});
        }
    });

}
//start: excel row start index
function excelToDB(filepath, res, keyname, start){
    var workbook = xlsx.readFile(filepath);
    var sheet_name_list = workbook.SheetNames;
    var excel_json = "[";
    var sheetNameList = workbook.SheetNames;
    var worksheet = workbook.Sheets[sheetNameList[0]];

    var currentrow = -1;// excel row index
    for (z in worksheet) {
        if(z[0] === '!') continue;
        var rowindex = z.match(/[0-9]+$/)[0];
        if( rowindex < start ) continue;
        if( currentrow != z.match(/[0-9]+$/)[0] ){//judge wheather jump into next row
            excel_json = excel_json.replace(/,$/,"");
            if( currentrow == -1 ) excel_json += '{';
            else  excel_json += '},{';
            currentrow = rowindex;
        }
        if( keyname.hasOwnProperty(z.match(/^[A-Z]{1,2}/)[0]) ){
            excel_json += '' + keyname[z.match(/^[A-Z]{1,2}/)[0]] + ' : ' +  '"' + xlsx.utils.format_cell(worksheet[z],worksheet[z].v) + '",';
        }
    }
    excel_json = excel_json.replace(/,$/,"}]");
    var json_data = eval(excel_json);
    var data = '[';
    ProviderModel.find(function(err,providers){
        if( err ) return handleError(err);
        if( providers == null ) res.json({success:false, errorstate:("不存在合格的供应商"+splitinfo[0])});
        json_data.forEach(function(row){
            var splitinfo = row["provider_descript"].split("-");
            var providercode = getProviderCode(providers,splitinfo[0] );
            var cr = row.hasOwnProperty("cr") ? row["cr"] : "";
            var cd = row.hasOwnProperty("cd") ? row["cd"] : "";
            var hg = row.hasOwnProperty("hg") ? row["hg"] : "";
            var pb = row.hasOwnProperty("pb") ? row["pb"] : "";
            var br = row.hasOwnProperty("br") ? row["br"] : "";
            var solve = judgeResu1( row["workcurve"],cr, cd, hg, pb, br);
            var resu1 = solve.result;
            var nopassreason = solve.reason;
            var testtime = new Date(row["testdate"]);
            var batchnumber = "";
            var exceptionhandle = "";
            var finalresult = "";

            data +='{ testdate : "' + testtime.getTime() + '", provider : "' + splitinfo[0] + '", descript : "' + splitinfo[1] +  '", ean : "' + row["ean"]
            + '", workcurve : "' + row["workcurve"] + '", cr : "' + cr + '", cd : "' + cd + '", hg : "' + hg
            + '", pb : "' + pb +  '", br : "' + br + '", resu1 : "' + resu1 + '", batchnumber : "' + batchnumber
            + '", exceptionhandle : "' + exceptionhandle + '", finalresult : "' + finalresult + '", pinming : "' + row["provider_descript"] + '", nopassreason : "' + nopassreason + '", providercode : "' + providercode + '"},';
        });

        data = data.replace(/,$/,"]");

        json_data = eval(data);
        DetectdataModel.findOne({ean : json_data[0].ean }, function(err,detect){
            if ( err )
                return res.json({success: false, data: "", Message: err});
            if( detect ) {
                json_data[0].testdate= detect.testdate;
                json_data[0].pinming= detect.pinming;
                json_data[0].workcurve= detect.workcurve;
                json_data[0].cr= detect.cr;
                json_data[0].cd= detect.cd;
                json_data[0].hg= detect.hg;
                json_data[0].br= detect.br;
                json_data[0].pb= detect.pb;

                return res.json({success: false, data: "", Message: "excel表里的数据已经导入过了"});
            }
            else{
                DetectdataModel.collection.insert(json_data, function(err, detect){
                    if (err) {
                        res.json({success:false});
                    } else {
                        console.info('%d data were successfully stored.', detect.length);
                        res.json({success:true});
                    }
                });
            }

        });



    });
}

function getProviderCode(providers, providername){
    for( var i = 0; i < providers.length; i++  ){
        if( (providers[i].provider).match(providername) != null ){
            return providers[i].providercode;
        }
    }
}

//由于对于不同类型的材料标准不一样
//判断材料是否合格
function judgeResu1(workcurve, cr, cd, hg, pb, br){
    var workcurves = {"HSM1000" : "Metal","BRASS-ZINC" : "Metal","FeCr" : "Metal","Fe" : "Metal","SOLDER" : "Metal","HSP1000" : "Nonmetallic","PE" : "Nonmetallic","PVC" : "Nonmetallic"};
    var result, reason="";
    if( workcurves[workcurve] == "Metal" ){//金属
        if( cr != "ND" && cr != "" && cr >=75 ) {
            result = "NO";
            reason += "铬: " + cr + ". ";
        }
        if( cd != "ND" && cd != "" && cd >=100 ) {
            result = "NO";
            reason += "镉: " + cd + ". ";
        }
        if( pb != "ND" && pb != "" && pb >=500 ) {
            result = "NO";
            reason += "铅: " + pb + ". ";
        }
    }else if( workcurves[workcurve] == "Nonmetallic" ){//fei金属
        if( cr != "ND" && cr != "" && cr >=40 ) {
            result = "NO";
            reason += "铬: " + cr + ". ";
        }
        if( cd != "ND" && cd != "" && cd >=200 ) {
            result = "NO";
            reason += "镉: " + cd + ". ";
        }
        if( pb != "ND" && pb != "" && pb >=200 ) {
            result = "NO";
            reason += "铅: " + pb + ". ";
        }
    }else{
        throw workcurve +" workcurve 不存在" ;
    }
    if( hg != "ND" && hg != "" && hg >=200 ) {
        result = "NO";
        reason += "汞: " + hg + ". ";
    }
    if( br != "ND" && br != "" && br >=900 ) {
        result = "NO";
        reason += "溴: " + br + ". ";
    }
    if( result != "NO" ) {
        reason = "";
        result = "PASS";
    }
    return {"reason":reason, "result":result};
}


module.exports = function(files, res){
    var keyname ;
    if( files.upload.name.match(/精工/) != null ) {

        keyname = {"D" : "workcurve", "H" : "testdate", "I" : "provider_descript", "J" : "ean", "T" : "cd", "AB" : "pb", "AJ" : "hg", "AR" : "br", "AZ" : "cr"};
        excelToDB(files.upload.path, res, keyname, 4);

    }
    else if( files.upload.name.match(/天瑞/) != null ) {

        keyname = {"C" : "workcurve", "B" : "testdate", "A" : "provider_descript", "D" : "ean", "G" : "cd", "I" : "pb", "H" : "hg", "J" : "br", "F" : "cr"};
        excelToDB(files.upload.path, res, keyname, 2);

    }else if( files.upload.name.match(/ODM合格供方名单/) != null ){

        keyname = {'B':'providercode', 'C':'provider'};

        excelToDB_providercode(files.upload.path, res, keyname, 2);

    }
}
