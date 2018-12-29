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

    constructor(private productsService: ProductsService, private cartService: CartService) { }

    ngOnInit() { 
        this.productsService.getAll().subscribe(data => this.products = data);
    }

    private addToCart(product: Product){
        this.cartService.addToCart(product);
    }

    private togglePopup(popup: Element, popupNotEmpty: boolean){
        if(popupNotEmpty){
            popup.classList.toggle('show');
        }      
    }

    
}