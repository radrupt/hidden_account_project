/**
 * Created by wangd933 on 15-3-11.
 */
var winc = 1;
//1. 从服务器获取货主的货物相关信息
var info1 =  {"status":"货品运输中","id":"LK1503020043","sender":{"province":"上海","city":"上海","district":"闸北","address":"秣陵路303号"},"receiver":{"province":"北京","city":"北京","district":"东城","address":"南京路6号"},"orderstatus":[{"id":"843","orderid":"207","status":"1","update_time":"2015-03-05 10:26:05","update_accountid":"6","isshow":"1","note":"订单已提交，等待受理。"},{"id":"844","orderid":"207","status":"2","update_time":"2015-03-05 10:26:33","update_accountid":"35","isshow":"1","note":"订单已受理，等待安排司机。"},{"id":"845","orderid":"207","status":"2","update_time":"2015-03-05 10:26:33","update_accountid":"35","isshow":"2","note":"订单已受理，正在做运输计划。"},{"id":"846","orderid":"207","status":"3","update_time":"2015-03-05 10:27:33","update_accountid":"35","isshow":"2","note":"运输计划已完成，等待安排司机。"},{"id":"908","orderid":"207","status":"4","update_time":"2015-03-06 13:37:50","update_accountid":"35","isshow":"1","note":"已安排1位司机来承运订单。"},{"id":"909","orderid":"207","status":"4","update_time":"2015-03-06 13:37:56","update_accountid":"35","isshow":"1","note":"已安排1位司机来承运订单。"},{"id":"910","orderid":"207","status":"4","update_time":"2015-03-06 13:38:14","update_accountid":"35","isshow":"1","note":"已安排1位司机来承运订单。"},{"id":"911","orderid":"207","status":"5","update_time":"2015-03-06 13:39:53","update_accountid":"78","isshow":"2","note":"货品已经开始运输了。"}],"waybill":[{"id":"221","orderid":"207","print_orderid":"LK1503020043","print_waybillid":"LK1503020043_0","status":"3","create_time":"2015-03-05 10:27:17","update_time":"2015-03-06 13:39:53","update_accountid":"22","product_type":"1","require_truck_length":"29","truck_number":"1","freight":"8000","is_buy_insurance":"1","total_value":"100000","insurance_amount":"100","is_bidding":"1","bidding_time":"2015-03-05 11:00:00","accountid":"78","trucker_name":"刘骥","trucker_mobile":"18621820303","truckno":"新SS4561","truck_length":"0","goods_brief":"","sender_name":"杨柳","sender_mobile":"13562587895","sender_province":"上海","sender_city":"上海","sender_district":"闸北","sender_provinceid":"310000","sender_cityid":"310100","sender_districtid":"310108","sender_address":"秣陵路303号","sender_company":"办事处","require_sendtime":"2015-03-06 15:30:00","actual_sendtime":"2015-03-06 13:39:53","receiver_name":"王小波2","receiver_mobile":"13854265869","receiver_province":"北京","receiver_city":"北京","receiver_district":"东城","receiver_provinceid":"110000","receiver_cityid":"110100","receiver_districtid":"110101","receiver_address":"南京路6号","receiver_company":"","require_arrivetime":"2015-03-09 18:54:00","actual_arrivetime":"0000-00-00 00:00:00","bankid":"0","amount_payable":"8000","amount_paid":"0","is_abnormal":"0","rating":"0","rating_note":"","ref_tempid":"0","total_weight":"25000","total_volume":"62.5","is_temp":"1","location":{"time":"2015-03-06 14:01:04","latitude":"31.165402","longitude":"121.138779"},"xs":[{"time":"2015-03-06 14:01:04","latitude":"31.165402","longitude":"121.138779"},{"time":"2015-03-06 13:59:51","latitude":"31.165402","longitude":"121.138779"},{"time":"2015-03-06 13:57:24","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:52:12","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:49:43","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:47:22","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:45:27","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:42:33","latitude":"31.153622","longitude":"121.121005"},{"time":"2015-03-06 13:41:32","latitude":"31.153622","longitude":"121.121005"},{"time":"2015-03-06 13:40:32","latitude":"31.153622","longitude":"121.121005"}]}]};
//2. 根据用户输入的地理位置信息计算得到始发地和目的地的地理坐标, 路线中点的地理坐标,
computeSenderAndReceiverPOI(info1, main);

//绘制最基本的地图信息
function main(info, senderP, receiverP){

    var map = new BMap.Map('container');        // container: html 页面里的地图容器id名
    map.enableScrollWheelZoom(true);            // 开启滚轮缩放
    map.setViewport([senderP, receiverP], {     // 根据需要显示的点，地图自动调整缩放等级和显示位置
        margins: [40, 10, 10, 10]
    });

    var drawRoute = new DrawRoute(map, senderP, receiverP);         // 用于绘制线路的对象

    var points = [{"time":"2015-03-06 14:01:04","latitude":"31.165402","longitude":"121.138779"},{"time":"2015-03-06 13:59:51","latitude":"31.165402","longitude":"121.138779"},{"time":"2015-03-06 13:57:24","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:52:12","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:49:43","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:47:22","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:45:27","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:42:33","latitude":"31.153622","longitude":"121.121005"},{"time":"2015-03-06 13:41:32","latitude":"31.153622","longitude":"121.121005"},{"time":"2015-03-06 13:40:32","latitude":"31.153622","longitude":"121.121005"}];

    drawRoute.draw(points, new InfoBox(map, 0, {placeinfo: "sd", updatetime: "sd"}));

    drawLeftControl(map, senderP, receiverP, info, 1);        //绘制地图界面的左侧自定义控件

    map.addControl(new BMap.NavigationControl({anchor:BMAP_ANCHOR_TOP_RIGHT}));
    map.addControl(new BMap.ScaleControl({anchor:BMAP_ANCHOR_BOTTOM_RIGHT}));

}


//绘制左侧窗体控件
//flag: 0: 只显示订单
//flag: 1: 显示订单和车辆信息
function drawLeftControl(map, senderP, receiverP, info, flag){
    //先实现flag == 1
    // 创建控件
    var myZoomCtrl = new ZoomControl(info);
    // 添加到地图当中,会执行initialize
    map.addControl(myZoomCtrl);

}




/*******左侧窗体控件构造 start********/

//控件构造
//左侧黑窗控件
function ZoomControl(info) {
    this.defaultAnchor = BMAP_ANCHOR_TOP_LEFT;
    this.defaultOffset = new BMap.Size(0, 0);
    this.info = info;
    this.maindiv = "";

}
ZoomControl.prototype = new BMap.Control();
ZoomControl.prototype.initialize = function (map) {

    var Tab = function () {
        var ordercontent;
        var truckcontent;
        var flex;
        this.makeContent = function (info) {
            this.ordercontent = '<div>' + getOrderContent(info) + '</div>';
            this.truckcontent = getTruckContent(info);
            ordercontent = this.ordercontent;
            truckcontent = this.truckcontent;
            this.content = '<div class="content" id="content"><div class="contentorder" id="contentorder">' +
                '<div style="height: 60px;width: 100%;position: relative"><span style="position: absolute;top:20px;left:40px">订单详情</span></div>' +
                this.ordercontent +
            '</div>'+'<div class="contenttruck" id="contenttruck" style="display: none;">' +
            this.truckcontent +
            '</div></div>';

            return tab + this.content + solidunflod;
        }

        function setContent(content) {
            document.getElementById('content').innerHTML = content;
        }

        //使用前必须map.getContainer().appendChild(main);
        this.addListener = function () {
            var maindivstyle = document.querySelector("#io_main").style;
            var tabdivstyle = document.querySelector("#tab").style;
            var contentdivstyle = document.querySelector("#content").style;
            var solidunfloddiv = document.getElementById("solidunflod");

            var flag = 0;
            change(-1);
            flex.init();

            solidunfloddiv.addEventListener("click", function (e) {


                if (!flag) {
                    maindivstyle.width = "20px";
                    tabdivstyle.height = "0px";
                    contentdivstyle.width = "0px";
                    contentdivstyle.display = "none";
                    solidunfloddiv.setAttribute("src", "./images/ico/pax.png");
                    flag = 1;
                } else {
                    maindivstyle.width = "300px";
                    tabdivstyle.height = "80px";
                    contentdivstyle.width = "100%";
                    contentdivstyle.display = "block";
                    solidunfloddiv.setAttribute("src", "./images/ico/plus.png");
                    flag = 0;
                }
            });
            var orderstatustab = document.getElementById('orderstatustab');
            orderstatustab.addEventListener('click', function () {
                change(0);
            });
            var trucktab = document.getElementById('trucktab');
            trucktab.addEventListener('click', function () {
                change(1);
            });
            //-1 : init, 0 and 1: switch
            function change(index) {
                if (index == 0 || index == -1) {
                    var orderimga = $('.orderimg.a');
                    orderimga.addClass('active');
                    var orderimgb = $('.orderimg.b');
                    orderimgb.removeClass('active');
                    var truckimga = $('.truckimg.a');
                    truckimga.removeClass('active');
                    var truckimgb = $('.truckimg.b');
                    truckimgb.addClass('active');
                    var tab = $('#tab');
                    tab.removeClass('tabtruck');
                    tab.addClass('taborder');
                    document.querySelector("#contenttruck").style.display = "none";
                    document.querySelector("#contentorder").style.display = "inline";
                    //setContent(ordercontent);
                } else if (index == 1) {
                    var orderimga = $('.orderimg.a');
                    orderimga.removeClass('active');
                    var orderimgb = $('.orderimg.b');
                    orderimgb.addClass('active');
                    var truckimga = $('.truckimg.a');
                    truckimga.addClass('active');
                    var truckimgb = $('.truckimg.b');
                    truckimgb.removeClass('active');
                    var tab = $('#tab');
                    tab.removeClass('taborder');
                    tab.addClass('tabtruck');
                    document.querySelector("#contentorder").style.display = "none";
                    document.querySelector("#contenttruck").style.display = "inline";
                    //setContent(truckcontent);
                }
            }
        }

        //动态部分
        function getOrderContent(info) {


            var info1 =  info;
            var info2 = eval('(' + JSON.stringify(info) + ')');
            info2.id = "LK1503020044";
            console.log(info.id);
            //暂时先模拟多个货单数据，由于服务器灭有传多个数据
            var infos = [info1, info2];

            flex = new Flex(infos);
            return flex.getFlexDiv();


        }

        function getTruckContent(info) {
            //运单收集
            var waybilllist = info.waybill;
            var addb = '';
            var addb = '<div style="text-align: center">' + "<span class='shiplist dark' style='height: 70px;line-height: 70px;'>" +
                '当前已安排 ' + waybilllist.length + ' 位司机运送' +
                "</span></div>" + addb;
            for (var i = 0; i < waybilllist.length; i++) {
                addb = addb +
                "<div class='carlink' cid=" + waybilllist[i]['id'] + ">" +
                "<img src='" + "./images/ico/b-w.png" + "' style='width:30px;' class='l_title left'>" +
                '<span class="r_driver">' +
                waybilllist[i]['truckno'] +
                "</span>" +
                "<div class='clearfix'></div>" +
                "</div>";
            }

            return addb;

        }

        //静态部分
        //选项卡
        var tab = '' +
            '<div class="taborder" id="tab">' +
            '<div class="orderstatustab" id="orderstatustab">' +
            '<span class="orderimg a">' +
            '<img class="" src="./images/ico/a-o.png">' +
            '</span>' +
            '<span class="orderimg b">' +
            '<img class="" src="./images/ico/a-w.png">' +
            '</span>' +
            '</div>' +
            '<div class="trucktab" id="trucktab">' +
            '<span class="truckimg a">' +
            '<img class="" src="./images/ico/b-o.png">' +
            '</span>' +
            '<span class="truckimg b">' +
            '<img class="" src="./images/ico/b-w.png">' +
            '</span>' +
            '</div>' +
            '</div>';
        //箭头区域
        var solidunflod = '' +
            '<img class="solidunflod" id="solidunflod" src="./images/ico/plus.png">' +

            '</img>';

    }

    var tab = new Tab();

    var main = makediv('io_main', tab.makeContent(this.info));

    // 添加DOM元素到地图中
    map.getContainer().appendChild(main);

    tab.addListener();

}

function makediv(classname,text){
    var div = document.createElement("div");
    div.setAttribute("class", classname);
    div.setAttribute("id", classname);
    div.innerHTML=(text);
    return div;
}
/*******左侧窗体控件构造 end********/

//lists: title, content
var Flex = function (infos) {
    var listids = new Array();//存储下拉列表的id
    //滑动框html
    this.getFlexDiv = function () {
        var flexdiv = '';

        for (var i = 0; i < infos.length; i++) {

            var info = infos[i];
            var id = info.id;
            flexdiv += '' +
            '<div class="orderlist"' + ' id="list' + id + '">' +
                '<div class="ordertitle" id="listtitle' + id + '">' +
                    '<img class="listtitleimg" id="listtitleimg' + id + '" src="./images/ico/line_p_2.png" >' +
                '</div>' +
                '<div id="listcontent' + id +'" style="color:white;width:300px;height:0px;overflow:hidden">'  +
                getOrderContent(info) +
                '</div>' +
            '</div>';
            listids.push("list" + id);
        }
        return flexdiv;
    }

    function getOrderContent(info){

        var stlist = info.orderstatus;
        var adda = '';
        for (var i = 0; i < stlist.length; i++) {
            var pad = "<img src='./images/ico/line_p_2.png' class='left'>";
            if (i == 0) {
                pad = "<img src='./images/ico/line_p_3.png' class='left'>"
            }
            ;
            if (i == (stlist.length - 1)) {
                pad = "<img src='./images/ico/line_p_1.png' class='left'>"
            }
            ;
            adda = "" +
            "<div class='shiplist'>" +
            pad + "<div style='padding-left: 35px;padding-top: 16px;line-height: 1.6;'>" +
            stlist[i]['note'] +
            "<br/>" +
            stlist[i]['update_time'] +
            "</div>" + "<div class='clearfix'></div>" +
            "</div>" + adda;
        }

        return adda;
    }
    //get dom
    function lkGetElementById(lists) {
        var elementlists = new Array();
        for (var i = 0; i < lists.length; i++) {
            elementlists[i] = document.getElementById(lists[i]);
        }
        return elementlists;
    }

    //lists: doms
    //lists: 所有订单号
    this.init = function (orderstatuscontenthtml) {
        var lists = lkGetElementById(listids);
        var currentunfold = null;
        for (var i = 0; i < lists.length; i++) {
            var list = lists[i];
            list.lkindex = i;
            list.contentheight = 300;
            var marks = list.id.replace("list", "");//车牌号
            list.contenthtml = orderstatuscontenthtml;

            list.innerHTML += list.contenthtml;
            document.getElementById("list"+ marks).innerHTML = document.getElementById("list"+ marks).innerHTML.replace("undefined","");
            list.contentstyle = document.getElementById(("listcontent"+marks)).style;
            list.contentstyle.transition = "1s";

            list.lkflag = 0; // shrink, 1: unfold
            //css init
            var style = list.style;
            style.width = '100%';
            style.height = '60px'
            style.marginBottom = '2px';
            style.transition = "1s";

            list.lktitle = document.getElementById("listtitleimg" + marks);

            list.lktitle.parent = list;

            list.lktitle.addEventListener("click", function () {
                if (this.parent.lkflag == 0) {//点击项未展开
                    if (currentunfold) {//若当前已有展开的项,则先关闭
                        currentunfold.contentstyle.height = "0px";
                        this.parent.shrink(currentunfold);

                        currentunfold.lkflag = 0;
                    }
                    this.parent.unfold(); // 展开用户点击的项
                    this.parent.contentstyle.height = "300px";
                    currentunfold = this.parent; // 更新当前展开项
                    this.parent.lkflag = 1;// 记录当前项已展开
                } else {//如果当前项已展开
                    this.parent.contentstyle.height = "0px";
                    this.parent.shrink(this.parent);//收缩当前项,即currentunfold == this
                    currentunfold = null;//当前无展开项
                    this.parent.lkflag = 0;//记录当前项已收缩
                }
            })

            list.shrink = function (current) { //收缩
                for (var i = current.lkindex + 1; i < lists.length; i++) {
                    lists[i].style.transform = "translate(0px, 0px)";
                }
            }

            list.unfold = function () { // 展开
                for (var i = this.lkindex + 1; i < lists.length; i++) {
                    lists[i].style.transform = "translate(0px," + this.contentheight + "px)";
                }
            };
        }
    }

}

//绘制路线的类
// 路线的功能：鼠标触摸会变色，且会弹出提示框
var DrawRoute = function(map, senderP, receiverP){

    //绘制路线函数
    //params: 路线上的任意个点(>2)(type: BMap.Point)

    var infoBox = null;

    var wayoption = null;

    var defaultcolor = "#9fa0a0";
    var overcolor = "#39bbff";

    this.setDefaultColor = function(color){ defaultcolor = color; }
    this.setOverColor = function(color){ overcolor = color; }

    this.draw = function(points, infobox){

        infoBox = infobox;

        if( points.length < 2 ){
            console.log("路线输入点数量小于2个");
        }
        points = wayPoints(points);             //  将后台数据处理为做BMap.Point类型

        driving.search(receiverP, senderP, {waypoints : points });

    }

    //清除已绘制的线
    this.clear = function(){
        if( wayoption )   map.removeOverlay(wayoption);
        wayoption = null;
    }

    var driving = new BMap.DrivingRoute(map, {
        onSearchComplete: function (results) {
            if (driving.getStatus() == BMAP_STATUS_SUCCESS) {
                // 地图覆盖物
                addOverlays(results);

            }
        }
    });

    // 添加覆盖物并设置视野
    function addOverlays(results) {

        // 获取方案
        var plan = results.getPlan(0);
        // 获取方案中包含的路线
        addRoute(plan.getRoute(0).getPath());

    }

    // 添加路线
    function addRoute(path) {
        var marker;
        BMap.Polyline.prototype.addEventListener("mouseover", function (e) {

            if( infoBox )   infoBox.open(e.point);

            if( wayoption )   wayoption.setStrokeColor(overcolor);
        });
        BMap.Polyline.prototype.addEventListener("mouseout", function (e) {

            if( infoBox )   infoBox.close();
            if( wayoption )   wayoption.setStrokeColor(defaultcolor);
        });
        wayoption = new BMap.Polyline(path, {//路线参数
            strokeColor: defaultcolor,
            enableClicking: false
        });

        map.addOverlay(wayoption);
    }

    /*private
     *由于同样的起点终点，司机可能跑的是不同的路线，
     * 所以服务器在保存司机路线的时候，
     * 是在该司机对应路线上取有限点来描述司机路线的
     * 但是百度search的时候输入数据最多只能是10个，
     * 所以需要处理路线点的数量
     */
    function wayPoints(wpoints){//导航路线上均匀分布的点

        var routed = wpoints;
        if(routed.length>0) {
            var xss = [];
            if (routed.length > 10) {
//绘制左侧窗体控件
//flag: 0: 只显示订单
//flag: 1: 显示订单和车辆信息
                function drawLeftControl(map, senderP, receiverP, info, flag){
                    //先实现flag == 1
                    // 创建控件
                    var myZoomCtrl = new ZoomControl();
                    // 添加到地图当中
                    map.addControl(myZoomCtrl);

                    change();
                }
                var x = routed.length / 10;
                xss[0] = routed[0];
                for (var i = 1; i < 9; i++) {
                    xss[i] = routed[Math.floor(x * i)];
                }
                xss[9] = routed[routed.length - 1];
            } else {
                xss = routed;
            }
            //xss->构造路径数组
            xss = redo(xss);
            return xss;
        }else
            return null;

        function redo(arr){
            var result = [];
            for(var i=0;i<arr.length;i++){
                result[i]=(new BMap.Point(arr[i]['longitude'],arr[i]['latitude']));
            }
            return result;
        }
    }

}

/******InfoBox start********/

/*信息提示框模板
 *类别:0: 不透明，箭头在左边, 1: 不透明，箭头在下面, 2: 半透明
 * options:  cars: 运输车辆信息,array
 *           isstart: 标志该提示框是否对应起点
 *           isstop:  标志该提示框是否对应终点
 *           truckinfo: 司机信息
 *           placeinfo: 位置信息
 *           updatetime:司机位置最新更新时间
 * */
var InfoBox = function(map, flag, options){

    var html = '';
    if( flag == 0 ) html = this.htmltem1(options.placeinfo, options.updatetime);
    if( flag == 1 ) html = this.htmltem2(options.cars);
    if( flag == 2 ) html = this.htmltem3(options.company, options.placeinfo, options.truckinfo);

    this.infobox = new BMapLib.InfoBox(map,html);
}

InfoBox.prototype.htmltem1 = function(placeinfo, updatetime){
    return '<div class="infoboxcss1"></div>';
}

InfoBox.prototype.htmltem2 = function(cars){
    return '<div class="infoboxcss2"></div>';
}

//trunkinfo: 司机信息：name姓名，mobilenumber号码
//flag: 0: start起点, 1: end终点
InfoBox.prototype.htmltem3 = function(company, palceinfo, truckinfo){
    return '<div class="infoboxcss3"></div>';
}

InfoBox.prototype.open = function(point){
    this.infobox.open(point);
}

InfoBox.prototype.close = function(){
    this.infobox.hide();
    this.infobox.close();//直接close无效,得先hide
}

/******InfoBox end********/

/*name: computeSenderAndReceiverPOI
 * function: 根据用户输入的地理位置信息计算得到始发地和目的地的地理坐标, 路线中点的地理坐标,
 * params: info-->服务器获取的货主信息，包括历史当前订单信息，运输车辆信息
 *         callback-->回调函数,当计算好坐标后调用
 * */
function computeSenderAndReceiverPOI(info, callback){

    if( !info ) {
        console.log("info: 无有效信息");
        return;
    }

    var senderR= info.sender.province+info.sender.city+info.sender.district+info.sender.address;
    var receiverR= info.receiver.province+info.receiver.city+info.receiver.district+info.receiver.address;
    var senderP = null;
    var receiverP = null;
    var midP = null;

    var senderPGeocoder = new BMap.Geocoder();
    senderPGeocoder.getPoint(senderR, function(point){
        if (point) {
            senderP=point;
            xboot();
        }else{
            alert("您选择地址没有解析到结果!");
        }
    }, info.sender.province);

    var receiverPGeocoder = new BMap.Geocoder();
    receiverPGeocoder.getPoint(receiverR, function(point){
        if (point) {
            receiverP=point;
            xboot();
        }else{
            alert("您选择地址没有解析到结果!");
        }
    }, info.receiver.province);

    function xboot(){ //如果起点，终点坐标计算好了，则开始绘制地图
        if(receiverP && senderP){
            callback(info, senderP, receiverP);

         }
    }
}
