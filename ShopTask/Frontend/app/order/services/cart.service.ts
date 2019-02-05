import { Injectable } from "@angular/core";
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';
import { BehaviorSubject } from 'rxjs';
import { HttpClient} from "@angular/common/http";
import { WindowRef } from "./windowRef";
import { ProductsService } from '../services/products.service';

@Injectable()
export class CartService {

    private _cartItems: CartItem[] = [];
    private _cartItemsBehaviorSubject = new BehaviorSubject(this.cartItems);
    private _products: Product[];

    public get cartItemsBehaviorSubject(): BehaviorSubject<CartItem[]> {
        return this._cartItemsBehaviorSubject;
    }

    public get cartItems(): CartItem[] {
        return this._cartItems;
    }

    constructor(private _http: HttpClient, private _windowRef: WindowRef,private _productsService: ProductsService) {     
        this._productsService.getAll().subscribe(data => {this._products = data; this.refreshCart()});
        setInterval(() => this.refreshCart(), 500);
    }

    public addToCart(product: Product) {
        var cartItem = this.cartItems.find(item => item.product.id == product.id);
        if (!cartItem){
            cartItem = new CartItem(product);
            this.cartItems.push(cartItem);
        } else {
            cartItem.productsCount++;
        }
        this.saveCart();
    }

    public removeFromCart(cartItem: CartItem) {
        this.setCartItems(this.cartItems.filter(item => item != cartItem));
        this.saveCart();
    };

    public increaseItemCount(cartItem: CartItem) {
        cartItem.productsCount++;
        this.saveCart();
    };

    public decreaseItemCount (cartItem: CartItem) {
        if (--cartItem.productsCount == 0) {
            this.removeFromCart(cartItem);
        }
        this.saveCart();
    };

    public clearCart() {
        this.setCartItems([]);
        this.saveCart();
    }

    public totalCartPrice(){
        var total = 0;
        this.cartItems.forEach(item => total += item.totalPrice);
        return total.toFixed(2);
    }

    private saveCart() {
        this._http.post(this._windowRef.nativeWindow.apiRootUrl + '/Order/SaveCart',
            {cart: this.getMappedcartItems()}).subscribe();
    }

    private getMappedcartItems(){
        return this._cartItems.map(function(item: CartItem) {
            return {productId: item.product.id, count: item.productsCount}
          });
    }

    private refreshCart() {
        this._http.get(this._windowRef.nativeWindow.apiRootUrl + '/Order/GetCart')
        .subscribe(data => this.setCartItems(this.parseCartItems(data))); 
    }

    private setCartItems(cart: CartItem[]) {
        this._cartItems = cart;
        this.cartItemsBehaviorSubject.next(this._cartItems);
    }

    private parseCartItems(data: object) {
        if(!data){
            return [];
        }
        var cartItems = data as Array<any>;
             
        return cartItems.map(data => {
            var product = this._products.find(p => p.id == data.ProductId);
            if(product) {
            var cartItem = new CartItem(product);
            cartItem.productsCount = data.Count;
            return cartItem;
            }
        });
    }
}
