import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from "@angular/forms";
import { Router } from '@angular/router';
import { Product } from "../models/product.model"
import { ProductManageService } from '../services/product-manage.service';
import { CategoriesService } from '../services/categories.service';
import { Category } from '../models/category.model';
     
@Component({
    selector: 'create-product',
    templateUrl: './product.component.html'
})
export class CreateProductComponent implements OnInit { 

    private _product = new Product(null);
    private _productControl: FormGroup;
    private _formSubmitAttempted: boolean = false;
    private _categories: Category[];

    public get categories(){
        return this._categories;
    }

    public get formSubmitAttempted(): boolean {
        return this._formSubmitAttempted;
    }

    public set formSubmitAttempted(val : boolean) {
        this._formSubmitAttempted = val;
    }

    public get product() : Product {
        return this._product;
    }

    public get productControl(): FormGroup {
        return this._productControl;
    }

    public get title(): AbstractControl {
        return this.productControl.get('title');
    }

    public get category(): AbstractControl {
        return this.productControl.get('category');
    }

    public get price(): AbstractControl {
        return this.productControl.get('price');
    }

    public get description(): AbstractControl {
        return this.productControl.get('description');
    }

    constructor(private _router: Router, private _productManageService: ProductManageService, 
        private _categoriesService: CategoriesService) { }

    ngOnInit() {
        this._productControl = new FormGroup({
            title: new FormControl(this.product.title, Validators.required),
            category: new FormControl(this.product.category, Validators.required),
            price: new FormControl(this.product.price, [Validators.required, 
                Validators.pattern('((^[1-9][0-9]*(\.[0-9]*)?)|(^0\.(([0-9]*)?[1-9]([0-9]*)?)))$')]),
            description: new FormControl(this.product.description)
        })
        this._productControl.valueChanges.subscribe((value) => this.product.SetData(value));
        this._categoriesService.getAll().subscribe(data => this._categories = data);
    } 



    public saveData() {
        if(this.productControl.valid) {
            this._productManageService.saveNewProduct(this.product);
            this._router.navigateByUrl('/shoptask/Inventory');
        }
        else {
            this.formSubmitAttempted = true;
        }
    }
}