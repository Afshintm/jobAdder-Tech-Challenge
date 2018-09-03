import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Job } from './job'

@Injectable({
  providedIn: 'root'
})
export class JobService {
	private jobsUrl = environment.apiBaseUrl+'api/jobs'; 

  constructor(
  	private http: HttpClient
  	) { }

  getJobs () : Observable<Job[]> {
  	return this.http.get<Job[]>(this.jobsUrl);
  }


}
