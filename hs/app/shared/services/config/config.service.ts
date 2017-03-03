/**
 * Created by wangd on 2016/3/11.
 */
import { Injectable } from 'angular2/core';
import { Http, Headers } from 'angular2/http';
import {leftmenu} from '../../../config';

var localStorage  = window.localStorage;

@Injectable()
export class ConfigService {

    constructor(private http: Http) {
    }

    getLeftMenuConfig(){
        if(leftmenu.flag) localStorage.removeItem(leftmenu.keyname);
        return !!localStorage.getItem(leftmenu.keyname) ?
                Object.assign(JSON.parse(localStorage.getItem('left_menu')),leftmenu.data) : leftmenu.data
                ;
    }

    setLeftMenuConfig(_leftmenu){
        localStorage.setItem('left_menu',JSON.stringify(_leftmenu));
    }
}