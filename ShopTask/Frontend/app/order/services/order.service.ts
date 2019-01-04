import { Injectable } from "@angular/core";
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';
import { BehaviorSubject } from 'rxjs';
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
        this._cartItems = this.cartItems.filter(item => item != cartItem);
        this.cartItemsBehaviorSubject.next(this.cartItems);
    };

    public clearCart() {
        this._cartItems = [];
        this.cartItemsBehaviorSubject.next(this.cartItems);
    }

    public totalCartPrice(){
        var total = 0;
        this.cartItems.forEach(item => total += item.totalPrice);
        return total.toFixed(2);
    }

    public clearOrderDetails() {
        this._orderDetails = new OrderDetails();
    }
}