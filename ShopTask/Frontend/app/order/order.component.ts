import { Component, OnInit } from '@angular/core';
import { CartItem } from './models/cart-item.model';
     
@Component({
    selector: 'order',
    templateUrl: './order.component.html'
})
export class OrderComponent implements OnInit { 
  
    private cartItems: CartItem[] = [];

    constructor() { }

    ngOnInit() { }

    public addToCart(cartItem: CartItem){
        if (this.cartItems.indexOf(cartItem) == -1){
        this.cartItems.push(cartItem);
        } else {
            this.increaseItemCount(cartItem);
        }
    }

    private removeFromCart(cartItem: CartItem) {
        this.cartItems = this.cartItems.filter(item => item != cartItem);
        cartItem.productsCount = 1;
    };

    private increaseItemCount(cartItem: CartItem) {
        cartItem.productsCount++;
    };

    private decreaseItemCount (cartItem: CartItem) {
        cartItem.productsCount--;
        if (cartItem.productsCount == 0) {
            this.removeFromCart(cartItem);
        }
    };

    private totalCartPrice(){
        var total = 0;
        this.cartItems.forEach(item => total += item.totalPrice);
        return total.toFixed(2);
    }
}