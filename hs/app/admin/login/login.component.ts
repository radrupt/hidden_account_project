/**
 * Created by wangd on 2016/3/7.
 */
import { Component } from 'angular2/core';
import { Router } from 'angular2/router';
import { UserService } from '../../shared/services/user/user.service';
import {md5} from '../../shared/util/md5';

@Component({
    selector: 'login',
    templateUrl: "login.component.html",
    styleUrls:['login.component.css'],
    moduleId:__moduleName,
    providers:[UserService]
})
export class LoginComponent {
    logining:boolean = false;
    errorMessage:string =  null;
    constructor(
        private userService: UserService,
        private router: Router
    ) { }

    onSubmit(username, password,event) {
        this.logining = true;
        this.userService.login(username, md5(password,'','')).subscribe((result) => {
            if (result == 'success') {
                this.router.navigate(['HomeDashboard']);
            }else{
                this.errorMessage = result;
            }
        });
    }
    inputChangeListen(){
        this.errorMessage = null;
        this.logining = false;
    }
}