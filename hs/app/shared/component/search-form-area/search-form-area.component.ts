/**
 * Created by wangd on 2016/3/14.
 */
import {Component,View,Input} from 'angular2/core';
import {areaall,area} from './area';

/*
* input:data:{kind:(1(没有全部选项),2),value:id}
* output:data:{value:id(000000--全部)}
* */
@Component({
    selector: 'search-form-area',
    templateUrl:'search-form-area.component.html',
    styleUrls:['search-form-area.component.css'],
    moduleId:__moduleName,
})

export class SearchFormAreaComponent{
    @Input('data') data: Object;
    _area:Array = [];
    _prov:Object = {};
    _city:Object = {};
    _dist:Object = {};
    set provid(id){
        for (var v of this._area) {
            if (v.id == id) this._prov = v;
        }
        this.cityid = this._prov.childs[0].id;
        this.distid = this._city.childs[0].id;
        this.setValue();
    }
    get provid(){
        return this._prov.id;
    }
    set cityid(id){
        for (var v of this._prov.childs) {
            if (v.id == id) this._city = v;
        }
        this.distid = this._city.childs[0].id;
        this.setValue();
    }
    get cityid(){
        return this._city.id;
    }
    set distid(id){
        for (var v of this._city.childs) {
            if (v.id == id) this._dist = v;
        }
        this.setValue();
    }
    get distid(){
        return this._dist.id;
    }
    constructor(
    ){
    }

    ngOnInit() {
        if(this.data.kind == 1) this._area = area;
        else this._area = areaall;
        if(!this.data.value){
            this._prov = this._area[0];
            this._city = this._prov.childs[0];
            this._dist = this._city.childs[0];
        }else{
            this.formatArea(this.data.value);
        }
        this.setValue();
    }
    private setValue(){
        this.data.value = this._dist.id;
    }
    private formatArea(areaid){
        var provid = areaid.substr(0,2);
        var cityid = areaid.substr(2,2);
        var distid = areaid.substr(4,2);
        if(provid.length<2) this._prov = this._area[0];
        else {
            for (var v of this._area) {
                if (v.id == provid) this._prov = v;
            }
        }
        if(cityid.length<2) this._city = this._prov.childs[0];
        else {
            for (var v of this._prov.childs) {
                if (v.id == cityid) this._city = v;
            }
        }
        if(distid.length<2) this._dist = this._city.childs[0];
        else {
            for (var v of this._city.childs) {
                if (v.id == distid) this._dist = v;
            }
        }
    }
}