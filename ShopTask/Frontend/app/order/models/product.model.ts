export class Product {
    private _id: number;
    private _title: string;
    private _price: number;
    private _description: string;
    private _category: string;

    public get id(): number {
        return this._id;
    }

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
        this._id = data.Id;
        this._title = data.Title;
        this._price = data.Price;
        this._description = data.Description;
        this._category = data.Category;
    }
};