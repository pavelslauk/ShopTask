import { Component, OnInit } from '@angular/core';
import { OrderDetailsService } from '../services/order-details.service';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { OrdserDetails } from '../models/order-details.model';
import { CartService } from '../services/cart.service';
import { Router } from '@angular/router';
     
@Component({
    selector: 'customer-data',
    templateUrl: './order-details.component.html'
})
export class OrderDetailsComponent implements OnInit { 

    private _orderDetailsControl: FormGroup;
    private _orderDetails: OrdserDetails;
    private _errorMessage: string = '';

    public get errorMessage(): string {
        return this._errorMessage;
    }

    public set errorMessage(val : string) {
        this._errorMessage = val;
    }

    public get orderDetailsControl(): FormGroup {
        return this._orderDetailsControl;
    }

    public get orderDetails(): OrdserDetails {
        return this._orderDetails;
    }

    constructor(private _orderDetailsService: OrderDetailsService, private _cartService: CartService, private _router: Router) {
        this._orderDetails = _orderDetailsService.orderDetails;
        if(this._cartService.cartItems.length == 0){
            this._router.navigate(['/shoptask/Order']);
        }
    }

    ngOnInit() {
        this._orderDetailsControl = new FormGroup({
            name: new FormControl(this.orderDetails.name, Validators.required),
            surname: new FormControl(this.orderDetails.surname, Validators.required),
            address: new FormControl(this.orderDetails.address, Validators.required),
            phone: new FormControl(this.orderDetails.phone, Validators.required),
            comments: new FormControl(this.orderDetails.comments)
        })

        this._orderDetailsControl.valueChanges.subscribe((value) => this.orderDetails.SetData(value));
    }

    public saveData() {
        if(this._orderDetailsControl.valid) {
            this._orderDetailsService.clearOrderDetails();
            this._cartService.clearCart();
            this._router.navigate(['/shoptask/Order']);
        }
        else {
            this.errorMessage = "Fields Name, Surname, Address, Phone are requred";
        }
    }

}