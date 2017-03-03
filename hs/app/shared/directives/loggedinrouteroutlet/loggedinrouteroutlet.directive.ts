/**
 * Created by wangd on 2016/3/10.
 */
import { ElementRef, DynamicComponentLoader, AttributeMetadata, Directive, Attribute } from 'angular2/core';
import { Router, RouterOutlet, ComponentInstruction } from 'angular2/router';
import { UserService } from '../../services/user/user.service';


@Directive({
    selector: 'router-outlet',
    providers:[UserService],
})
export class LoggedInRouterOutlet extends RouterOutlet {
    publicRoutes: Array;
    private parentRouter: Router;
    private userService: UserService;

    constructor(
        _elementRef: ElementRef, _loader: DynamicComponentLoader,
        _parentRouter: Router, @Attribute('name') nameAttr: string,
        private userService: UserService,private router: Router
    ) {
        super(_elementRef, _loader, _parentRouter, nameAttr);

        this.router = _parentRouter;
        this.publicRoutes = [
            '', 'login'
        ];
    }

    activate(instruction: ComponentInstruction) {
        if (this._canActivate(instruction.urlPath)) {
            return super.activate(instruction);
        }

        this.router.navigate(['Login']);
    }

    _canActivate(url) {
        return this.publicRoutes.indexOf(url) !== -1
            || this.userService.isLoggedIn();
    }
}