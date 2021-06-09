import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
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
     logoutEvent = new EventEmitter<boolean>();
     loginEvent = new EventEmitter<boolean>();
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
            debugger;
            let decodedToken = this.helper.decodeToken(res.token);
            this.currentUser.email = decodedToken.email;
            this.currentUser.username = decodedToken.username;
            localStorage.setItem('token',res.token);
            this.loginEvent.emit(true);
            return this.currentUser;
          }),catchError(error=> {
            console.log("Caught in catchError");
            return throwError(error);
          })
        );
  }

  register(model:any){
    return this.http.post(this.apiUrl+"/account/register",model).pipe(
      catchError(this.errorHandler)
    );
  }
  getUserName(){
    let token =  localStorage.getItem('token');
    if(!this.helper.isTokenExpired(token)){
      let decodedToken = this.helper.decodeToken(token);
      this.currentUser.email = decodedToken.email;
      this.currentUser.username = decodedToken.given_name;
      return this.currentUser.username;
    }
   else{
     return null;
   }
  }
  errorHandler(error){
    let errorMessage = "";
    console.log(error);
    return throwError(error);
    
  }
  logout() {
    localStorage.removeItem('token');
    this.loginEvent.emit(false);
    this.currentUser={
      username:null,
        email:null,
        role:null,
        jobTitle:null
    };
  }
}
