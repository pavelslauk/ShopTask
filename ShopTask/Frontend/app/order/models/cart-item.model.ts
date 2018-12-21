import { Product } from './product.model';

export class CartItem {

    private _product: Product;
    private _productsCount: number;
    private _totalPrice: number;

    public get product(): Product {
        return this._product;
    }

    public get productsCount(): number {
        return this._productsCount;
    }
 
    public set productsCount(c: number) {
        this._productsCount = c;
        this._totalPrice = Number((this.product.price * this._productsCount).toFixed(2));
    }

    public get totalPrice(): number {
        return this._totalPrice;
    }

    constructor(product: Product){
        this._product = product;
        this._productsCount = 1;
        this._totalPrice = product.price;
    }
};