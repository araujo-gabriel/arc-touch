import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';
import { BaseService } from 'app/shared/services/base.service';
import { environment } from 'environments/environment';
import { AppLoaderService } from 'app/shared/services/app-loader/app-loader.service';
import { Observable } from 'rxjs';
import { Paginator } from 'app/shared/models/paginator.model';
import { ResponseModel } from 'app/shared/models/response.model';
import { Movie } from './movie.model';


@Injectable()
export class MoviesService extends BaseService {

    private readonly url = environment.backendUrl + 'movie';

    constructor(http: HttpClient,
        snack: MatSnackBar, private loader: AppLoaderService, ) {
        super(snack, http);
    }

    get(page: number): Observable<ResponseModel<Paginator<Movie>>> {
        return this.http
            .get<ResponseModel<Paginator<Movie>>>(`${this.url}?page=${page}`);
    }

    getDetails(id: number): Observable<ResponseModel<Movie>> {
        return this.http
            .get<ResponseModel<Movie>>(`${this.url}/${id}/details`);
    }


}
