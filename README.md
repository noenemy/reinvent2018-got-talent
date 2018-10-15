### This is a Builders Fair project for re:Invent 2018

## 1. Project Overview
A casual game using AWS Rekognition emotion API

## 2. Project Abstract
This demo is to play a simple game using facial expression to be recognized using AWS Rekognition emotion API in real time. The demo is utilizing AngularJs for dashboard, .Net core for API functions, Unity or HTML5 for game client and other AWS services. The demo is a game like famous TV show ‘America’s Got Talent’, it is require you to make several different emotion faces like happiness, angriness, sadness and so on. Based on your action, a score is provided and will be ranked with your facial expression on the leaderboard.

## 3. Development Environment Setup (macOS)

### 3.1 Prerequisites
1. Install .NET Core SDK 
   - www.microsoft.com/net/download/macos
2. Install Node and NPM
   - https://nodejs.org/en
   - Select the LTS build (recommneded for most users)
3. Install Visual Studio Code
   - https://code.visualstudio.com
4. Install Angular CLI
   - Npm install -g @angular/cli@6.0.8
5. Install Git
   - https://git-scm.com/download/mac
6. Install libgdiplus for using Graphics in API project
   - brew install mono-libgdiplus
7. Install Postman (optional)
   - http://www.getpostman.com
8. Recommended extensions for Visual Studio Code (optional)
   - C# by OmniSharp
   - C# Extensions
   - NuGet package manager
   - Angular v6 snippets
   - Angular files
   - Angular2-switcher
   - Angular Language service
   - Auto rename tag
   - Bracket pair colorizer
   - Debugger for Chrome
   - Material Icon Theme
   - Path Intellisense
   - Prettier
   - TSLint
  
### 3.2 How to clone the repository 

1. Open a terminal
2. Move to your project folder
3. Checkout the latest version of project files 
   - git clone https://github.com/noenemy/reinvent2018-got-talent.git

### 3.3 How to build

1. Open a terminal
2. Move to your project folder
   - cd reinvent2018-got-talent
3. For API project (.Net core)
   - cd GotTalent-API
   - dotnet run (or dotnet watch run)
   - Kestrel server will run on http://localhost:5000
4. For Web project (AngularJS)
   - cd GotTalent-Web
   - npm update (only requires the first time for downloading app-modules)
   - ng serve
   - The website will run on http://localhost:4200
   
## 4. Deployment 

### 4.1 Create required resources on AWS
  
### 4.2 IAM 
  
### 4.3 How to deploy Web site 

### 4.4 How to deploy API server

### 4.5 How to deploy Database





