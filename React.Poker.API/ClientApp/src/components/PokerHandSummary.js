import React, { Component } from 'react';

export class PokerHandSummary extends Component {
    static displayName = PokerHandSummary.name;

    constructor(props) {
        super(props);
        this.state = {
            playerName: props.playerName,
            gameId: props.gameId,
            normalizedHand: props.normalizedHand,
            handRank: props.handRank,
            calculatedValue: props.calculatedValue,
            summary: props.getSummaryText
        };
    }

    componentDidMount() { }

    render() {
        return (
            <h6>{this.state.summary}</h6>
        );
    }
}