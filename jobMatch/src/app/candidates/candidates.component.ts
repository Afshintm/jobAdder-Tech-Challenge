import { Component, OnInit } from '@angular/core';
import { Candidate } from '../Candidate';
import { CandidatesService } from '../candidates.service';


@Component({
  selector: 'app-candidates',
  templateUrl: './candidates.component.html',
  styleUrls: ['./candidates.component.scss']
})
export class CandidatesComponent implements OnInit {
	candidates: Candidate[];


  constructor(private candidatesService:CandidatesService ) { }

  ngOnInit() {
  	this.getCandidates();
  }

  getCandidates(): void {
  	this.candidatesService.getCandidates().subscribe(candidates => this.candidates = candidates) ;
  }
}
