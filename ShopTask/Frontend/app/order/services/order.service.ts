import { Injectable } from "@angular/core";
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { OrderDetails } from "../models/order-details.model";

@Injectable()
export class OrderService {

    private _cartItems: CartItem[] = [];
    private _cartItemsBehaviorSubject = new BehaviorSubject(this.cartItems);
    private _orderDetails: OrderDetails = new OrderDetails();

    public get cartItemsBehaviorSubject(): BehaviorSubject<CartItem[]> {
        return this._cartItemsBehaviorSubject;
    }

    public get cartItems(): CartItem[] {
        return this._cartItems;
    }

    public get orderDetails(): OrderDetails {
        return this._orderDetails;
    }

    constructor() { 
        this._cartItems = this.GetCartItemsFromSession();

        this.cartItemsBehaviorSubject.next(this.cartItems);
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

    public clearOrderDetails() {
        this._orderDetails = new OrderDetails();
    }

    public saveCartItemsToSession() {
        sessionStorage.setItem('CartItems', JSON.stringify(this._cartItems));
    }

    private GetCartItemsFromSession() {
        var cartItems = JSON.parse(sessionStorage.getItem('CartItems')) as Array<any>;
        if(cartItems){
            return cartItems.map(data=>{
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
            return [];
        }
    }
}