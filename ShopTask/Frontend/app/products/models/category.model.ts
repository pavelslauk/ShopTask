export class Category {
    private _id: number;
    private _name: string = '';

    public get id(): number {
        return this._id;
    }

    public get name(): string {
        return this._name;
    }

    constructor(data: any){
        this._id = data.Id;
        this._name = data.Name;
    }
};