import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Product } from '../models/product.model';

@Injectable()
export class ProductsService {

    constructor(private _http: HttpClient) { }

    public getAll() : Observable<Product[]> {
        return this._http.get('/shoptask/Order/GetProductsAsync').pipe(map(data=>{
            var products = data as object[];
            return products.map(function(item: object) {
                return new Product(item);
              });
        }));
    }

}