import { Component, OnInit} from '@angular/core';
import { ProductsService } from '../services/products.service';
import { Product } from '../models/product.model';
import { CartService } from '../services/cart.service';
     
@Component({
    selector: 'product-list',
    templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit { 
  
    private products: Product[];

    constructor(private productItems: ProductsService, private cart: CartService) { }

    ngOnInit() { 
        this.productItems.getAll().subscribe(data => this.products = data);
    }

    private addToCart(product: Product){
        this.cart.addToCart(product);
    }
}