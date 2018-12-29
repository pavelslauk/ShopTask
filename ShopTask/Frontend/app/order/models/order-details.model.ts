export class OrdserDetails {

    private _name:string = '';
    private _surname:string = '';
    private _address:string = '';
    private _phone:string = '';
    private _comments:string = '';

    public get name(): string {
        return this._name;
    }

    public get surname(): string {
        return this._surname;
    }

    public get address(): string {
        return this._address;
    }

    public get phone(): string {
        return this._phone;
    }

    public get comments(): string {
        return this._comments;
    }

    constructor() { } 

    public SetData(data: any){
        this._name= data.name;
        this._surname = data.surname;
        this._address = data.address;
        this._phone = data.phone;
        this._comments = data.comments;
    }
};