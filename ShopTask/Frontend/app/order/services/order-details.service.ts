import { Injectable} from "@angular/core";
import { OrdserDetails } from "../models/order-details.model";


@Injectable()
export class OrderDetailsService{

private _orderDetails: OrdserDetails = new OrdserDetails();

public get orderDetails(): OrdserDetails {
    return this._orderDetails;
}

public clearOrderDetails() {
    this._orderDetails = new OrdserDetails();
}

constructor() { }

}