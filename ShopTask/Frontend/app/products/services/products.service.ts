import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Product } from '../models/product.model';
import { WindowRef } from "./windowRef";

@Injectable()
export class ProductsService {

    constructor(private _http: HttpClient, private _windowRef: WindowRef) { }

    public getAllProducts() : Observable<Product[]> {
        return this._http.get(this._windowRef.nativeWindow.apiRootUrl + '/Base/GetProducts').pipe(map(data=>{
            var products = data as object[];
            return products.map(function(item: object) {
                var product = new Product();
                product.InitializeProduct(item);
                return product;
              });
        }));
    }



}