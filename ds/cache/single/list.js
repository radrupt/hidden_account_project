/**
 * Created by wangd on 2016/2/29.
 */
var redis = require('../../redis').single;

var array2Object = function(row,keys){
    var array = [];
    for(var i = 0; i < row.length; i+=keys.length){
        var o = {};
        for(var k = 0; k < keys.length;k++){
            o[keys[k]] = row[i+k];
        }
        array.push(o);
    }
    return array;
}

var list = function(obj){//sortkey,page,pagesize
    return new Promise(function(resolve,reject){
        var sortkey = '';
        var asc_desc = obj.asc && 'asc' || 'desc';
        if(!obj.sortkey) sortkey = 'sid';
        else if(obj.sortkey == 'hot') sortkey = 'period';
        else if(obj.sortkey == 'new') sortkey = 'starttime';
        else if(obj.sortkey == 'progress') sortkey = 'percent';///percent字段是写在视图里的
        obj.page = obj.page ||1;
        obj.pagesize = obj.pagesize ||15;

        redis.sort('single_id','by','single_*->'+sortkey,asc_desc,
            'get','single_*->sid',
            'get','single_*->name',
            'get','single_*->smallimage',
            'get','single_*->bigimage',
            'get','single_*->type',
            'get','single_*->tag',
            'get','single_*->price',
            'get','single_*->gid',
            'get','single_*->period',
            'get','single_*->kind',
            'get','single_*->starttime',
            'get','single_*->endtime',
            'get','single_*->restinvolve',
            'get','single_*->state',
            'get','single_*->percent',
            'limit',(obj.page-1)*obj.pagesize,obj.pagesize,
            function(err,res){
                if(err) return reject(err);
                var result = array2Object(res,['sid','name','smallimage','bigimage','type','tag','price','gid','period','kind','starttime','endtime','restinvolve','state','percent']);
                resolve(result);
            })

    })
}

module.exports = list;