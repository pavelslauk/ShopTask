import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { WindowRef } from "./windowRef";
import { Product } from "../models/product.model";

@Injectable()
export class ProductsManagementService {

    private _editableProduct: Product = new Product;

    public get editableProduct() : Product {
        var product = this._editableProduct;
        this._editableProduct = new Product;
        return product;
    }

    public set editableProduct( value: Product) {
        this._editableProduct = value;
    }

    constructor(private _http: HttpClient, private _windowRef: WindowRef) { }

    public saveProduct(product: Product) {
        return this._http.post(this._windowRef.nativeWindow.apiRootUrl + '/Inventory/SaveProduct',
            {productModel: this.getPostModel(product)});
    }

    public deleteProduct(productId: number) {
        return this._http.post(this._windowRef.nativeWindow.apiRootUrl + '/Inventory/DeleteProduct',
            {productId: productId});
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