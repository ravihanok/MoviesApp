import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt'
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Iuser } from './iuser';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

      private apiUrl = "https://localhost:44383/api";
     // private apiUrl ="https://brhwebapp.azurewebsites.net/api";
      helper = new JwtHelperService();
      currentUser:Iuser = {
        username:null,
        email:null,
        role:null,
        jobTitle:null

      };
  constructor(private http:HttpClient) { }

  login(model:any):Observable<Iuser>{
       return this.http.post(this.apiUrl+"/account/login",model).pipe(
          map((res:any)=>{
            let decodedToken = this.helper.decodeToken(res.token);
            this.currentUser.email = decodedToken.email;
            this.currentUser.username = decodedToken.username;
            localStorage.setItem('token',res.token);
            return this.currentUser;
          }),catchError(error=> {
            console.log("Caught in catchError");
            return throwError(error);
          })
        );
  }

  errorHandler(error){
    let errorMessage = "";
    return throwError(error.errorMessage);
    
  }
}
