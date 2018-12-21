import { Injectable } from "@angular/core";
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';
import { Observable, of } from 'rxjs';

@Injectable()
export class CartService {

    private _cartItems: CartItem[] = [];
    private _cartItemsObservable = of(this._cartItems);

    public get cartItemsObservable(): Observable<CartItem[]> {
        return this._cartItemsObservable;
    }

    public get cartItems(): CartItem[] {
        return this._cartItems;
    }

    constructor() { }

    public addToCart(product: Product){
        var cartItem = this.cartItems.find(item => item.product == product);
        if (!cartItem){
            cartItem = new CartItem(product);
            this.cartItems.push(cartItem);
        } else {
            cartItem.productsCount++;
        }
    }

    public removeFromCart(cartItem: CartItem) {
        this._cartItems.splice(this._cartItems.indexOf(cartItem), 1);
    };

    public totalCartPrice(){
        var total = 0;
        this.cartItems.forEach(item => total += item.totalPrice);
        return total.toFixed(2);
    }
}