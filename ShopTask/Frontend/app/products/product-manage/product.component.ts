import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl } from "@angular/forms";
import { Router } from '@angular/router';
import { Product } from "../models/product.model"
import { ProductsManagementService } from '../services/products-management.service';
import { CategoriesService } from '../services/categories.service';
import { Category } from '../models/category.model';
import { ProductsService } from '../services/products.service';
     
@Component({
    selector: 'product',
    templateUrl: './product.component.html'
})
export class ProductComponent implements OnInit { 

    private _product = new Product();
    private _formGroup: FormGroup;
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

    public get formGroup(): FormGroup {
        return this._formGroup;
    }

    public get title(): AbstractControl {
        return this.formGroup.get('title');
    }

    public get category(): AbstractControl {
        return this.formGroup.get('category');
    }

    public get price(): AbstractControl {
        return this.formGroup.get('price');
    }

    public get description(): AbstractControl {
        return this.formGroup.get('description');
    }

    constructor(private _activatedRoute: ActivatedRoute, private _router: Router,
        private _productsManagementService: ProductsManagementService, private _categoriesService: CategoriesService,
        private _productsService: ProductsService) { }

     ngOnInit() { 
        this._activatedRoute.params.subscribe(params => {
            this._categoriesService.getAll().subscribe(data => {
                this._categories = data;
                this.setupForProductId(params['productId']);                  
            });
        }); 
     } 

    public saveData() {
        if(this.formGroup.valid) {
            this._productsManagementService.saveProduct(this.product)
                .subscribe(() => this._router.navigateByUrl('/shoptask/Inventory'));
        }
        else {
            this.formSubmitAttempted = true;
        }
    }

    private setupForProductId(productId: number) {
        if(productId) {
            this._productsService.getAllProducts().subscribe(products =>{
                this._product = products.find(item => item.id == productId);
                this.mapCategory();
                this.formGroupInitialize()
            });
        } else {
            this.formGroupInitialize();
        }
    }
    
    private mapCategory() {
        this.product.category = this.categories.find(item => item.name == this._product.category).id;
    }

    private formGroupInitialize() {
        this._formGroup = new FormGroup({
            title: new FormControl(this.product.title, Validators.required),
            category: new FormControl(this.product.category, Validators.required),
            price: new FormControl(this.product.price, [Validators.required, 
                Validators.pattern('((^[1-9][0-9]*(\.[0-9]*)?)|(^0\.(([0-9]*)?[1-9]([0-9]*)?)))$')]),
            description: new FormControl(this.product.description)
        })    
        this.formGroup.valueChanges.subscribe((value) => this.product.SetData(value));
    }
}