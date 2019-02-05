import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { WindowRef } from "./windowRef";
import { Product } from "../models/product.model";
import { ProductsService } from "./products.service";

@Injectable()
export class ProductManageService {

    constructor(private _http: HttpClient, private _windowRef: WindowRef, private _productService: ProductsService) { }

    public saveNewProduct(product: Product) {
        this._http.post(this._windowRef.nativeWindow.apiRootUrl + '/Inventory/SaveNewProduct',
            {newProduct: this.getPostModel(product)}).subscribe(result => {if(result) this._productService.refreshProducts()});
    }

    public saveChangedProduct(product: Product) {
        this._http.post(this._windowRef.nativeWindow.apiRootUrl + '/Inventory/SaveChangedProduct',
            {changetProduct: this.getPostModel(product)}).subscribe(result => {if(result) this._productService.refreshProducts()});
    }

    public deleteProduct(productId: number) {
        this._http.post(this._windowRef.nativeWindow.apiRootUrl + '/Inventory/DeleteProduct',
            {productId: productId}).subscribe(result => {if(result) this._productService.refreshProducts()});
    }

    private getPostModel(product: Product){
        return {
            id: product.id,
            title: product.title,
            price: product.price,
            description: product.description,
            categoryId: product.category
        }
    }
}