/**
 * Created by wangd on 2016/3/7.
 */
import {Component} from 'angular2/core';
import {RouteConfig,ROUTER_DIRECTIVES,AsyncRoute} from 'angular2/router';
import {HomeDashboardComponent} from './admin/home-dashboard/home-dashboard.component';
import {LoggedInRouterOutlet} from './shared/directives/loggedinrouteroutlet/loggedinrouteroutlet.directive';
import {baseUrl} from './config';

@Component({
    selector:'App',
    template:`<router-outlet></router-outlet>`,
    directives:[LoggedInRouterOutlet],
})
@RouteConfig([
    {
        path: '/...',
        name: 'HomeDashboard',
        component: HomeDashboardComponent,
        useAsDefault: true
    },
    new AsyncRoute({
            path: '/login',
            name: 'Login',
            loader: () => System.import(baseUrl+'admin/login/login.component')
                .then(m => m.LoginComponent)
        }
    ),
])

export class AppComponent {}