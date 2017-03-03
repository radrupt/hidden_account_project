/**
 * Created by wangd on 2016/3/9.
 */
import { Injectable } from 'angular2/core';
import { Http, Headers } from 'angular2/http';

var localStorage  = window.localStorage;

@Injectable()
export class UserService {
    private loggedIn = false;

    constructor(private http: Http) {
        this.loggedIn = !!localStorage.getItem('auth_token');
    }
    /*
     *function:user login
     *param:username,password(has been md5)
     *return:success:"success",fail:"failMessage"
     */
    login(username, password) {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        let data = { username:username, password:password };

        return this.http.post(
                '/root/login',
                JSON.stringify(data),
                { headers: headers }
                ).map(res => res.json())
                .map((res) => {
                    if (res.status == 'success') {
                        localStorage.setItem('auth_token', res.result.token);
                        localStorage.setItem('username', res.result.username);
                        this.loggedIn = true;
                        return 'success';
                    }else{
                        return res.failMessage;
                    }
                });
    }

    logout() {
        localStorage.removeItem('auth_token');
        localStorage.removeItem('username');
        this.loggedIn = false;
    }

    isLoggedIn() {
        return this.loggedIn;
    }

    getUsername(){
        return localStorage.getItem('username') || '';
    }
}