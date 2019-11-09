import { Pipe, PipeTransform } from '@angular/core';
import { Movie } from 'app/views/movies/movie.model';

@Pipe({
    name: 'filter'
})
export class FilterPipe implements PipeTransform {

    transform(data: Movie[], searchText: string): any {

        if (!searchText) {
            return data;
        } else {
            if (searchText) {
                data = data.filter(x => x.name.toLowerCase().indexOf(searchText.toLowerCase()) !== -1);
            }
        }
        return data;
    }

}  