import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
            <h1>Poker API Demo</h1>
            <hr/>
            <h5>Welcome to the Poker Demo API!</h5>
            <p>
                This basic demo app shows the ability to create and use a.NET Core RESTFul Web API back-end, coupled with a ReactJS front-end.  
                You can use the top NavBar menu to navigate to the Poker Room, as well as the Swashbuckle Swagger UI implementation to use
                the Web API directly.  All shuffling, dealing, and hand analysis are done via Web API RESTFul calls.
            </p>
            <h5>Usage:</h5>
            <p>
                The Poker Room will allow you to deal and analyze Poker Hands for 2-6 players per game.  You can use the dropdown list
                to select how many players you would like to use for the next game before you select the 'New Game' Button.  Once the
                hands have been dealt, you can then select the 'Analyze Hands' Button to determine which user has the best hand and won
                the game in question.  Outside of the visual indicator on the winning player, you can see the hand summary for the winner
                at the bottom of the player table.  You can start a new hand/game at any time by pressing that button.
            </p>
            <p>
                This app uses the following tools, and more:
                <ul>
                    <li>ReactJS</li>
                    <li>.NET Core 3.1 MVC WebAPI with C#</li>
                    <li>Swagger/Swashbuckle</li>
                    <li>NPM 3rd party packages, such as create-react-app, react-free-playing-cards, and many more</li>
                </ul>
            </p>
    </div>
    );
  }
}
