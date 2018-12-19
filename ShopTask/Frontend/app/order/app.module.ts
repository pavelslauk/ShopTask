import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ProductListComponent }   from './product-list/product-list.component';
import { ItemsService } from './services/items.service';
import { OrderComponent } from './order.component';
@NgModule({
    imports:      [ BrowserModule, FormsModule, HttpClientModule ],
    declarations: [ ProductListComponent, OrderComponent ],
    bootstrap:    [ OrderComponent ],
    providers: [ ItemsService ]
})
export class AppModule { }