import { Component, OnInit } from '@angular/core';
import { Product } from "../models/product.model"
import { Router } from '@angular/router';
import { ProductsService } from '../services/products.service';
import { ActivatedRoute } from '@angular/router';
import { ProductsManagementService } from '../services/products-management.service';
     
@Component({
    selector: 'product-list',
    templateUrl: './product-list.component.html',
})
export class ProductListComponent implements OnInit { 

    private _products: Product[] = [];

    public get products(){
        return this._products;
    }

    constructor(private _productsService: ProductsService, private _productsManagementService: ProductsManagementService, 
        private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.activatedRoute.queryParams.subscribe(params => this.changeProductsFilter(params['filterCategory']));
    } 

    private changeProductsFilter(categoryFilter: string) {
            this._productsService.getAllProducts().subscribe(products => {
                if(categoryFilter) {
                this._products = products.filter(item => item.category == categoryFilter)
                } else {
                    this._products = products;
                }
            });
    }

    private deleteProduct(productId: number) {
        this._productsManagementService.deleteProduct(productId).subscribe(result => {
            if(result) this._products = this.products.filter(item => item.id != productId);
        });
    }
}