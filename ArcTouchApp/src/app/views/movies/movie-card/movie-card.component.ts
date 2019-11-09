import { Component, OnInit, Input } from '@angular/core';
import { Movie } from '../movie.model';
import { MoviesService } from '../movies.service';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {

  @Input() movie: Movie = new Movie();
  isLoading = false;
  hasDetails = false;
  constructor(private moviesService: MoviesService) { }

  ngOnInit() {
  }

  getDetails() {
    if (this.movie) {
      this.isLoading = true;

      this.moviesService.getDetails(this.movie.id)
        .subscribe(response => {
          if (response.data) {

            this.movie = response.data;
            this.hasDetails = true;
          }

          this.isLoading = false;
        },
          error => {
            this.isLoading = false;
            this.moviesService.errorResponseHandler(error);
          });
    }
  }

  hide() {
    this.hasDetails = false;
  }

  getImage() {
    if (!this.movie || !this.movie.backdrop) {
      return "assets/images/image-not-found.png";
    }

    return this.movie.backdrop;
  }
}
