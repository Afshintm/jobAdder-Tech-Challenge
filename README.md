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

Get request to get list of candidates: http://localhost:50532/api/candidates




### The Assumption in Matching candidates ###
if a job or candidate have more then one skill in therir skill tags, this means the importance of that skill is highlighted. 
So the multiplier of that skill will be the sum of position of all occurances. 


### Get up and running with front-end App ###

A simple angularJs 1.6 project to show the Races detail report

### Important ###
WebApi should be running for the following website to work.

Please run under node version 9.4.0

install npm packages and bower dependencies using following commands 



