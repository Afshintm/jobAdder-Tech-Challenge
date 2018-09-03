export class Candidate{
	candidateId: number;
	name: string;
	skillTags: string;
	score: number;
	info(){
		return `${this.candidateId} ${this.score}`;
	}
  }
