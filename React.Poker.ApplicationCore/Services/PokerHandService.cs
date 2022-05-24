using React.Poker.ApplicationCore.Extensions;
using React.Poker.DataModel.Interfaces;
using React.Poker.DataModel.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace React.Poker.ApplicationCore.Services
{
    public class PokerHandService : IPokerService
    {
        private int _nextCounter = 1;
        private static object syncRoot = new object();

        /// <summary>
        /// Cache of active games we know about
        /// </summary>
        private ConcurrentDictionary<int, IPokerGame> _activeGames = 
            new ConcurrentDictionary<int, IPokerGame>();

        /// <summary>
        /// Deal a new game from player names collection provided
        /// </summary>
        /// <param name="playerNames"></param>
        /// <returns></returns>
        public IPokerGame DealNewGame(IList<string> playerNames)
        {
            IPokerGame newGame;
            lock (syncRoot)
            {
                newGame = new PokerGame(_nextCounter, playerNames);
                _activeGames.TryAdd(_nextCounter, newGame);
                _nextCounter++;
            }
            return newGame;
        }

        /// <summary>
        /// Delete a saved active game
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteGame(int id)
        {
            return _activeGames.TryRemove(id, out IPokerGame _);
        }

        /// <summary>
        /// Retrieve a saved active game.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public IPokerGame GetExistingPokerGame(int gameId)
        {
            if (_activeGames.TryGetValue(gameId, out IPokerGame pokerGame))
            {
                return pokerGame;
            }
            return null;
        }

        /// <summary>
        /// Determine a winner from provided hands
        /// </summary>
        /// <param name="hands"></param>
        /// <returns></returns>
        public IPokerHandSummary DetermineWinner(IList<PokerHand> hands, int gameId = 0)
        {
            return hands.Select(h => h.GetPokerHandSummary(gameId))
                .OrderByDescending(h => h.CalculatedValue)
                .First();
        }

        /// <summary>
        /// Determine a winner from a given Poker Game
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public IPokerHandSummary DetermineWinner(IPokerGame game)
        {
            if (game.CardsDealt)
            {
                return DetermineWinner(game.Hands, game.GameId);
            }
            return null;
        }

        /// <summary>
        /// Determine a winner from a given game ID
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public IPokerHandSummary DetermineWinner(int gameId)
        {
            if (_activeGames.TryGetValue(gameId, out IPokerGame game))
            {
                return DetermineWinner(game);
            }
            return null;
        }
    }
}
