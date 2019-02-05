import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from "@angular/forms";
import { OrderDetails } from '../models/order-details.model';
import { CartService } from '../services/cart.service';
import { Router } from '@angular/router';
import { OrderService } from '../services/order.service';
     
@Component({
    selector: 'customer-data',
    templateUrl: './order-details.component.html'
})
export class OrderDetailsComponent implements OnInit { 

    private _orderDetailsControl: FormGroup;
    private _orderDetails: OrderDetails;
    private _formSubmitAttempted: boolean = false;

    public get formSubmitAttempted(): boolean {
        return this._formSubmitAttempted;
    }

    public set formSubmitAttempted(val : boolean) {
        this._formSubmitAttempted = val;
    }

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

    constructor(private _cartService: CartService, private _router: Router, private _orderService: OrderService) {
        this._orderDetails = this.getOrderDetailsLocal();
    }

    ngOnInit() {
        this._orderDetailsControl = new FormGroup({
            name: new FormControl(this.orderDetails.name, Validators.required),
            surname: new FormControl(this.orderDetails.surname, Validators.required),
            address: new FormControl(this.orderDetails.address, Validators.required),
            phone: new FormControl(this.orderDetails.phone, Validators.required),
            comments: new FormControl(this.orderDetails.comments)
        })

        this._orderDetailsControl.valueChanges.subscribe((value) => this.saveOrderDetailsLocal(value));
    }

    public saveData() {
        if(this.orderDetailsControl.valid) {
            this._orderService.saveOrder(this._orderDetails);
            this._router.navigateByUrl('/shoptask/Order');
        }
        else {
            this._formSubmitAttempted = true;
        }
    }

    private saveOrderDetailsLocal(value: any) {
        this.orderDetails.SetData(value);
        localStorage.setItem('OrderDetails', JSON.stringify(this.orderDetails));
    }

    private getOrderDetailsLocal() {
        var orderDetails = new OrderDetails();
        var orderDetailsJSON = JSON.parse(localStorage.getItem('OrderDetails'));
        if(orderDetailsJSON){
            orderDetails.SetData({
                name: orderDetailsJSON._name,
                surname: orderDetailsJSON._surname,
                address: orderDetailsJSON._address,
                phone: orderDetailsJSON._phone,
                comments: orderDetailsJSON._comments
            });
        }
        return orderDetails;
    }
}