import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { WindowRef } from "./windowRef";
import { OrderDetails } from "../models/order-details.model";
import { CartService } from "./cart.service";

@Injectable()
export class OrderService {

    constructor(private _http: HttpClient, private _windowRef: WindowRef, private _cartService: CartService) { }

    public saveOrder(orderDetails: OrderDetails) {
        this._http.post(this._windowRef.nativeWindow.apiRootUrl + '/Order/SaveOrder',
            {orderModel: this.getMappedOrderDetails(orderDetails)})
            .subscribe(result => {if(result) this._cartService.clearCart()});
    }

    private getMappedOrderDetails(orderDetails: OrderDetails){
        return {
            name: orderDetails.name,
            surname: orderDetails.surname,
            address: orderDetails.address,
            phone: orderDetails.phone,
            comments: orderDetails.comments
        }
    }
}