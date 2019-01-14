import { Injectable } from "@angular/core";
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from  "@angular/router";
import { CartService } from "../services/cart.service";

@Injectable()
export class OrderDetailsGuard implements CanActivate {

    constructor(private _cartService: CartService, private router: Router) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
            if (this._cartService.cartItems.length != 0) {
                return true;
            }
            else {
                this.router.navigateByUrl('/shoptask/Order');
            }
            return false;
        }
}