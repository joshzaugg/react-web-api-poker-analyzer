import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { PokerHand } from './PokerHand';
import { PokerHandSummary } from './PokerHandSummary';

export class PokerLobby extends Component {
    static displayName = PokerLobby.name;

    static playerNameOptions = ['Bill', 'Mike', 'Mary', 'Alex', 'Russell', 'Terrence', 'Bob', 'Jose', 'Angela', 'Henry', 'Freddy'];

    constructor(props) {
        super(props);
        this.state = {
            currentGame: {},
            loading: true,
            handSummary: null,
            numberOfPlayers: 2,
            numberOfPlayersOptions: [2,3,4,5,6]
        };

        this.handlePlayerCountChange = this.handlePlayerCountChange.bind(this);
    }

    componentDidMount() {
        this.createGame();
        
    }

    static renderGame(game, handSummary) {
        let summaryContent = handSummary == null
            ? 'N/A'
            : <PokerHandSummary {...handSummary} />;
        return (
            <Container>               
                <table className="table table-striped" aria-labelledby="tabelLabel" >
                    <thead>
                        <tr>
                            <th>Player</th>
                            <th>Hand</th>
                        </tr>
                    </thead>
                    <tbody>
                        { game.hands.map(hand => <PokerHand key={hand.playerName} gameId={game.gameId} playerName={hand.playerName} cards={hand.cards} handSummary={handSummary} /> )}
                    </tbody>
                </table>
                <div className="game-summary">
                    <h4>Game Summary</h4>
                    <p>{summaryContent}</p>
                </div>
            </Container>            
        );
    }

    render() {
        let content = this.state.loading
            ? <p><em>Loading...</em></p>
            : PokerLobby.renderGame(this.state.currentGame, this.state.handSummary);

        return (
            <div>
                <h1 id="tabelLabel">Poker Room</h1>
                <div className="flex-around">
                    <button className="btn btn-primary" onClick={() => this.createGame()}>New Game</button>
                    <button className="btn btn-primary" onClick={() => this.determineWinner(this.state.currentGame.gameId)}>Analyze Hands</button>
                    <label>
                        Number of Players:
                        <select className="select-margin" value={this.state.numberOfPlayers} onChange={this.handlePlayerCountChange}>
                            {this.state.numberOfPlayersOptions.map((option) => (
                                <option value={option}>{option}</option>
                            ))}
                        </select>
                    </label>
                </div>
                {content}
            </div>
        );
    }

    createGame() {
        this.setState({ loading: true });

        // Shuffle array
        const shuffled = PokerLobby.playerNameOptions.sort(() => 0.5 - Math.random());
        const selected = shuffled.slice(0, this.state.numberOfPlayers);

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(selected)
        };
        fetch('api/v1.0/poker/DealNewGame', requestOptions)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                this.setState({ currentGame: data, handSummary: null, loading: false });
            })
            .catch(e => console.error(e));
    }

    determineWinner(gameId) {
        this.setState({ loading: true });

        fetch('api/v1.0/poker/DetermineWinner/' + gameId)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                this.setState({ handSummary: data, loading: false });
            })
            .catch(e => console.error(e));
    }

    handlePlayerCountChange(event) {
        this.setState({ numberOfPlayers: event.target.value });
    }
}