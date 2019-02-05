export class Product {
    private _id: number;
    private _title: string;
    private _price: number;
    private _description: string;
    private _category: any;

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

    public get category(): any {
        return this._category;
    }

    public set category(value : any) {
        this._category = value;
    }

    constructor(data: any) {
        if(data) {
            this._id = data.Id;
            this._title = data.Title;
            this._price = data.Price;
            this._description = data.Description;
            this._category = data.Category;
        }
     }

    public SetData(data: any){
        this._title = data.title;
        this._price = data.price;
        this._description = data.description;
        this._category = data.category;
    }
};