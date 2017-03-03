/**
 * Created by wangd on 2016/3/10.
 */
import {Component,View} from 'angular2/core';
import {SearchFormComponent} from '../../../shared/component/search-form/search-form.component'
import {selectlist} from '../../../config'

@Component({
    selector: 'shipper',
    templateUrl:'shipper.component.html',
    styleUrls:['shipper.component.css'],
    directives:[SearchFormComponent],
    moduleId:__moduleName,
})

export class ShipperComponent{
    public config:Object = [
        {
            title:'mobile',
            value:'1',
            key:'mobile',
            type:'text',//normall input
        },{
            title:'name',
            value:selectlist[0].value,
            key:'name',
            selectlist:selectlist,
            type:'select',//normall input
        },{
            title:'time',
            value:'',
            key:'time',
            type:'date',//normall input
        },{
            value:{start:{value:'',title:'from'},end:{value:'',title:'to'}},
            key:'daterange',
            type:'date-double',//normall input
        },{
            kind:1,
            value:'',
            key:'area',
            type:'area',//normall input
        },{
            kind:2,
            value:'',
            key:'area2',
            type:'area',//normall input
        }
    ];
    constructor(
    ){

    }
    search(data){//再这里处理value的值为object的数据
        console.log(data);
    }
}