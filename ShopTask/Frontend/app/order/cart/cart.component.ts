import { Component, OnInit } from '@angular/core';
import { CartItem } from '../models/cart-item.model';
import { CartService } from '../services/cart.service';
import { Observable } from 'rxjs';
     
@Component({
    selector: 'cart',
    templateUrl: './cart.component.html'
})
export class CartComponent implements OnInit { 

    private cartItems: Observable<CartItem[]>;

    constructor(private cartService: CartService) { }

    ngOnInit() {
        this.cartItems = this.cartService.cartItemsBehaviorSubject.asObservable();
    }

    private removeFromCart(cartItem: CartItem) {
        this.cartService.removeFromCart(cartItem);
    };

    private increaseItemCount(cartItem: CartItem) {
        this.cartService.increaseItemCount(cartItem);
    };

    private decreaseItemCount (cartItem: CartItem) {
        this.cartService.decreaseItemCount(cartItem);
    };

    private totalCartPrice(){
        return this.cartService.totalCartPrice();
    }
}