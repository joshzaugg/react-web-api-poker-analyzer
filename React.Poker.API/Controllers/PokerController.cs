using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using React.Poker.ApplicationCore.Services;
using React.Poker.DataModel.Interfaces;
using React.Poker.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace React.Poker.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PokerController : ControllerBase
    {
        /// <summary>
        /// Singleton service to handle our poker needs
        /// </summary>
        private IPokerService _pokerService;

        /// <summary>
        /// Logger for this controller
        /// </summary>
        private readonly ILogger _logger;

        public PokerController(IPokerService pokerService, ILogger<PokerController> logger)
        {
            _pokerService = pokerService;
            _logger = logger;
        }

        // POST: api/<PokerController>/DealNewGame
        [HttpPost, Route("DealNewGame")]
        public ActionResult<IPokerGame> DealNewGame([FromBody] string[] players)
        {
            if (players == null || players.Length < 2)
            {
                return BadRequest("Not enough players to determine a winner");
            }
            try
            {
                var result = _pokerService.DealNewGame(players.ToList());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DealNewGame)}: {ex}");
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("DetermineWinner/")]
        public ActionResult<IPokerHandSummary> DetermineWinner([FromBody] IList<PokerHand> hands)
        {
            if (hands == null || hands.Count < 2)
            {
                return BadRequest("Not enough hands to determine a winner");
            }
            try
            {
                var result = _pokerService.DetermineWinner(hands, 0);
                if (result == null)
                {
                    _logger.LogWarning($"Could not Determine Winner from hands provided.");
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DetermineWinner)}: {ex}");
                return Problem(ex.Message);
            }
        }

        // GET api/<PokerController>/DetermineWinner/5
        [HttpGet, Route("DetermineWinner/{id}")]
        public ActionResult<IPokerHandSummary> DetermineWinner(int id)
        {
            var game = _pokerService.GetExistingPokerGame(id);
            if (game == null)
            {
                return NoContent();
            }
            try
            {
                var result = _pokerService.DetermineWinner(id);
                if (result == null)
                {
                    _logger.LogWarning($"Could not Determine Winner from id provided: {id}");
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DetermineWinner)}: {ex}");
                return Problem(ex.Message);
            }
        }

        // GET api/<PokerController>/GetExistingGame/5
        [HttpGet, Route("GetExistingGame/{id}")]
        public ActionResult<IPokerGame> GetExistingGame(int id)
        {
            var game = _pokerService.GetExistingPokerGame(id);
            if (game == null)
            {
                _logger.LogWarning($"Could not find existing game for id: {id}");
                return NoContent();
            }
            return Ok(game);
        }

        // DELETE api/<PokerController>/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            return Ok(_pokerService.DeleteGame(id));
        }
    }
}
