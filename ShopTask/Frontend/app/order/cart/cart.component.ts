import { Component, OnInit } from '@angular/core';
import { CartItem } from '../models/cart-item.model';
import { CartService } from '../services/cart.service';
     
@Component({
    selector: 'cart',
    templateUrl: './cart.component.html'
})
export class CartComponent implements OnInit { 
  
    private cartItems: CartItem[] = [];

    constructor(private cart: CartService) { 
        this.cartItems = cart.cartItems;
    }

    ngOnInit() { }

    private removeFromCart(cartItem: CartItem) {
        this.cart.removeFromCart(cartItem);
        this.cartItems = this.cart.cartItems;
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
        var total = 0;
        this.cartItems.forEach(item => total += item.totalPrice);
        return total.toFixed(2);
    }
}