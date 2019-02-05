import { Component, OnInit } from '@angular/core';
import { Product } from "../models/product.model"
import { CategoriesService } from '../services/categories.service';
import { ProductsService } from '../services/products.service';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ProductManageService } from '../services/product-manage.service';
     
@Component({
    selector: 'product-list',
    templateUrl: './product-list.component.html',
})
export class ProductListComponent implements OnInit { 

    private _products: Observable<Product[]>;

    public get products(){
        return this._products;
    }

    constructor(private _productsService: ProductsService, private _productManageService: ProductManageService, 
        private activatedRoute: ActivatedRoute, private _categoriesService: CategoriesService) {
            this.activatedRoute.queryParams.subscribe(params => this.setCategoryFilterName(params['filterCategoryId']));
        }

    ngOnInit() {
        this._products = this._productsService.productsBehaviorSubject.asObservable();

    } 

    private deleteProduct(productId: number) {
        this._productManageService.deleteProduct(productId);
    }

    private setCategoryFilterName(categoryFilterId: number) {
        this._categoriesService.getAll().subscribe(data => {
            var category = data.find(item => item.id == categoryFilterId);
            if(category)
            this._productsService.categoryFilter = category.name});
    }


}