import { Component, OnInit} from '@angular/core';
import { ProductsService } from '../services/products.service';
import { Product } from '../models/product.model';
import { OrderService } from '../services/order.service';
     
@Component({
    selector: 'product-list',
    templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit { 
  
    private products: Product[];

    constructor(private productsService: ProductsService, private orderService: OrderService) { 
        
    }

    ngOnInit() { 
        this.productsService.getAll().subscribe(data => this.products = data);
    }

    private addToCart(product: Product){
        this.orderService.addToCart(product);
    }

    private ShowDescription(descriptionModal: Element, descriptionContent: Element, description: string){
        descriptionModal.classList.add('description-modal-show');
        if(description != null){
            descriptionContent.textContent = description;
        }
        else {
            descriptionContent.textContent = 'No descreption!';
        }
    }
}