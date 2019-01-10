import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service';
     
@Component({
    selector: 'order-creation',
    templateUrl: './order-creation.component.html'
})
export class OrderCreationComponent implements OnInit { 

    constructor(private orderService: OrderService) { }

    ngOnInit() { }  
}