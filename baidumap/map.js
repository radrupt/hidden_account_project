/**
 * Created by wangd933 on 15-3-11.
 */

var infosmap = [];

var LKMap = function (container) {
    if( $("#"+container)[0] == null ){
        alert("id为"+ container +"的元素不存在");
        return;
    }
    this.initialize(container);
}
//创建lkmap的时候调用initialize
//初始化地图的静态布局
LKMap.prototype.initialize = function (container) {
    //地图初始化
    this.map = mapInitialize(container);
    //控件初始化
    this.leftcontrol = new LeftControl();
    //当调用addcontrol时，会自动执行control的initialize函数
    this.map.addControl(this.leftcontrol);
}
//设置左侧窗口控件样式
LKMap.prototype.setLeftControl = function () {

}
//加载数据,创建完地图后调用loadData
LKMap.prototype.loadData = function (data) {

}

function LeftControl() {
    this.defaultAnchor = BMAP_ANCHOR_TOP_LEFT;
    this.defaultOffset = new BMap.Size(0, 0);
}

LeftControl.prototype = new BMap.Control();
LeftControl.prototype.initialize = function (map) {
    map.getContainer().appendChild($(".leftcontrol")[0]);
    function titleListener(title){
        var occontent = $(".occontent")[0];
        var img   = title.children("img");
        var itcborder = title.next();
        var itcmain = itcborder.children(".itcmain");
        var defaultheight = itcborder.children(".itcmainheight").attr("act");
        if( occontent.currentopentitle
            && occontent.currentopentitle[0] != title[0]){
            occontent.currentopentitle.css("background", "");
            occontent.currentopentitle.children("img").attr("src","/img/ico/down.png");
            occontent.currentopentitle.next().css("height","0px");
        }
        if( /\/img\/ico\/down.png/.test(img.attr("src")) ){//title未展开
            title.css("background", "rgba(83,74,72,0.8)");
            img.attr("src","/img/ico/up.png");
            occontent.currentopentitle = title;
            itcborder.css("height",defaultheight+"px");
            itcmain.css("transform","translate(0px,0px)");
            occontent.ctitlecontentheight = defaultheight;
            occontent.ctitleoffset = 0;
        }else{
            title.css("background", "");
            img.attr("src","/img/ico/down.png");
            occontent.currentopentitle = null;
            itcborder.css("height","0px");
        }
    }
    function routeListener(printid,waybillid){
        var info = infosmap[printid];
        var lkroute = new LKRoute(map);
        if( waybillid ){
            for( var i = 0; i < info.waybill.length; i++ ){
                if(info.waybill[i].id == waybillid ){
                    lkroute.draw(info,info.waybill[i]);
                    return;
                }
            }
        }
        lkroute.draw(info);
    }
    //给title添加click事件
    $(".infotitle").click(function(e){
        //获取当前点击title的jquery对象
        var title = $(e.currentTarget);
        titleListener(title);
        routeListener(title.attr("act"));
    })
    //处理订单列表的整体上下滑动
    var step = 60;
    $(".upbtn").click(function(e){
        var occontent = $(".ocbottom").prev();
        if( occontent[0].currentopentitle ){
            var itcborder = occontent[0].currentopentitle.next();
            var itcmain = itcborder.children(".itcmain");
            var itcmainheight = (itcmain.css("height") || "0px").replace("px","");
            if( occontent[0].ctitleoffset > 0 ) {
                occontent[0].ctitleoffset -= step;
                if (occontent[0].ctitleoffset < 0) occontent[0].ctitleoffset = 0;

                itcmain.css("transform", "translate(0px,-" + occontent[0].ctitleoffset + "px)");
                itcborder.css("height", itcmainheight - occontent[0].ctitleoffset + "px");
                return;
            }
        }

        var occontentheight = occontent.css("height").replace("px", "");
        var occmain = occontent.children(".occmain");
        var occmainheight = occmain.css("height").replace("px", "");
        if ( !occmain[0].lkoffset ) {
            occmain[0].lkoffset = 0;
        }
        if (occmainheight - occmain[0].lkoffset - step >= occontentheight) {
            occmain[0].lkoffset += step;
        } else {
            occmain[0].lkoffset = occmainheight - occontentheight;
        }
        occmain[0].lkoffset = occmain[0].lkoffset/60*60;
        occmain.css("transform", "translate(0px,-" + occmain[0].lkoffset + "px)");

    })
    $(".downbtn").click(function(e){
        var occontent = $(".ocbottom").prev();
        if( occontent[0].currentopentitle ){
            var itcborder = occontent[0].currentopentitle.next();
            var itcmain = itcborder.children(".itcmain");
            var itcmainheight = (itcmain.css("height") || "0px").replace("px","");
            if( occontent[0].ctitleoffset < occontent[0].ctitlecontentheight ) {
                if (occontent[0].ctitleoffset + step < occontent[0].ctitlecontentheight) {
                    occontent[0].ctitleoffset += step;
                } else {
                    occontent[0].ctitleoffset = occontent[0].ctitlecontentheight;
                }
                itcmain.css("transform", "translate(0px,-" + occontent[0].ctitleoffset + "px)");
                itcborder.css("height", itcmainheight - occontent[0].ctitleoffset + "px");
                return;
            }
        }
        var occontentheight = occontent.css("height").replace("px", "");
        var occmain = occontent.children(".occmain");
        var occmainheight = occmain.css("height").replace("px", "");
        if (occmain[0].lkoffset == undefined) {
            occmain[0].lkoffset = 0;
        }
        occmain[0].lkoffset -= step;
        occmain[0].lkoffset = occmain[0].lkoffset/60*60;
        if (occmain[0].lkoffset < 0) {
            occmain[0].lkoffset = 0;
        }
        occmain.css("transform", "translate(0px,-" + occmain[0].lkoffset + "px)");
    })
    //监听自定义窗体的伸缩
    $(".unfoldshrink").click(function(e){
        var img = $(e.currentTarget);
        if( /\/img\/ico\/left.png/.test(img.attr("src")) ) {//title未展开
            $(".leftcontrol").addClass("shrink");
            img.attr("src","/img/ico/right.png");
            $(".lctop").css("display","none");
            $(".content").css("display","none");
        }else{
            $(".leftcontrol").removeClass("shrink");
            img.attr("src","/img/ico/left.png");
            $(".lctop").css("display","block");
            $(".content").css("display","block");
        }
    })

    if(flag) {
        $(".orderbtnimg").click(function(){
            $(".orderbtnimg").attr("src","/img/ico/a-o.png");
            $(".truckbtnimg").attr("src","/img/ico/b-w.png");
            $(".lcdbottomimg").attr("src","/img/ico/x.png");
            $(".ordercontent").css("display","");
            $(".truckcontent").css("display","none");
            $(".truckcontent").html("");
        })
        $(".truckbtnimg").click(function(){
            $(".orderbtnimg").attr("src","/img/ico/a-w.png");
            $(".truckbtnimg").attr("src","/img/ico/b-o.png");
            $(".lcdbottomimg").attr("src","/img/ico/p.png");
            $(".ordercontent").css("display","none");
            $(".truckcontent").css("display","block");
            if( $(".occontent")[0].currentopentitle ) {
                var truckinfo = $(".occontent")[0].currentopentitle.parent(".infocontent").next();
                $(".truckcontent").html(truckinfo.html());
                var printid = $(".occontent")[0].currentopentitle.attr("act");
                var truckid = $(".truckcontent").children(".carlink").attr("act");
                var cars = $(".truckcontent").children(".carlink");
                for(var i = 0; i<cars.length; i++){
                    $(cars[i]).children("img").attr("src","/img/ico/b-w.png");
                    $(cars[i]).children("span").css("color","white");
                    $(cars[i]).css("background","");
                }
                $(".carlink").click(function(e){
                    routeListener(printid,truckid);
                    $(e.currentTarget).children("img").attr("src","/img/ico/b-o.png");
                    $(e.currentTarget).children("span").css("color","#ee7932");
                    $(e.currentTarget).css("background","rgba(83,74,72,0.8)");
                })
            }else{
                $(".truckcontent").html("请选择订单");
            }
        })
    }
}

var LKRoute = function(map){
    var defaultcolor = "#333333";
    var overcolor = "#0083ff";
    var wayoption = null;
    var routemakers = [];
    this.draw = function(info,waybill){
        if( !info ) return;
        computeSenderAndReceiverPOI(info,function(senderP, receiverP){

            if( waybill ){
                var point = new BMap.Point(waybill.location.lng, waybill.location.lat);
                next(
                    wayPoints(waybill.xs || [])
                    ,waybill
                    ,( waybill.location && point) || ""
                    ,0
                );
                map.setViewport([senderP, point], {     // 根据需要显示的点，地图自动调整缩放等级和显示位置
                    margins: [71, 200, 0, 200]
                });
            }else{
                //drawicon start end
                info.waybill.forEach(function(waybill){
                    next(
                        wayPoints(waybill.xs || [])
                        ,waybill
                        ,( waybill.location && new BMap.Point(waybill.location.lng, waybill.location.lat)) || ""
                        ,1
                    );
                })
                map.setViewport([senderP, receiverP], {     // 根据需要显示的点，地图自动调整缩放等级和显示位置
                    margins: [71, 200, 1, 200]
                });
            }
            function next(points, waybill, point, flag) {
                if( !point ){
                    if( !flag )
                        alert("当前司机离线");
                    return;
                }
                var geoc = new BMap.Geocoder();
                geoc.getLocation(point, function (rs) {
                    var addComp = rs.addressComponents;
                    var addressinfo = addComp.province + ", " + addComp.city + ", " +
                        addComp.district + ", " + addComp.street + ", " + addComp.streetNumber;
                    var truckrouteinfobox = new LKInfoBox(
                        "truckrouteinfobox" + waybill.id,
                        'infoboxcss1',
                        makeInfoBoxInnerHtml(
                            1,
                            {
                                placeinfo: addressinfo,
                                updatetime: waybill.location.date
                            }
                        )
                    );console.log(point);
                    drawIcon(map, point, "/img/ico/point.png", truckrouteinfobox, 98);
                    var driving = new BMap.DrivingRoute(map, {
                        onSearchComplete: function (results) {
                            if (driving.getStatus() == BMAP_STATUS_SUCCESS) {
                                // 地图覆盖物
                                addOverlays(results);
                            }else{
                                console.log("sdf");
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
                        wayoption = new BMap.Polyline(path, {//路线参数
                            strokeColor: flag ? defaultcolor : overcolor,
                            enableClicking: false
                        });
                        wayoption.flag = flag;
                        wayoption.addEventListener("mouseover", function (e) {
                            this.setStrokeColor(overcolor);
                        });
                        wayoption.addEventListener("mouseout", function (e) {
                            this.setStrokeColor(this.flag ? defaultcolor : overcolor);
                        });
                        map.addOverlay(wayoption);
                        routemakers.push(wayoption);

                    }
                    driving.search(senderP, point);
                })
            }
            var senderinfoBox = new LKInfoBox('senderinfobox' + info.id, 'infoboxcss3',
                makeInfoBoxInnerHtml(
                    3,
                    {
                        company: info.sender.company,
                        placeinfo: (info.sender.province + "," + info.sender.city + "," + info.sender.district + "," + info.sender.address),
                        namemobile: {name: info.sender.name, mobile: info.sender.mobile},
                        flag: "sender"
                    }
                )
            )//start

            var recevierinfoBox = new LKInfoBox('reveiverinfobox' + info.id, 'infoboxcss3',
                makeInfoBoxInnerHtml(
                    3,
                    {
                        company: info.receiver.company,
                        placeinfo: (info.receiver.province + "," + info.receiver.city + "," + info.receiver.district + "," + info.receiver.address),
                        namemobile: {name: info.receiver.name, mobile: info.receiver.mobile},
                        flag: "recevier"
                    }
                )
            );//start
            drawIcon(map, senderP, "/img/ico/sender.png", senderinfoBox, 99);
            drawIcon(map, receiverP, "/img/ico/receiver.png", recevierinfoBox, 99);
        })
    }
    this.clear = function(){
        while(routemakers.length>0){
            this.map.removeOverlay(routemakers.pop());
        }
    }
    //point:图标在地图上显示的地理位置(BMap.point)
    //src:图标对应的图片
    function drawIcon(map, point, src, lkinfobox, indexz) {

        var img = document.createElement("img");
        img.setAttribute("src", src);
        img.onload = function () {
            var pic = new BMap.Icon(src, new BMap.Size(img.width, img.height));
            var maker = new BMap.Marker(point, {icon: pic});
            maker.setZIndex(indexz);
            map.addOverlay(maker);
            routemakers.push(maker);
            maker.img = img;
            maker.addEventListener("mouseover", function (e) {
                //lkinfobox.show(this.map.offsetX + this.K.offsetLeft - this.K.offsetWidth  - this.img.width , this.map.offsetY  + this.K.offsetTop - this.K.offsetHeight );
                //this.map.offsetX + this.K.offsetLeft, this.map.offsetY  + this.K.offsetTop
                //可以将信息框定位到ico的左上角

                var x = this.map.offsetX + this.K.offsetLeft + this.K.clientWidth / 2;
                var y = this.map.offsetY + this.K.offsetTop + this.K.clientHeight / 2;

                if (y - this.K.clientHeight / 2 - 50 > lkinfobox.height) {//上方空间足够
                    if (x - this.K.clientWidth / 2 - 100 < lkinfobox.width / 2) {
                        //显示在右上方
                        lkinfobox.show(x + 5, y - this.K.clientHeight / 2 - lkinfobox.height - 50);
                    } else if (document.body.clientWidth - x - this.K.clientWidth / 2 - 100 < lkinfobox.width / 2) {
                        //显示在左上方
                        lkinfobox.show(x - lkinfobox.width - this.K.clientWidth / 2 + 5, y - this.K.clientHeight / 2 - lkinfobox.height - 50);
                    }
                    else {//左右方空间足够,显示在正上方
                        lkinfobox.show(x - lkinfobox.width / 2 - this.K.clientWidth / 2 + 5, y - this.K.clientHeight / 2 - lkinfobox.height - 50);
                    }
                } else if (y - this.K.clientHeight / 2 - 50 < lkinfobox.height) {
                    if (x - this.K.clientWidth / 2 - 100 < lkinfobox.width / 2) {//显示在右上方
                        lkinfobox.show(x + 5, y + this.K.clientHeight / 2);
                    } else if (document.body.clientWidth - x - this.K.clientWidth / 2 - 100 < lkinfobox.width / 2) {//显示在左上方
                        lkinfobox.show(x - lkinfobox.width - this.K.clientWidth / 2 + 5, y + this.K.clientHeight / 2);
                    }
                    else {
                        lkinfobox.show(x - lkinfobox.width / 2 - this.K.clientWidth / 2 + 5, y + this.K.clientHeight / 2);
                    }
                }
                //动态判断图标的位置

                //x = this.map.offsetX + this.K.offsetLeft + this.K.offsetWidth/2 - lkinfobox.width/2 - 13;
                //y = this.map.offsetY  + this.K.offsetTop + this.K.offsetHeight/2 - lkinfobox.height - 100


            })
            maker.addEventListener("mouseout", function (e) {
                lkinfobox.hidden();
            })
        }
    }
}

/* name: computeSenderAndReceiverPOI
 * function: 根据用户输入的地理位置信息计算得到始发地和目的地的地理坐标,
 * params: info-->服务器获取的货主信息，包括历史当前订单信息，运输车辆信息
 *         callback-->回调函数,当计算好坐标后调用
 * */
function computeSenderAndReceiverPOI(info, callback) {

    if (!info) {
        console.log("info: 无有效信息");
        return;
    }

    var senderR = info.sender.province + info.sender.city + info.sender.district + info.sender.address;
    var receiverR = info.receiver.province + info.receiver.city + info.receiver.district + info.receiver.address;
    var senderP = null;
    var receiverP = null;

    var senderPGeocoder = new BMap.Geocoder();
    senderPGeocoder.getPoint(senderR, function (point) {
        if (point) {
            senderP = point;
            xboot();
        } else {
            alert("您选择地址没有解析到结果!");
        }
    }, info.sender.province);

    var receiverPGeocoder = new BMap.Geocoder();
    receiverPGeocoder.getPoint(receiverR, function (point) {
        if (point) {
            receiverP = point;
            xboot();
        } else {
            alert("您选择地址没有解析到结果!");
        }
    }, info.receiver.province);

    function xboot() { //如果起点，终点坐标计算好了，则开始绘制地图
        if (receiverP && senderP) {
            callback(senderP, receiverP);
        }
    }
}

/*private
 *由于同样的起点终点，司机可能跑的是不同的路线，
 * 所以服务器在保存司机路线的时候，
 * 是在该司机对应路线上取有限点来描述司机路线的
 * 但是百度search的时候输入数据最多只能是10个，
 * 所以需要处理路线点的数量
 */
function wayPoints(wpoints) {//导航路线上均匀分布的点

    var routed = wpoints;
    if (routed.length > 0) {
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
    } else
        return null;

    function redo(arr) {
        var result = [];
        for (var i = 0; i < arr.length; i++) {
            result[i] = (new BMap.Point(arr[i]['longitude'], arr[i]['latitude']));
        }
        return result;
    }
}

//map load
//默认显示用户的当前位置
function mapInitialize(container) {
    var map = new BMap.Map(container, {enableMapClick: false});        // container: html 页面里的地图容器id名
    map.enableScrollWheelZoom(true);            // 开启滚轮缩放
    map.addControl(new BMap.NavigationControl({anchor: BMAP_ANCHOR_TOP_RIGHT}));
    map.addControl(new BMap.ScaleControl({anchor: BMAP_ANCHOR_BOTTOM_RIGHT}));
    var point = new BMap.Point(116.331398,39.897445);
    map.centerAndZoom(point,12);  // 初始化地图,设置中心点坐标和地图级别
    function myFun(result){
        var cityName = result.name;
        map.setCenter(cityName);
    }
    var myCity = new BMap.LocalCity();
    myCity.get(myFun);
    return map;
}

/*******LKInfoBox start*********/
//links 消息弹出框,属于body的字节点
{
    var left = 100;
    var LKInfoBox = function (id, classname, innerHtml) {
        this.width = 250;
        this.height = 100;
        var infoboxtype = {'infoboxcss1': 1, 'infoboxcss2': 2, 'infoboxcss3': 3};
        this.infoboxdiv = makeInfoBoxDiv(id, classname, innerHtml);
        this.type = infoboxtype[classname];

        function makeInfoBoxDiv(id, classname, innerHtml) {
            var infoboxdiv = document.createElement("div");
            infoboxdiv.id = id;
            infoboxdiv.innerHTML = innerHtml;
            infoboxdiv.className = classname;
            infoboxdiv.style.left = left + 100 + "px";
            left += 100;
            document.body.appendChild(infoboxdiv);
            return infoboxdiv;
        }
    }
    //x,y:相对于当前页面（不包括页面的滚动）的左上角的偏移
    LKInfoBox.prototype.show = function (x, y) {
        //if (this.type == 3) {
        //    y = 0;
        //}

        this.infoboxdiv.style.display = "block";
        this.infoboxdiv.style.left = x + document.body.scrollLeft + "px";
        this.infoboxdiv.style.top = y + document.body.scrollTop + "px";
    }
    LKInfoBox.prototype.hidden = function () {
        this.infoboxdiv.style.display = "none";
    }
    LKInfoBox.prototype.addEventListener = function (event, callback) {
        this.infoboxdiv.addEventListener(event, callback);
    }
}

//生成LKInfoBox的innerhtml内容
/*
 *flag: 1: 车辆位置信息提示框,
 *      2: 运输订单货物的车辆列表,
 *      3: 发送方和接收方信息
 * options:  cars: 运输车辆信息,array
 *           isstart: 标志该提示框是否对应起点
 *           isstop:  标志该提示框是否对应终点
 *           truckinfo: 司机信息
 *           placeinfo: 位置信息
 *           updatetime:司机位置最新更新时间
 *           namemobile{name;"dsf", mobile:"21324"}
 * */
function makeInfoBoxInnerHtml(flag, options) {

    function htmltem1(placeinfo, updatetime) {
        return '<span style="color:red;display: block">当前位置</span>' +
            '<div style="display: block">' + placeinfo + "</div>" + updatetime;
    }

    function htmltem2(info) {

        var html = "<div style='height:20px;padding-left: 20px'><span style='font-size: 110%'>当前已安排&nbsp;<span style='color:#FF7C2A'>" + info.waybill.length + "</span>&nbsp;位司机提货</span></div>";
        info.waybill.forEach(function (way) {
            html += "<div style='cursor:pointer;width:100%;height:10px;display:block;margin-top:20px;margin-left:25px' id='ordertruckinfoboxtruck" + way.id + "' >" +
            "<img id='ordertruckinfoboxtruckimg" + way.id + "' src='" + "/img/ico/b-w.png" + "' height='20px'  style='margin-right:20px' class='left'>" +
            '<div ><span   id="ordertruckinfoboxtruckspan' + way.id + '">' +
            way['truckno'] +
            "</span></div>" +
            "</div>";
        })
        return html;
    }

    //trunkinfo: 司机信息：name姓名，mobilenumber号码
    //senderorrecevier: 0: sender起点, 1: recevier
    function htmltem3(company, placeinfo, namemobile, senderorrecevier) {
        var imgsrc;
        if (senderorrecevier == "sender") imgsrc = "发";
        else if (senderorrecevier == "recevier") imgsrc = "收";
        return '<div style="margin-bottom:20px;">' + imgsrc + ": " + company + '</div>' +
            '<div style="border-bottom: gray solid 1px"></div><' +
            'div style="margin-top: 20px">' + namemobile.name + '&nbsp;&nbsp;' + namemobile.mobile + "</div>" +
            placeinfo;
    }

    if (flag == 1) return htmltem1(options.placeinfo, options.updatetime);
    if (flag == 2) return htmltem2(options.info);
    if (flag == 3) return htmltem3(options.company, options.placeinfo, options.namemobile, options.flag);
    return "";
}

$(document).ready(function(){
    infos.forEach(function(info){
        infosmap[info.id] = info;
    })
    var lkmap = new LKMap("container");
})
