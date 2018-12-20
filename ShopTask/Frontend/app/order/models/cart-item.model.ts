import { Product } from './product.model';

export class CartItem extends Product {

    private _productsCount: number;
    private _totalPrice: number;

    public get productsCount(): number {
        return this._productsCount;
    }
 
    public set productsCount(c: number) {
        this._productsCount = c;
        this._totalPrice = Number((this.price * this._productsCount).toFixed(2));
    }

    public get totalPrice(): number {
        return this._totalPrice;
    }

    constructor(product: Product){
        super(product);
        this._productsCount = 1;
        this._totalPrice = product.price;
    }
};