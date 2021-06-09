import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../account.service';
import { Login } from '../../login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginModel:Login= {
    username:null,
    password:null
  }
  statusMessage:string;
  loginForm:FormGroup;
  constructor(private accountService:AccountService,
    private route:ActivatedRoute,
    private router:Router,
    private formBuilder:FormBuilder) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username:['',Validators.required],
      password:['',Validators.required]
    });
  }

  login(){
    this.accountService.login(this.loginForm.value)
    .subscribe(res=>{
      if(this.accountService.currentUser.username!=null)
      this.router.navigate(['/movies']);
    },
    (error)=>{
      console.log(error)
        if(error instanceof HttpErrorResponse){
          var resError = <HttpErrorResponse>error; 
          this.statusMessage =resError.error.detail; 
        }
        else{
          this.statusMessage = error.message;
        }
    });
  }
}
