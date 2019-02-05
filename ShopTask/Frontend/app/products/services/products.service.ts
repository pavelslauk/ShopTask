import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Product } from '../models/product.model';
import { WindowRef } from "./windowRef";

@Injectable()
export class ProductsService {

    private _products: Product[];
    private _productsBehaviorSubject = new BehaviorSubject(this.products);
    private _categoryFilter: string;

    public get categoryFilter(){
        return this._categoryFilter;
    }

    public set categoryFilter(value: string) {
        this._categoryFilter = value;
        this.refreshProducts();
    }

    public get productsBehaviorSubject(): BehaviorSubject<Product[]> {
        return this._productsBehaviorSubject;
    }

    public get products(){
        return this._products;
    }

    constructor(private _http: HttpClient, private _windowRef: WindowRef) {
        this.refreshProducts();
    }

    public findById(id: number) {
        return this.products.find(item => item.id == id);
    }

    public refreshProducts() {
        this.getAllProducts().subscribe(data => this.setProducts(data)); 
    }

    private setProducts(products: Product[]) {
        this._products = products;
        if(this.categoryFilter) {
            this.productsBehaviorSubject.next(this._products.filter(item => item.category == this.categoryFilter));
        }
        else {
            this.productsBehaviorSubject.next(this._products);
        } 
    }

    private getAllProducts() : Observable<Product[]> {
        return this._http.get(this._windowRef.nativeWindow.apiRootUrl + '/Base/GetProducts').pipe(map(data=>{
            var products = data as object[];
            return products.map(function(item: object) {
                return new Product(item);
              });
        }));
    }



}