import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm:FormGroup;
  statusMessage:string;
  constructor(private formBuilder:FormBuilder,
    private accountService:AccountService,
    private router:Router) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      username:['',Validators.required],
      password:['',Validators.required],
      email:['',Validators.required]
    });
  }

  register(){
    this.accountService.register(this.registerForm.value).subscribe((res:any)=>
      {
        
        if(res.result.succeeded){
          this.statusMessage="Registration successful";
        }
      },
      (error)=>{
        console.log(error)
        if(error instanceof HttpErrorResponse){
          var resError = <HttpErrorResponse>error; 
          this.statusMessage =error.error.detail; 
        }
        else{
          this.statusMessage = error.message;
        }
      });
  }

}
