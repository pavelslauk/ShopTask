import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { APP_BASE_HREF } from '@angular/common';

import { ProductListComponent } from './product-list/product-list.component'
import { ProductsComponent } from './products.component';
import { EditProductComponent } from './product-manage/edit-product.component';
import { CreateProductComponent } from './product-manage/create-product.component';
import { ProductManageService } from './services/product-manage.service'
import { CategoriesService } from './services/categories.service'
import { ProductsService } from './services/products.service'
import { WindowRef } from './services/windowRef'

const routes = [
    { path: 'shoptask', component: ProductListComponent},
    { path: 'shoptask/Inventory', component: ProductListComponent},
    { path: 'shoptask/Inventory/Product', component: CreateProductComponent},
    { path: 'shoptask/Inventory/Product/:productId', component: EditProductComponent}
]

@NgModule({
    imports:      [ BrowserModule, FormsModule, HttpClientModule, FormsModule, ReactiveFormsModule, RouterModule.forRoot(routes) ],
    declarations: [ ProductsComponent, EditProductComponent, CreateProductComponent, ProductListComponent ],
    bootstrap:    [ ProductsComponent ],
    providers:    [ ProductManageService, ProductsService, CategoriesService, WindowRef, {provide: APP_BASE_HREF, useValue : '' } ]
})

export class AppModule { }