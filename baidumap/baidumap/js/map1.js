/**
 * Created by wangd933 on 15-3-15.
 */
/**
 * Created by wangd933 on 15-3-11.
 */
var winc = 1;
//1. 模拟从服务器获取货主的货物相关信息
var info1 =  {"status":"货品运输中","id":"LK1503020043","sender":{"province":"上海","city":"上海","district":"闸北","address":"秣陵路303号"},"receiver":{"province":"北京","city":"北京","district":"东城","address":"南京路6号"},"orderstatus":[{"id":"843","orderid":"207","status":"1","update_time":"2015-03-05 10:26:05","update_accountid":"6","isshow":"1","note":"订单已提交，等待受理。"},{"id":"844","orderid":"207","status":"2","update_time":"2015-03-05 10:26:33","update_accountid":"35","isshow":"1","note":"订单已受理，等待安排司机。"},{"id":"845","orderid":"207","status":"2","update_time":"2015-03-05 10:26:33","update_accountid":"35","isshow":"2","note":"订单已受理，正在做运输计划。"},{"id":"846","orderid":"207","status":"3","update_time":"2015-03-05 10:27:33","update_accountid":"35","isshow":"2","note":"运输计划已完成，等待安排司机。"},{"id":"908","orderid":"207","status":"4","update_time":"2015-03-06 13:37:50","update_accountid":"35","isshow":"1","note":"已安排1位司机来承运订单。"},{"id":"909","orderid":"207","status":"4","update_time":"2015-03-06 13:37:56","update_accountid":"35","isshow":"1","note":"已安排1位司机来承运订单。"},{"id":"910","orderid":"207","status":"4","update_time":"2015-03-06 13:38:14","update_accountid":"35","isshow":"1","note":"已安排1位司机来承运订单。"},{"id":"911","orderid":"207","status":"5","update_time":"2015-03-06 13:39:53","update_accountid":"78","isshow":"2","note":"货品已经开始运输了。"},{ id: "1063", orderid: "207", status: "6", update_time: "2015-03-11 16:11:11", update_accountid: "78", isshow: "1", note: "货品已全部到达并签收，订单尚未支付，请及时付款。" }],"waybill":[{"id":"221","orderid":"207","print_orderid":"LK1503020043","print_waybillid":"LK1503020043_0","status":"3","create_time":"2015-03-05 10:27:17","update_time":"2015-03-06 13:39:53","update_accountid":"22","product_type":"1","require_truck_length":"29","truck_number":"1","freight":"8000","is_buy_insurance":"1","total_value":"100000","insurance_amount":"100","is_bidding":"1","bidding_time":"2015-03-05 11:00:00","accountid":"78","trucker_name":"刘骥","trucker_mobile":"18621820303","truckno":"新SS4561","truck_length":"0","goods_brief":"","sender_name":"杨柳","sender_mobile":"13562587895","sender_province":"上海","sender_city":"上海","sender_district":"闸北","sender_provinceid":"310000","sender_cityid":"310100","sender_districtid":"310108","sender_address":"秣陵路303号","sender_company":"办事处","require_sendtime":"2015-03-06 15:30:00","actual_sendtime":"2015-03-06 13:39:53","receiver_name":"王小波2","receiver_mobile":"13854265869","receiver_province":"北京","receiver_city":"北京","receiver_district":"东城","receiver_provinceid":"110000","receiver_cityid":"110100","receiver_districtid":"110101","receiver_address":"南京路6号","receiver_company":"","require_arrivetime":"2015-03-09 18:54:00","actual_arrivetime":"0000-00-00 00:00:00","bankid":"0","amount_payable":"8000","amount_paid":"0","is_abnormal":"0","rating":"0","rating_note":"","ref_tempid":"0","total_weight":"25000","total_volume":"62.5","is_temp":"1","location":{"time":"2015-03-06 14:01:04","latitude":"31.165402","longitude":"121.138779"},"xs":[{"time":"2015-03-06 14:01:04","latitude":"31.165402","longitude":"121.138779"},{"time":"2015-03-06 13:59:51","latitude":"31.165402","longitude":"121.138779"},{"time":"2015-03-06 13:57:24","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:52:12","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:49:43","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:47:22","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:45:27","latitude":"31.15441","longitude":"121.124753"},{"time":"2015-03-06 13:42:33","latitude":"31.153622","longitude":"121.121005"},{"time":"2015-03-06 13:41:32","latitude":"31.153622","longitude":"121.121005"},{"time":"2015-03-06 13:40:32","latitude":"31.153622","longitude":"121.121005"}]}]};
var info2 = eval('(' + JSON.stringify(info1) + ')');
info2.id = "LK1503020044";
info2.waybill[0].truckno = "新SS6666";
//2. 根据用户输入的地理位置信息计算得到始发地和目的地的地理坐标, 路线中点的地理坐标,
//绘制路线的类
// 路线的功能：鼠标触摸会变色，且会弹出提示框
var DrawRoute = function(map, info){

    //绘制路线函数
    //params: 路线上的任意个点(>2)(type: BMap.Point)

    var truckinfoBox = null;

    var wayoption = null;

    var routemakers = new Array();

    var defaultcolor = "#9fa0a0";
    var overcolor = "#39bbff";

    var highlightwaybill = null;

    this.setDefaultColor = function(color){ defaultcolor = color; }
    this.setOverColor = function(color){ overcolor = color; }

    //truckid == -1 表示显示所有火车路线（处于送货，未到达状态是）
    //否则，高亮显示truckid路线
    this.draw = function(truckid){
        highlighttruckid = truckid;
        computeSenderAndReceiverPOI(info, function(senderP, receiverP) {
            //linshi
            var dd = info.waybill[0];
            var trucklocal = null;
            var senderinfoBox = new InfoBox(map, 2, {company: dd.sender_company,
                placeinfo: (dd.sender_province+","+dd.sender_city+","+dd.sender_district+","+
                dd.sender_address), namemobile: {name: dd.sender_name, mobile:dd.sender_mobile}, startorend: 0})//start
            var receiverinfoBox = new InfoBox(map, 2, {company: dd.receiver_company,
                placeinfo: (dd.receiver_province+","+dd.receiver_city+","+dd.receiver_district+","+
                dd.receiver_address), namemobile: {name: dd.sender_name, mobile:dd.sender_mobile}, startorend: 1})//start

            //判断订单状态，如果订单处于运送状态，则显示路线
            if( info.status != "运输中" ) {
                info.waybill.forEach(function (waybill) {
                    if( truckid == -1   ) {
                        if (waybill.xs.length < 2) {
                            console.log("路线输入点数量小于2个");
                        }
                        var points = wayPoints(waybill.xs);             //  将后台数据处理为做BMap.Point类型
                        var point = new BMap.Point(waybill.location.longitude, waybill.location.latitude);

                        var geoc = new BMap.Geocoder();
                        geoc.getLocation(point, function (rs) {
                            var addComp = rs.addressComponents;
                            var addressinfo = addComp.province + ", " + addComp.city + ", " +
                                addComp.district + ", " + addComp.street + ", " + addComp.streetNumber;
                            truckinfoBox = new InfoBox(map, 0, {
                                placeinfo: addressinfo,
                                updatetime: waybill.location.time
                            });
                            //当前车的位置，添加车ico
                            drawIcon(map, point, "./images/ico/point.png", truckinfoBox, 98);

                        });

                        driving.search(senderP, point, {waypoints: points});
                    }else if(truckid == waybill.id){
                        defaultcolor = overcolor;
                        if (waybill.xs.length < 2) {
                            console.log("路线输入点数量小于2个");
                        }
                        var points = wayPoints(waybill.xs);             //  将后台数据处理为做BMap.Point类型
                        var point = new BMap.Point(waybill.location.longitude, waybill.location.latitude);

                        var geoc = new BMap.Geocoder();
                        geoc.getLocation(point, function (rs) {
                            var addComp = rs.addressComponents;
                            var addressinfo = addComp.province + ", " + addComp.city + ", " +
                                addComp.district + ", " + addComp.street + ", " + addComp.streetNumber;
                            truckinfoBox = new InfoBox(map, 0, {
                                placeinfo: addressinfo,
                                updatetime: waybill.location.time
                            });
                            //当前车的位置，添加车ico
                            drawIcon(map, point, "./images/ico/point.png", truckinfoBox, 98);

                        });
                        trucklocal = point;
                        driving.search(senderP, point, {waypoints: points});
                    }
                })
            }

            drawIcon(map, senderP, "./images/ico/sender.png", senderinfoBox, 99);
            drawIcon(map, receiverP, "./images/ico/receiver.png", receiverinfoBox, 99);

            //if( trucklocal != null   ) {
            //    map.setViewport([senderP, trucklocal], {     // 根据需要显示的点，地图自动调整缩放等级和显示位置
            //        margins: [40, 10, 10, 10]
            //    });
            //}else
            map.setViewport([senderP, receiverP], {     // 根据需要显示的点，地图自动调整缩放等级和显示位置
                margins: [40, 10, 10, 10]
            });
        })



    }

    //清除已绘制的线
    this.clear = function(){
        console.log();
        var routemakerslength = routemakers.length;
        for(var i = 0; i < routemakerslength; i++){

            map.removeOverlay(routemakers.pop());
        }
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

            if( truckinfoBox )   truckinfoBox.show(e.point);

            if( wayoption )   wayoption.setStrokeColor(overcolor);
        });
        BMap.Polyline.prototype.addEventListener("mouseout", function (e) {

            if( truckinfoBox )   truckinfoBox.close();

            if( wayoption )   wayoption.setStrokeColor(defaultcolor);
        });
        wayoption = new BMap.Polyline(path, {//路线参数
            strokeColor: defaultcolor,
            enableClicking: false
        });

        map.addOverlay(wayoption);
        routemakers.push(wayoption);
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

    /********icon start**********/
//point:图标在地图上显示的地理位置(BMap.point)
//src:图标对应的图片
    function drawIcon(map, point, src,  infobox, indexz){

        var img = document.createElement("img");
        img.setAttribute("src", src);
        img.onload = function() {
            var pic    =       new BMap.Icon(src, new BMap.Size(img.width, img.height));
            var maker = new BMap.Marker(point,{icon:pic});
            maker.setZIndex(indexz);
            map.addOverlay(maker);
            routemakers.push(maker);
            maker.addEventListener("mouseover", function(e){
                infobox.show(e.point);
            })
            maker.addEventListener("mouseout", function(e){
                infobox.close();
            })
        }
    }
    /********icon end**********/

}


//绘制左侧窗体控件
//flag: 0: 只显示订单
//flag: 1: 显示订单和车辆信息
function drawLeftControl(map, infos, flag){
    if( flag == 1 ) {
        // 创建控件
        var leftControl = new LeftControl(infos, flag);
        // 添加到地图当中,会执行initialize
        map.addControl(leftControl);
    }
}

/*******左侧窗体控件构造 start********/

//控件构造
//左侧黑窗控件
function LeftControl(infos) {
    this.defaultAnchor = BMAP_ANCHOR_TOP_LEFT;
    this.defaultOffset = new BMap.Size(0, 0);
    this.infos = infos;
    this.maindiv = "";

}
LeftControl.prototype = new BMap.Control();
LeftControl.prototype.initialize = function (map) {

    var Tab = function (infos) {
        var ordercontent;
        var truckcontent;
        var flex;
        this.makeContent = function () {
            this.ordercontent =
                '<div id="viewheight" style="overflow: hidden"><div class="header" id="header" style="display: none;text-align: center"><img style="margin-top: 3px" src="./images/ico/upshrink.png"></div>' +
                '<div class="footer" id="footer" style="display: none;text-align: center"><img style="margin-top:3px" src="./images/ico/downunfold.png"></div>' +
                '<div id="alllist" style="width:100%;position: relative;">' + getOrderContent(infos) + '</div></div>';

            ordercontent = this.ordercontent;

            this.content = '<div class="content" id="content"><div class="contentorder" id="contentorder">' +
            '<div style="height: 60px;width: 100%;position: relative"><span style="position: absolute;top:20px;left:40px">订单详情</span></div>' +
            this.ordercontent +
            '</div>'+'<div class="contenttruck" id="contenttruck" style="display: none;">' +
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
                    solidunfloddiv.setAttribute("src", "./images/ico/rightunfold.png");
                    flag = 1;
                } else {
                    maindivstyle.width = "300px";
                    tabdivstyle.height = "80px";
                    contentdivstyle.width = "100%";
                    contentdivstyle.display = "block";
                    solidunfloddiv.setAttribute("src", "./images/ico/leftshrink.png");
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
        function getOrderContent(infos) {
            flex = new Flex(map, infos);
            return flex.getFlexDiv();

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
            '<img class="solidunflod" id="solidunflod" src="./images/ico/leftshrink.png">' +

            '</img>';

    }

    var tab = new Tab(this.infos);

    var main = makediv('io_main', tab.makeContent());

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

/****start******/
//lists: title, content
var Flex = function (map, infos) {
    var listids = new Array();//存储下拉列表的id
    var trucksinfo = null;
    var alllistdefaultheight = 0;
    var alllistcurrentheight = 0;
    var currentroute = null;
    var viewheight = 410 ;//这样定义的变量就是静态变量
    var step = 200;
    //每一个order内容详情的高度
    var contentHeight = new Array();

    //滑动框html
    this.getFlexDiv = function () {
        var flexdiv = '';

        for (var i = 0; i < infos.length; i++) {

            var info = infos[i];
            var id = info.id;
            flexdiv += '' +
            '<div class="orderlist"' + ' id="list' + id + '">' +
            '<div class="ordertitle" id="listtitle' + id + '">' +'<span style="position: absolute;left:20px;top:20px">订单号: '+ info.id +'</span>'+
            '<img class="listtitleimg" id="listtitleimg' + id + '" src="./images/ico/downunfold.png" >' +
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
        var contentheight = 0;
        for (var i = 0; i < stlist.length; i++) {
            var pad;

            if ( i == 0){
                pad = "<img src='./images/ico/line_p_3.png' class='left'>";
                contentheight += 73;
            }
            else if ( i == stlist.length - 1) {
                pad = "<img src='./images/ico/line_p_1.png' class='left'>";
                contentheight += 74;
            }
            else {
                pad = "<img src='./images/ico/line_p_2.png' class='left'>";
                contentheight += 68;
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
        contentHeight[info.id] = contentheight;
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
    this.init = function () {
        var lists = lkGetElementById(listids);
        var currentunfold = null;

        alllistdefaultheight = lists.length * (60 + 2);//所有list都处于收缩状态的情况下的总高度
        alllistcurrentheight = alllistdefaultheight;
        //司机货车数据是根据用户点击哪个订单显示对应的信息
        function getTruckContent(info) {
            //daixie................
            //运单收集
            var waybilllist = info.waybill;
            var addb = '';
            var addb = '<div style="text-align: center">' + "<span class='shiplist dark' style='height: 70px;line-height: 70px;'>" +
                '当前已安排 ' + waybilllist.length + ' 位司机运送' +
                "</span></div>" + addb;
            for (var i = 0; i < waybilllist.length; i++) {
                addb = addb +
                "<div class='carlink' id='truck" + waybilllist[i]['id'] + "'>" +
                "<img src='" + "./images/ico/b-w.png" + "' style='width:30px;' class='l_title left'>" +
                '<span class="r_driver">' +
                waybilllist[i]['truckno'] +
                "</span>" +
                "<div class='clearfix'></div>" +
                "</div>";
            }

            return addb;
        }
        for (var i = 0; i < lists.length; i++) {
            lists[i].info = infos[i];
            lists[i].trucksinfo = getTruckContent(lists[i].info);
            var list = lists[i];
            list.lkindex = i;
            list.contentheight = 300;
            var orderid = list.id.replace("list", "");//订单号
            list.orderid = orderid;
            document.getElementById("list"+ orderid).innerHTML = document.getElementById("list"+ orderid).innerHTML.replace("undefined","");
            list.contentstyle = document.getElementById(("listcontent"+orderid)).style;
            list.contentstyle.transition = "1s";

            //wait.....
            //以后是要通过计算得到内容高度的
            list.contentheight = contentHeight[orderid];

            list.lkflag = 0; // shrink, 1: unfold
            //css init
            var style = list.style;
            style.width = '100%';
            style.height = '60px'
            style.marginBottom = '2px';
            style.transition = "1s";

            list.lktitle = document.getElementById("listtitleimg" + orderid);

            list.lktitle.parent = list;

            list.lktitle.addEventListener("click", function () {
                if( this.parent.lkflag == 0 ){
                    routeListener(map, this.parent.info, -1);
                }
                titleListener(this.parent);


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

        var contenttruck = document.getElementById("contenttruck");
        var footer = document.getElementById("footer");
        var header = document.getElementById("header");
        var alllistlenggth = 5 * 60;//所有list都处于收缩状态的情况下的高度
        var downoffset = 0;//记录窗体向下偏移的距离
        var headerstyle = document.querySelector("#header").style;
        var footerstyle = document.querySelector("#footer").style;
        var allliststyle = document.querySelector("#alllist").style;
        var viewheightstyle = document.querySelector("#viewheight").style;
        viewheightstyle.height = viewheight + "px";
        footerstyle.top = viewheight + 82 + 60 - 30 + "px";
        allliststyle.transition = "1s";
        if( alllistcurrentheight > viewheight ){
            footerstyle.display = "inline";
        }
        footer.addEventListener("click", function(e){
            console.log(alllistcurrentheight+"::" + downoffset);
            //移动步长20
            //我也不知道62是怎么计算的,就这么用
            if( downoffset + step > alllistcurrentheight - viewheight + 30){
                downoffset = (alllistcurrentheight - viewheight) > 0 ? (alllistcurrentheight - viewheight +30) : 0;
                footerstyle.display = "none";
            }else{
                downoffset += step;
            }
            if( downoffset != 0 ) {
                headerstyle.display = "inline";
            }
            console.log(downoffset);
            allliststyle.transform = "translate(0px, -" + downoffset + "px)";

        });

        header.addEventListener("click", function(e){
            var list = document.getElementById("list");
            var allliststyle = document.querySelector("#alllist").style;
            allliststyle.transition = "1s";
            if( downoffset - step < 0 ){
                downoffset = 0;
            }else{
                downoffset -= step;
            }
            if( downoffset == 0 ) {
                headerstyle.display = "none";
            }else{
                footerstyle.display = "inline";
            }
            allliststyle.transform = "translate(0px, -" + downoffset + "px)";

        });

        function titleListener(list){
            contenttruck.innerHTML = list.trucksinfo;
            for(var j = 0; j < list.info.waybill.length; j++){
                var truckid = list.info.waybill[j]['id'];
                var truckidhtml = document.getElementById("truck"+truckid);
                var truckidstyle = document.querySelector("#truck"+truckid).style;
                console.log(truckidhtml);
                truckidhtml.addEventListener("click", function(e){
                    truckidstyle.background = "rgba(83,74,72,0.8)";
                    routeListener(map, list.info, truckid);
                })
            }
            if (list.lkflag == 0) {//点击项未展开
                if (currentunfold) {//若当前已有展开的项,则先关闭
                    currentunfold.contentstyle.height = "0px";
                    list.shrink(currentunfold);
                    document.getElementById("listtitleimg"+currentunfold.orderid).setAttribute("src",  "./images/ico/downunfold.png");
                    currentunfold.lkflag = 0;
                }

                alllistcurrentheight = alllistdefaultheight + list.contentheight;
                if( downoffset + viewheight < alllistcurrentheight ){
                    footerstyle.display = "inline";
                    if(downoffset > 0){//如果当前订单信息列表高度小于视图高度，则隐藏header和footer
                        headerstyle.display = "inline";
                    }
                }
                document.getElementById("listtitleimg"+list.orderid).setAttribute("src",  "./images/ico/upshrink.png");
                list.unfold(); // 展开用户点击的项

                list.contentstyle.height = list.contentheight+"px";
                currentunfold = list; // 更新当前展开项
                list.lkflag = 1;// 记录当前项已展开

            } else {//如果当前项已展开
                document.getElementById("listtitleimg"+list.orderid).setAttribute("src",  "./images/ico/downunfold.png");
                alllistcurrentheight = alllistdefaultheight;
                if( downoffset + viewheight > alllistcurrentheight ){
                    console.log(downoffset+"::" + alllistcurrentheight+"::" + viewheight);


                    if( (alllistcurrentheight - viewheight) > 0 ){
                        allliststyle.transform =
                            "translate(0px, -" + (alllistcurrentheight - viewheight-62) + "px)";
                        downoffset = alllistcurrentheight - viewheight-62;
                        footerstyle.display = "none";
                    }else{//如果当前订单信息列表高度小于视图高度，则隐藏footer
                        allliststyle.transform =
                            "translate(0px, 0px)";
                        downoffset = 0;
                        footerstyle.display = "none";
                    }
                    console.log(downoffset);
                    if( downoffset > 0 ){
                        headerstyle.display = "inline";
                    }else{
                        headerstyle.display = "none";
                    }
                }

                list.contentstyle.height = "0px";
                list.shrink(list);//收缩当前项,即currentunfold == this
                currentunfold = null;//当前无展开项
                list.lkflag = 0;//记录当前项已收缩
            }
        }
        //第3个参数可不填， 当指定某一个司机的路线高亮的时候，填写该waybill.id
        function routeListener(map, info, truckid){
            //clear....
            if( currentroute != null ) currentroute.clear();
            var drawRoute = new DrawRoute(map, info);
            currentroute = drawRoute;
            drawRoute.draw(truckid);
        }

        titleListener(lists[0]);
        routeListener(map, lists[0].info, -1);

    }

}
/*****end******/

//container: 装载地图容器id
function loadLkMap(infos, container){
    var map = new BMap.Map(container);        // container: html 页面里的地图容器id名
    map.enableScrollWheelZoom(true);            // 开启滚轮缩放
    //map.addControl(new BMap.NavigationControl({anchor:BMAP_ANCHOR_TOP_RIGHT}));
    //map.addControl(new BMap.ScaleControl({anchor:BMAP_ANCHOR_BOTTOM_RIGHT}));

    drawLeftControl(map,  infos, 1);        //绘制地图界面的左侧自定义控件

    //由于进入页面默认显示第一个订单的信息，所以先将第一个订单的路线，起点终点信息显示出来

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
 *           namemobile{name;"dsf", mobile:"21324"}
 * */
var InfoBox = function(map, flag, options){

    var html = '';
    if( flag == 0 ) html = this.htmltem0(options.placeinfo, options.updatetime);
    if( flag == 1 ) html = this.htmltem1(options.cars);
    if( flag == 2 ) html = this.htmltem2(options.company, options.placeinfo, options.namemobile, options.startorend);

    this.infobox = new BMapLib.InfoBox(map,html);
}

InfoBox.prototype.htmltem0 = function(placeinfo, updatetime){
    return '<div class="infoboxcss1"><span style="color:red;display: block">当前位置</span><div style="display: block">'+placeinfo+ "</div>" + updatetime+'</div>';
}

InfoBox.prototype.htmltem1 = function(cars){
    return '<div class="infoboxcss2"></div>';
}

//trunkinfo: 司机信息：name姓名，mobilenumber号码
//flag: 0: sender起点, 1: recevier
InfoBox.prototype.htmltem2 = function(company, placeinfo, namemobile, senderorrecevier){
    if( senderorrecevier == 0 ) imgsrc = "发";
    else imgsrc = "收";;
    return '<div class="infoboxcss3" id="infobox3"><div style="margin-bottom:20px;">'+imgsrc + ": "+ company + '</div><div style="border-bottom: gray solid 1px"></div><div style="margin-top: 20px">'+namemobile.name+"   "+namemobile.mobile+"</div>" + placeinfo+'</div>';
}

InfoBox.prototype.show = function(point){
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
            callback(senderP, receiverP);
        }
    }
}


loadLkMap([info1, info2], "container");
