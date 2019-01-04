import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from  "@angular/router";
import { OrderService } from "../services/order.service";

@Injectable()
export class OrderDetailsGuard implements CanActivate {

    constructor(private _orderService: OrderService) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
            return this._orderService.cartItems.length != 0;
        }
}