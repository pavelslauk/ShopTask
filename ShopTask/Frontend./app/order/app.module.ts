import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ProductListComponent }   from './product-list/product-list.component';
import { ProductsService } from './services/products.service';
import { OrderComponent } from './order.component';
import { CartService } from './services/cart.service';
import { CartComponent } from './cart/cart.component';

@NgModule({
    imports:      [ BrowserModule, FormsModule, HttpClientModule ],
    declarations: [ ProductListComponent, OrderComponent, CartComponent ],
    bootstrap:    [ OrderComponent ],
    providers: [ ProductsService, CartService ]
})
export class AppModule { }