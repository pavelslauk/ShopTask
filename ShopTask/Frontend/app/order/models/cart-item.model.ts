export class CartItem {

    private _title: string;
    private _productPrice: number;
    private _description: string;
    private _category: string;
    private _productsCount: number;
    private _totalPrice: number;

    constructor(data: any){
        this._title = data.Title;
        this._productPrice = data.Price;
        this._description = data.Description;
        this._category = data.Category;
        this._productsCount = 1;
        this._totalPrice = data.Price;
    }

    public get productsCount(): number {
        return this._productsCount;
    }
 
    public set productsCount(c: number) {
        this._productsCount = c;
        this._totalPrice = Number((this._productPrice * this._productsCount).toFixed(2));
    }

    public get totalPrice(): number {
        return this._totalPrice;
    }
};