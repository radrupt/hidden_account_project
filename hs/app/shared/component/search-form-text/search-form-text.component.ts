/**
 * Created by wangd on 2016/3/11.
 */
import {Component,View,Input} from 'angular2/core';


@Component({
    selector: 'search-form-text',
    templateUrl:'search-form-text.component.html',
    styleUrls:['search-form-text.component.css'],
    moduleId:__moduleName,
})

export class SearchFormTextComponent{
    @Input('data') data: Object;
    constructor(
    ){
    }
}