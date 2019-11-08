export class Movie {

    public id: number;
    public name: string;
    public backdrop: string;
    public releaseDate: Date;
    public details: MovieDetails;

    constructor(values?: any) {
        Object.assign(this, values);
    }
}

export class MovieDetails {

    public overview: string;
    public genres: Array<MovieGenre>;

    constructor(values?: any) {
        Object.assign(this, values);
    }
}

export class MovieGenre {

    public name: string;

    constructor(values?: any) {
        Object.assign(this, values);
    }
}