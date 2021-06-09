import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterStateSnapshot } from '@angular/router';
import { EventEmitter } from 'events';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  username:string=null;
  isLoggedIn:boolean;
  
  constructor(private accountService:AccountService,
    private router:Router,
    private route:ActivatedRoute
    ){
    
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  ngOnInit(){
    this.updateLoginInfo();
    this.accountService.loginEvent.subscribe(
      res=>{
        if(res){
          this.updateLoginInfo();
        }
        else{
          this.isLoggedIn = false;
          this.username ="";
        }
      }
    );
  }
  logout(){
    
    this.accountService.logout();
    this.router.navigate(['/']);
  }

  updateLoginInfo(){
    if(this.accountService.getUserName()){
      this.isLoggedIn = true;
      this.username = "Hello "+ this.accountService.currentUser.username;
    }
  }
}
