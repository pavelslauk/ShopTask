import { Component, OnInit} from '@angular/core';
import { ProductsService } from '../services/products.service';
import { Product } from '../models/product.model';
import { CartService } from '../services/cart.service';
     
@Component({
    selector: 'product-list',
    templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit { 
  
    private _products: Product[];

    public get products(){
        return this._products;
    }

    constructor(private _productsService: ProductsService, private _cartService: CartService) { 
        
    }

    ngOnInit() { 
        this._productsService.getAll().subscribe(data => this._products = data);
    }

    private addToCart(product: Product){
        this._cartService.addToCart(product);
    }

    private showDescription(descriptionModal: Element, descriptionContent: Element, description: string){
        descriptionModal.classList.add('description-modal-show');
        if(description != null){
            descriptionContent.textContent = description;
        }
        else {
            descriptionContent.textContent = 'No descreption!';
        }
    }
}