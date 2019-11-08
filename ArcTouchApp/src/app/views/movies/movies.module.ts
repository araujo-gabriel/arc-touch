import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import {
  MatListModule,
  MatCardModule,
  MatButtonModule,
  MatIconModule,
  MatTooltipModule,
  MatChipsModule,
  MatInputModule
} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';

import { MoviesComponent } from './movies.component';
import { MoviesRoutes } from './movies.routing';
import { MoviesService } from './movies.service';
import { MovieCardComponent } from './movie-card/movie-card.component';
import { SharedComponentsModule } from 'app/shared/components/shared-components.module';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from 'app/shared/pipes/filter.pipe';

@NgModule({
  imports: [
    CommonModule,
    MatListModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatChipsModule,
    FlexLayoutModule,
    PerfectScrollbarModule,
    SharedComponentsModule,
    MatInputModule,
    FormsModule,
    RouterModule.forChild(MoviesRoutes)
  ],
  declarations: [MoviesComponent, MovieCardComponent, FilterPipe],
  providers: [MoviesService]
})
export class MoviesModule { }
