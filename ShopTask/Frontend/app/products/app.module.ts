import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { APP_BASE_HREF } from '@angular/common';

import { ProductListComponent } from './product-list/product-list.component'
import { ProductsComponent } from './products.component';
import { ProductComponent } from './product-manage/product.component';
import { ProductsManagementService } from './services/products-management.service'
import { CategoriesService } from './services/categories.service'
import { ProductsService } from './services/products.service'
import { WindowRef } from './services/windowRef'

const routes = [
    { path: 'shoptask', component: ProductListComponent},
    { path: 'shoptask/Inventory', component: ProductListComponent},
    { path: 'shoptask/Inventory/Product', component: ProductComponent},
    { path: 'shoptask/Inventory/Product/:productId', component: ProductComponent}
]

@NgModule({
    imports:      [ BrowserModule, FormsModule, HttpClientModule, FormsModule, ReactiveFormsModule, RouterModule.forRoot(routes) ],
    declarations: [ ProductsComponent, ProductComponent, ProductListComponent ],
    bootstrap:    [ ProductsComponent ],
    providers:    [ ProductsManagementService, ProductsService, CategoriesService, WindowRef, {provide: APP_BASE_HREF, useValue : '' } ]
})

export class AppModule { }