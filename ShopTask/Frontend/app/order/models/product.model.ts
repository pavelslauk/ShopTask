export class Product {

    private _title: string;
    private _price: number;
    private _description: string;
    private _category: string;

    public get title(): string {
        return this._title;
    }

    public get price(): number {
        return this._price;
    }

    public get description(): string {
        return this._description;
    }

    public get category(): string {
        return this._category;
    }

    constructor(data: any){
        this._title = data.Title || data.title;
        this._price = data.Price || data.price;
        this._description = data.Description || data.description;
        this._category = data.Category || data.category;
    }
};