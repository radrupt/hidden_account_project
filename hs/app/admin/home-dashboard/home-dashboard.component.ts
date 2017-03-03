/**
 * Created by wangd on 2016/3/7.
 */
import {Component,View} from 'angular2/core';
import {Router,RouteConfig,ROUTER_DIRECTIVES,AsyncRoute,CanActivate} from 'angular2/router';
import {UserService } from '../../shared/services/user/user.service';
import {ConfigService } from '../../shared/services/config/config.service';
import {HomeComponent} from '../../csystem/components/home/home.component';
import {baseUrl} from '../../config';

@Component({
    selector: 'home-dashboard',
    templateUrl:'home-dashboard.component.html',
    styleUrls:['home-dashboard.component.css'],
    moduleId:__moduleName,
    directives:[ROUTER_DIRECTIVES],
    providers:[UserService,ConfigService]
})
@RouteConfig([
    {path: '/',name: 'Home',component: HomeComponent,useAsDefault: true},
    new AsyncRoute({
            path: '/shipper',
            name: 'Shipper',
            loader: () => System.import(baseUrl+'csystem/components/shipper/shipper.component')
                .then(m => m.ShipperComponent)
        }
    ),
    new AsyncRoute({
            path: '/driver',
            name: 'Driver',
            loader: () => System.import(baseUrl+'csystem/components/driver/driver.component')
                .then(m => m.DriverComponent)
        }
    ),
    new AsyncRoute({
            path: '/order',
            name: 'Order',
            loader: () => System.import(baseUrl+'csystem/components/order/order.component')
                .then(m => m.OrderComponent)
        }
    ),
])
export class HomeDashboardComponent{
    public leftMenu:Array = [];
    public productId:string = '';
    constructor(
        private userService: UserService,
        private configService: ConfigService,
        private router: Router
    ){
        this.leftMenu = this.configService.getLeftMenuConfig();
    }

    logout(){
        this.userService.logout();
        this.router.navigate(['Login']);
    }

    ngOnDestroy() {
        this.configService.setLeftMenuConfig(this.leftMenu);//save state
    }


}