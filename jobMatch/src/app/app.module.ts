/*
import { BrowserModule } from '@angular/platform-browser';

import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JobsComponent } from './jobs/jobs.component';
import { CandidatesComponent } from './candidates/candidates.component';

@NgModule({
  declarations: [
    AppComponent,
    JobsComponent,
    CandidatesComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
*/

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule }    from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatMenuModule, MatInputModule, MatButtonModule, MatSelectModule, MatIconModule ,MatCardModule, MatCheckboxModule, MatExpansionModule } from '@angular/material';
import {MatListModule} from '@angular/material/list';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CandidatesComponent } from './candidates/candidates.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { JobsComponent } from './jobs/jobs.component';


@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatListModule,
    MatMenuModule,
    MatInputModule, 
    MatButtonModule, 
    MatSelectModule, 
    MatIconModule ,
    MatCardModule, 
    MatCheckboxModule,MatExpansionModule
  ],
  providers: [],
  declarations: [
    AppComponent,
    CandidatesComponent,
    DashboardComponent,
    JobsComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
