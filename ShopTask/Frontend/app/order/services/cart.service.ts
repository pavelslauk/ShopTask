import { Injectable } from "@angular/core";
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { WindowRef } from "./windowRef";

@Injectable()
export class CartService {

    private _cartItems: CartItem[] = [];
    private _cartItemsBehaviorSubject = new BehaviorSubject(this.cartItems);

    public get cartItemsBehaviorSubject(): BehaviorSubject<CartItem[]> {
        return this._cartItemsBehaviorSubject;
    }

    public get cartItems(): CartItem[] {
        return this._cartItems;
    }

    constructor(private _http: HttpClient, private windowRef: WindowRef) {         
        this.GetCartItemsFromSession();
    }

    public addToCart(product: Product){
        var cartItem = this.cartItems.find(item => item.product == product);
        if (!cartItem){
            cartItem = new CartItem(product);
            this.cartItems.push(cartItem);
        } else {
            cartItem.productsCount++;
        }
        this.saveCartItemsToSession();
    }

    public removeFromCart(cartItem: CartItem) {
        this._cartItems = this.cartItems.filter(item => item != cartItem);
        this.cartItemsBehaviorSubject.next(this.cartItems);
        this.saveCartItemsToSession();
    };

    public increaseItemCount(cartItem: CartItem) {
        cartItem.productsCount++;
        this.saveCartItemsToSession();
    };

    public decreaseItemCount (cartItem: CartItem) {
        if (--cartItem.productsCount == 0) {
            this.removeFromCart(cartItem);
        }
        this.saveCartItemsToSession();
    };

    public clearCart() {
        this._cartItems = [];
        this.cartItemsBehaviorSubject.next(this.cartItems);
        this.saveCartItemsToSession();
    }

    public totalCartPrice(){
        var total = 0;
        this.cartItems.forEach(item => total += item.totalPrice);
        return total.toFixed(2);
    }

    public saveCartItemsToSession() {
        this._http.post(this.windowRef.nativeWindow.apiRootUrl + '/Order/SaveCart',
            {cart: JSON.stringify(this._cartItems)}).subscribe();
    }

    private GetCartItemsFromSession() {
        this._http.get(this.windowRef.nativeWindow.apiRootUrl + '/Order/GetCart')
        .subscribe(data => this.OnCartGetted(data));      
    }

    private OnCartGetted(data: object) {
        var cartItemsJSON = data as Array<any>;
        if(cartItemsJSON){
            this._cartItems = cartItemsJSON.map(data=>{
                var product = new Product({
                    Title: data._product._title,
                    Price: data._product._price,
                    Category: data._product._category,
                    Description: data._product._description
                })
                var cartItem = new CartItem(product);
                cartItem.productsCount = data._productsCount;
                return cartItem;
            });
        }
        else {
            this._cartItems = [];
        }
        this.cartItemsBehaviorSubject.next(this.cartItems);
    }
}