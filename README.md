# README #

### What is this repository for? ###

This is a simple .NET Core 2 web api and an angularJs-6 front-end applications for the technical challenge I have been asked to do.

### Get up and running with Web Api app ###

To build web api just clone it and open jobMatch.Api.sln using visual studio 2017 then Build and run it.

Webapi has got 4 projects.

1- Model Layer, 
2- Business Services Layer
3- Web APi layer
4- Unit testing -  for Business Services unit testing


### Api Endpoints: ###

Get request to get list of candidates: `http://localhost:50532/api/candidates`
Get Job with candidate score: `http://localhost:50532/api/jobs`
Get specific Job Id candidate list `http://localhost:50532/api/jobs/1`

### The Assumption in Matching candidates ###

if a job or candidate have more than one skill in their skill tags, this means the importance of that skill is highlighted. 
So, the multiplier of that skill will be the sum of position of all occurrences. 

### Match Candidate Algorithm ###


The position of the skill will be the multiplier for scoring algorithm.

Algorithm initially weighs each candidate skill based on their skillTags.

There are two strategies for weighing skills. 

Adding repeated skill's multiplier  OR  Ignoring repeated skills.
These strategies can be passed to business service class 
 `CandidateJobScoreCalculatorServices<TCandidateSkillStrategy,TJobSkillStrategy>`  

The system is currently using first algorithm.

For instance, with first algorithm   candidate with skillTags will have a following Skill Weigh.
 `SkillTags:Java, x-code, illustrator, IOS, fast-typing, x-code, c-Sharp`
Java: 7
x-code:8  , because x-code appeared twice in the list.
illustrator: 5,   
IOS:4,   
Fast-typing: 3,     
c-sharp: 1
The same Scoring will be done for the job skills.
Then for each job, we score the candidates based on 
`Job.Skill.Weight * Candidate.Skill.Weight` 

This way, candidates with most relevant skills will get higher marks.



### Get up and running with front-end App ###

A simple angularJs 6 project to show the result of the Matching 5 top candidates for the jobs

### Important ###

WebApi should be running for the following website to work.
Please run under node version 9.4.0
This app uses ng-cli 6
Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.



