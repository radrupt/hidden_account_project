/**
 * Created by wangd on 2016/3/11.
 */
export var leftmenu = {
    keyname:'left_menu',
    flag:false,//weather to update localstorage
    data:[
        {
            title:"货主",
            open:false,
            listHeight:'80',
            list:[{
                name: '新注册未认证',
                productId:'s1',
                routerLink: ['HomeDashboard', 'Shipper','new']
            },{
                name: '待认证',
                productId:'s2',
                routerLink: ['HomeDashboard','Shipper','noauth']
            }]
        },
        {
            title:"司机",
            open:false,
            listHeight:'80',
            list:[{
                name: '新注册未认证',
                productId:'d1',
                routerLink: ['HomeDashboard', 'Driver','new']
            },{
                name: '待认证',
                productId:'d2',
                routerLink: ['HomeDashboard','Driver','noauth']
            }]
        },
        {
            title:"订单",
            open:false,
            listHeight:'80',
            list:[{
                name: '待成交',
                productId:'o1',
                routerLink: ['HomeDashboard', 'Order','remain']
            },{
                name: '运输中',
                productId:'o2',
                routerLink: ['HomeDashboard','Order','trans']
            }]
        }
    ]
}

export var baseUrl = 'app/';

export var selectlist = [{key:1,value:'1'},{key:2,value:'2'},{key:3,value:'3'}];