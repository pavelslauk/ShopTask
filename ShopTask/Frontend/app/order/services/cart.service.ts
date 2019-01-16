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
        setInterval(() => this._refreshCart(), 500);
    }

    public addToCart(product: Product) {
        var cartItem = this.cartItems.find(item => item.product.id == product.id);
        if (!cartItem){
            cartItem = new CartItem(product);
            this.cartItems.push(cartItem);
        } else {
            cartItem.productsCount++;
        }
        this._saveCart();
    }

    public removeFromCart(cartItem: CartItem) {
        this._setCartItems(this.cartItems.filter(item => item != cartItem));
        this._saveCart();
    };

    public increaseItemCount(cartItem: CartItem) {
        cartItem.productsCount++;
        this._saveCart();
    };

    public decreaseItemCount (cartItem: CartItem) {
        if (--cartItem.productsCount == 0) {
            this.removeFromCart(cartItem);
        }
        this._saveCart();
    };

    public clearCart() {
        this._setCartItems([]);
        this._saveCart();
    }

    public totalCartPrice(){
        var total = 0;
        this.cartItems.forEach(item => total += item.totalPrice);
        return total.toFixed(2);
    }

    private _saveCart() {
        this._http.post(this.windowRef.nativeWindow.apiRootUrl + '/Order/SaveCart',
            {cart: JSON.stringify(this._cartItems)}).subscribe();
    }

    private _refreshCart() {
        this._http.get(this.windowRef.nativeWindow.apiRootUrl + '/Order/GetCart')
        .subscribe(data => this._setCartItems(this._parseCartItems(data)));      
    }

    private _setCartItems(cart: CartItem[]) {
        this._cartItems = cart;
        this.cartItemsBehaviorSubject.next(this._cartItems);
    }

    private _parseCartItems(data: object) {
        var cartItemsJSON = data as Array<any>;
        if(!cartItemsJSON){
            return [];
        }
        return cartItemsJSON.map(data => {
            var product = new Product({
                Id: data._product._id,
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
}
