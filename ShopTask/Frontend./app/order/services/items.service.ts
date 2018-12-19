import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import {Observable} from 'rxjs';
import { map } from 'rxjs/operators';
import { CartItem } from '../models/cart-item.model';

@Injectable()
export class ItemsService {

    constructor(private _http: HttpClient) { }

    public getAll() : Observable<CartItem[]> {
        return this._http.get('/Order/GetProductsAsync').pipe(map(data=>{
            var cartItems = data as object[];
            return cartItems.map(function(item: object) {
                return new CartItem(item);
              });
        }));
    }

}