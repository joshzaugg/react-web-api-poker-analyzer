import React, { Component } from 'react';
import Card from 'react-free-playing-cards';


export class PokerHand extends Component {
    static displayName = PokerHand.name;

    constructor(props) {
        super(props);
        this.state = {
            gameId: props.gameId,
            playerName: props.playerName,
            cards: props.cards,
            handSummary: props.handSummary
        };
    }

    componentDidMount() { }

    render() {
        let trClass = this.state.handSummary != null && this.state.handSummary.playerName == this.state.playerName
            ? "winning-player"
            : "";

        return (
            <tr className={trClass}>
                <td>{this.state.playerName}</td>
                <td>
                    {this.state.cards.map(card =>
                        <Card card={this.formatAbbreviationForCard(card.abbreviation)} key={card.abbreviation} className="card" deckType="basic" height="90px" />
                        )}
                </td>
            </tr>
        );
    }

    determineWinner(gameId) {
        fetch('api/v1.0/poker/DetermineWinner/' + gameId)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                this.setState({ handSummary: data });
            })
            .catch(e => console.error(e));
    }

    formatAbbreviationForCard(abbrev) {
        // format for Poker Card Library requirements
        abbrev = abbrev.replace("10", "T");
        abbrev = abbrev.charAt(0).toUpperCase() + abbrev.charAt(1).toLowerCase();
        return abbrev;
    }
}