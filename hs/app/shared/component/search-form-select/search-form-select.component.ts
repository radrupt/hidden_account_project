/**
 * Created by wangd on 2016/3/11.
 */
import {Component,View,Input} from 'angular2/core';

@Component({
    selector: 'search-form-select',
    templateUrl:'search-form-select.component.html',
    styleUrls:['search-form-select.component.css'],
    moduleId:__moduleName,
})

export class SearchFormSelectComponent{
    @Input('data') data: Object;
    constructor(
    ){
    }
    ngOnInit() {
        console.log(this.data);
    }
}