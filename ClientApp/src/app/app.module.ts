import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AgGridModule } from 'ag-grid-angular';
import { DetailsComponent } from './Details/details/details.component';
import { ButtonRendererComponent } from './Renderer/button-renderer/button-renderer.component';
import { LoginComponent } from './account/login/login/login.component';
import { RegisterComponent } from './account/register/register/register.component';
import { BookingComponent } from './booking/booking/booking.component';
import { AuthGaurdService } from './auth-gaurd.service';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { JWT_Module_Options } from './account/JWT_Module_Options';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    DetailsComponent,
    ButtonRendererComponent,
    LoginComponent,
    RegisterComponent,
    BookingComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AgGridModule.withComponents([ButtonRendererComponent]),
    JwtModule.forRoot(JWT_Module_Options),
    RouterModule.forRoot([
      {path:'',redirectTo:'movies',pathMatch:'full'},
      {path:'login',component:LoginComponent},
      {path:'register',component:RegisterComponent},
      {path:'book',component:BookingComponent,canActivate:[AuthGaurdService]},
      { path: 'movies', component: HomeComponent, },
      {path:'movies/:id/details',component:DetailsComponent}
     // {path:'**',redirectTo:'movies',pathMatch:'full'}
    ])
  ],
  providers: [JwtHelperService],
  bootstrap: [AppComponent]
})
export class AppModule { }
