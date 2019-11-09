export class Paginator<T> {

    public page: number;
    public totalPages: number;
    public data: Array<T>;

    constructor(values?: any) {
        Object.assign(this, values);
    }
}