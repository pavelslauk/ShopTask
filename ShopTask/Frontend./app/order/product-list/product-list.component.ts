import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ItemsService } from '../services/items.service';
import { CartItem } from '../models/cart-item.model';
     
@Component({
    selector: 'product-list',
    templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit { 
  
    private products: CartItem[];

    @Output() addToCartClick = new EventEmitter<object>();

    constructor(private cartItems: ItemsService) { }

    ngOnInit() { 
        this.cartItems.getAll().subscribe(data => this.products = data);
    }

    private addToCart(product: CartItem){
        this.addToCartClick.emit(product);
    }
}