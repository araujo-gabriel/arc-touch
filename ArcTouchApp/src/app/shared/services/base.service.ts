
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';

export class BaseService {

    constructor(protected snack: MatSnackBar,
        protected http: HttpClient) { }




    errorResponseHandler(error) {
        if (error.status > 0 && error.status < 500) {
            if (error.status === 400) {
                this.snack.open(error.error.message, 'Fechar', { duration: 6000 });
            }
        } else {
            this.snack.open('Ocorreu um erro inesperado, tente novamente mais tarde.', 'Fechar', { duration: 6000 });
        }
    }
}