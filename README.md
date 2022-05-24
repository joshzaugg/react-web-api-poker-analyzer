# react-web-api-poker-analyzer
This is a basic demo application demonstrating using a ReactJS front-end with a .NET Core RESTFul Web API back-end to analyze poker hands.  Although a goal was to use RESTFul API endpoints to do the work of analyzing poker hands, the setup operations of shuffling, dealing, and persisting game data are also offloaded to the back-end to separate our concerns.  I used bitshift operations to efficiently rank and compare the poker hands.

<!-- ABOUT THE PROJECT -->
## About The Project
I used the SPA Web Application templates provided by Visual Studio 2019. These use create-react-app under the hood, and also add some nice SPA boilerplate to a React front-end...the boilerplate includes bootstrapping several front-end libraries and also easy configuration to this or another back-end.  Additionally, the template also creates a .NET Core Web API project that can be easily configured to eliminate a lot of busy work.  The goal was to quickly spin up a UI that could talk to a RESTFul Web API backend, but not be completely coupled with the other implemenation.

Swashbuckle was implemented early into this project...it servers a dual purpose of testing Web API endpoints, while also helping to visualize the model in the process.  There are still some issues with the schema visualization and examples, but just with the enums.  I didn't want to focus my time resolving their dynamic examples and schema visualization issues just yet, but they don't accurately describe our Enums at this point.  The enums are currently serialized into values, instead of the intended string or Attribute values intended by the custom converters.

### Built With
* [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet)
* [React.js](https://reactjs.org/)
* [Bootstrap](https://getbootstrap.com)
* [Swashbuckle ASP.NET Core](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger/)
* [NewtonSoft JSON.Net](https://www.newtonsoft.com/json)
* [react-free-playing-cards](https://www.npmjs.com/package/react-free-playing-cards)

<!-- GETTING STARTED -->
## Getting Started

### Prerequisites
* .NET Core 3.1 (LTS)'
* Node.js
* NPM
* Visual Studio

### Installation
1.  Clone the repo
   ```sh
   git clone https://github.com/joshzaugg/react-web-api-poker-analyzer.git
   ```
2. Install NPM packages
   ```sh
   npm install
   ```
3. Restore Nuget packages (https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore)
4. Open in Visual Studio (if you choose to use VS to manage your Nuget packages, this will already be open)
5. In Visual Studio, set the startup project to React.Poker.API in the solution.
6. Build and Run

<!-- USAGE EXAMPLES -->
## Usage
The Home Page of the UI gives some basic instructions on use of the Poker Analyzer.  You can use the top NavBar menu to navigate to the Poker Room, as well as the Swashbuckle Swagger UI implementation to use the Web API directly. 

The Poker Room will allow you to deal and analyze Poker Hands for 2-6 players per game.  You can use the dropdown list to select how many players you would like to use for the next game before you select the 'New Game' Button.  Once the hands have been dealt, you can then select the 'Analyze Hands' Button to determine which user has the best hand and won the game in question.  Outside of the visual indicator on the winning player, you can see the hand summary for the winner at the bottom of the player table.  You can start a new hand/game at any time by pressing that button. Of note is that the dropdown selecting number of players won't have an effect until the dealing of the next hand.

## Current API 
The current API is versioned at 1.0 via URL segments.  This is important to remember for any requests, including Swashbuckle inputs.
#### API Endpoints
* DealNewGame (POST): /api/v1.0/Poker/DealNewGame, accepts array of player names
* DetermineWinner (POST): /api/v1.0/Poker/DetermineWinner, accepts JSON array of PokerHands for analysis
* DetermineWinner (GET): /api/v1.0/Poker/DetermineWinner/{id}, determines winner of previously dealt game
* GetExistingGame (GET): /api/v1.0/Poker/GetExistingGame/{id}, retrieves previously dealt game
* Delete (DELETE): /api/v1.0/Poker/{id}, deletes a previously dealt game

## TODO
* Unit Tests - these are a must, but time constraints eliminated them from happening immediately.  React.Poker.ApplicationCore and the front-end are the primary nees here.
* Swashbuckle Schema issues with custom Enum needs. Some more time needs to be spent on the JSON and Type Converters that aren't working as planned yet.

## License
[MIT](https://choosealicense.com/licenses/mit/)
