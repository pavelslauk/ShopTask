import { Component, OnInit } from '@angular/core';
import { CartItem } from '../models/cart-item.model';
import { OrderService } from '../services/order.service';
import { Observable } from 'rxjs';
     
@Component({
    selector: 'cart',
    templateUrl: './cart.component.html'
})
export class CartComponent implements OnInit { 

    private cartItems: Observable<CartItem[]>;

    constructor(private orderService: OrderService) { }

    ngOnInit() {
        this.cartItems = this.orderService.cartItemsBehaviorSubject.asObservable();
    }

    private removeFromCart(cartItem: CartItem) {
        this.orderService.removeFromCart(cartItem);
    };

    private increaseItemCount(cartItem: CartItem) {
        cartItem.productsCount++;
    };

    private decreaseItemCount (cartItem: CartItem) {
        if (--cartItem.productsCount == 0) {
            this.removeFromCart(cartItem);
        }
    };

    private totalCartPrice(){
        return this.orderService.totalCartPrice();
    }
}