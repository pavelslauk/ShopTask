import { Injectable } from "@angular/core";
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from  "@angular/router";
import { OrderService } from "../services/order.service";

@Injectable()
export class OrderDetailsGuard implements CanActivate {

    constructor(private _orderService: OrderService, private router: Router) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
            if (this._orderService.cartItems.length != 0) {
                return true;
            }
            else {
                this.router.navigateByUrl('/shoptask/Order');
            }
            return false;
        }
}