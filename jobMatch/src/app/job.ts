import { Candidate } from './candidate';
export class Job{
  jobId: number;
  name: string;
  company: string; 
  goodCandidates: Candidate[];
  toString = function(){
  	return 'name '+name;
  }
}
