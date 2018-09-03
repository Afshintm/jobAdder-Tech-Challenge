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
