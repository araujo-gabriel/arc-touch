import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { AppLoaderService } from '../../shared/services/app-loader/app-loader.service';
import { MoviesService } from './movies.service';
import { Paginator } from 'app/shared/models/paginator.model';
import { Movie } from './movie.model';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit, AfterViewInit, OnDestroy {
  paginator: Paginator<Movie> = new Paginator<Movie>();
  searchText: string;
  constructor(
    private loader: AppLoaderService,
    private moviesService: MoviesService
  ) { }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.loader.open();
    this.moviesService.get(1)
      .subscribe(response => {
        this.paginator = response.data;
        this.loader.close();
      },
        error => {
          this.loader.close();
          this.moviesService.errorResponseHandler(error);
        });
  }

  ngOnDestroy() {
    this.loader.close();
  }

  goBack() {
    this.searchText = '';
    this.getMovies(this.paginator.page - 1);
  }

  goFoward() {
    this.searchText = '';
    this.getMovies(this.paginator.page + 1);
  }


  getMovies(page = 1) {
    this.loader.open();
    this.moviesService.get(page)
      .subscribe(response => {
        this.paginator = response.data;
        this.loader.close();
      },
        error => {
          this.loader.close();
          this.moviesService.errorResponseHandler(error);
        });
  }

}
