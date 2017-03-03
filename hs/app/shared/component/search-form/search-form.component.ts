/**
 * Created by wangd on 2016/3/11.
 */
import {Component,Directive,View,Input} from 'angular2/core';
import {SearchFormTextComponent} from '../search-form-text/search-form-text.component';
import {SearchFormSelectComponent} from '../search-form-select/search-form-select.component';
import {SearchFormDateComponent} from '../search-form-date/search-form-date.component';
import {SearchFormDateRangeComponent} from '../search-form-date-range/search-form-date-range.component';
import {SearchFormAreaComponent} from '../search-form-area/search-form-area.component';
export function trans(input){
    var tem = {};console.log(input)
    for(var v of input){
        tem[v.key] = v.value;
    }
    return tem;
}

@Component({
    selector: 'search-form',
    templateUrl:'search-form.component.html',
    styleUrls:['search-form.component.css'],
    directives:[SearchFormTextComponent,SearchFormSelectComponent,SearchFormDateComponent,SearchFormDateRangeComponent,SearchFormAreaComponent],
    moduleId:__moduleName,
})

export class SearchFormComponent{
    @Input('config') config: Array;
    @Input('callback') callback: Function;
    constructor(
    ){
    }
    search(){
        this.callback(trans(this.config));
    }
}