import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Product } from '../models/product.model';
import { WindowRef } from "./windowRef";

@Injectable()
export class ProductsService {

    constructor(private _http: HttpClient, private _windowRef: WindowRef) { }

    public getAll() : Observable<Product[]> {
        return this._http.get(this._windowRef.nativeWindow.apiRootUrl + '/Order/GetProducts').pipe(map(data=>{
            var products = data as object[];
            return products.map(function(item: object) {
                return new Product(item);
              });
        }));
    }

}