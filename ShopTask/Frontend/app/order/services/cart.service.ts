import { Injectable } from "@angular/core";
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';

@Injectable()
export class CartService {

    private _cartItems: CartItem[] = [];

    public get cartItems(): CartItem[] {
        return this._cartItems;
    }

    public set cartItems(value: CartItem[]) {
        this._cartItems = value;
    }

    constructor() { }

    public addToCart(product: Product){
        var cartItem = this.cartItems.find(item => item.title == product.title);
        if (!cartItem){
            cartItem = new CartItem(product);
            this.cartItems.push(cartItem);
        } else {
            cartItem.productsCount++;
        }
    }

    public removeFromCart(cartItem: CartItem) {
        this.cartItems = this.cartItems.filter(item => item != cartItem);
    };
}