import { Routes } from '@angular/router';

export const rootRouterConfig: Routes = [
  {
    path: '',
    redirectTo: 'movies',
    pathMatch: 'full'
  },
  {
    path: 'movies',
    loadChildren: () => import('./views/movies/movies.module').then(m => m.MoviesModule),
    data: { title: 'Movies' }
  },
  {
    path: '**',
    redirectTo: 'sessions/404'
  }
];

