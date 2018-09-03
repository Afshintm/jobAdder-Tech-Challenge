import { Component, OnInit } from '@angular/core';
import { Job } from '../job';
import { JobService } from '../job.service';
import { Candidate } from '../Candidate';


@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.scss']
})
export class JobsComponent implements OnInit {

jobs: Job[];
  title: string;

  constructor(private jobService: JobService) { }

  ngOnInit() {
    this.getJobs();
    this.title = 'Job List';
  }

  getJobs(): void {
    this.jobService.getJobs().subscribe(jobs => this.jobs = jobs) ;
  }

  getCandidateInfo(input:Candidate): string{

    return `${input.name} Score: ${input.score} Skill Tags:${input.skillTags}`; 
  }

  getJobInfo(input:Job): string{
    return `${input.name} in ${input.company}`;
  }

  getJobBestCandidate(input:Job): string{
    return `${input.goodCandidates[0].name}` ;
  }
}
