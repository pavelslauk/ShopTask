import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from "@angular/forms";
import { OrderDetails } from '../models/order-details.model';
import { OrderService } from '../services/order.service';
import { Router } from '@angular/router';
     
@Component({
    selector: 'customer-data',
    templateUrl: './order-details.component.html'
})
export class OrderDetailsComponent implements OnInit { 

    private _orderDetailsControl: FormGroup;
    private _orderDetails: OrderDetails;

    public get orderDetailsControl(): FormGroup {
        return this._orderDetailsControl;
    }

    public get orderDetails(): OrderDetails {
        return this._orderDetails;
    }

    public get name(): AbstractControl {
        return this._orderDetailsControl.get('name');
    }

    public get surname(): AbstractControl {
        return this._orderDetailsControl.get('surname');
    }

    public get address(): AbstractControl {
        return this._orderDetailsControl.get('address');
    }

    public get phone(): AbstractControl {
        return this._orderDetailsControl.get('phone');
    }

    constructor(private _orderService: OrderService, private _router: Router) {
        this._orderDetails = _orderService.orderDetails;
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
            this._orderService.clearOrderDetails();
            this._orderService.clearCart();
            this._router.navigate(['/shoptask/Order']);
        }
    }

}