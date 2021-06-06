import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http'
import {catchError,map} from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { MovieModal } from '../movie-modal';
@Injectable({
    providedIn:'root'
})
export class MovieService {
   
    private apiUrl = "https://localhost:44383/api";
    //private apiUrl ="https://brhwebapp.azurewebsites.net/api";
    constructor(private http:HttpClient){}
    getMoviesData(): Observable<MovieModal[]>{
       return this.http.get<MovieModal[]>(this.apiUrl+"/movies")
       .pipe(catchError(this.errorHandler))
       ;
    }
    getMovie(id: any):Observable<MovieModal> {
      return this.http.get<MovieModal>(this.apiUrl+"/movies/"+ id).pipe(catchError(this.errorHandler));
    }
    errorHandler(error){
      debugger;
       let errorMessage = "";
        if(error.error instanceof ErrorEvent){
            errorMessage = error.error.message;
        }
        else{
            errorMessage = "status code ${error.statusCode}\n message: ${error.message}";
        }
        return throwError(errorMessage);
    }
}