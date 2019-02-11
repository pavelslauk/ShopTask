import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Category } from '../models/category.model';
import { WindowRef } from "./windowRef";

@Injectable()
export class CategoriesService {

    constructor(private _http: HttpClient, private _windowRef: WindowRef) { }

    public getAll() : Observable<Category[]> {
        return this._http.get(this._windowRef.nativeWindow.apiRootUrl + '/Inventory/GetCategories').pipe(map(data=>{
            var categories = data as object[];
            return categories.map(function(item: object) {
                return new Category(item);
              });
        }));
    }

}