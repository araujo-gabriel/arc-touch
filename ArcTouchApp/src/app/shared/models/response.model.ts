export class ResponseModel<T> {

    public message: string;
    public data: T;

    constructor(values?: any) {
        Object.assign(this, values);
    }
}