import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../environments/environment';
import { Candidate } from './candidate'

@Injectable({
  providedIn: 'root'
})
export class CandidatesService {
	private candidatesUrl= environment.apiBaseUrl+'api/candidates';

  constructor(private http: HttpClient) { }

  getCandidates (): Observable<Candidate[]> {
  	return this.http.get<Candidate[]>(this.candidatesUrl);
  }
}

