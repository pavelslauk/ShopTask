import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { APP_BASE_HREF } from '@angular/common';

import { ProductListComponent }   from './product-list/product-list.component';
import { ProductsService } from './services/products.service';
import { OrderComponent } from './order.component';
import { CartService } from './services/cart.service';
import { CartComponent } from './cart/cart.component';
import { OrderCreationComponent } from './order-creation/order-creation.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { OrderDetailsService } from './services/order-details.service';

const routes = [
    { path: 'shoptask/Order', component: OrderCreationComponent},
    { path: 'shoptask/Order/OrderDetail', component: OrderDetailsComponent}
]

@NgModule({
    imports:      [ BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(routes), FormsModule, ReactiveFormsModule],
    declarations: [ ProductListComponent, OrderComponent, CartComponent, OrderCreationComponent, OrderDetailsComponent ],
    bootstrap:    [ OrderComponent ],
    providers:    [ ProductsService, CartService, OrderDetailsService, {provide: APP_BASE_HREF, useValue : '' } ]
})
export class AppModule { }