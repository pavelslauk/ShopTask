import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { WindowRef } from "./windowRef";
import { CartService } from "./cart.service";
import { OrderDetails } from "../models/order-details.model";
import { CartItem } from "../models/cart-item.model";

@Injectable()
export class OrderService {

    constructor(private _http: HttpClient, private _windowRef: WindowRef, private _cartService: CartService) { }

    public SaveOrder(orderDetails: OrderDetails) {
        this._http.post(this._windowRef.nativeWindow.apiRootUrl + '/Order/SaveOrder',
            {data: JSON.stringify({order: this.GetMappedOrderDetails(orderDetails),
                cart: this.GetMappedCartItems()})}).subscribe();
    }

    private GetMappedOrderDetails(orderDetails: OrderDetails){
        return {
            name: orderDetails.name, 
            surname: orderDetails.surname,
            address: orderDetails.address,
            phone: orderDetails.phone,
            comments: orderDetails.comments
        }
    }

    private GetMappedCartItems(){
    return this._cartService.cartItems.map(function(item: CartItem) {
        return {productId: item.product.id, orderPrice: item.product.price, count: item.productsCount};
      });
    }

}