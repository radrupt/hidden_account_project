/**
 * Created by wangd on 2016/3/14.
 */
import {Component,View,Input} from 'angular2/core';
import {SearchFormDateComponent} from '../search-form-date/search-form-date.component';

@Component({
    selector: 'search-form-date-range',
    templateUrl:'search-form-date-range.component.html',
    styleUrls:['search-form-date-range.component.css'],
    directives:[SearchFormDateComponent],
    moduleId:__moduleName,
})
/*
 * input:data:{start:{value:xxx},end:{value:xxx}}
 * output:data:{value:{start:xxx,end:xxx}}
 * */
export class SearchFormDateRangeComponent{
    @Input('data') data: Object;
    constructor(
    ){
    }
    ngOnInit() {
        console.log(this.data);
    }
}