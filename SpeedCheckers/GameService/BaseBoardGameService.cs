using SpeedCheckers.Contracts;
using SpeedCheckers.Contracts.SpeedCheckers.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.GameService
{
	public class BaseBOardGameService<TFigure, TRules> : IBoardGameService<TFigure>
		where TRules : IBoardGameRules<TFigure>, new()
	{
		public BaseBOardGameService(IGameBLL gameBLL, IGameMoveBLL gameMoveBLL)
		{
			Queue = new GameQueue();
			ActiveGames = new Dictionary<Guid, BoardGameState<TFigure>>();
			GameBLL = gameBLL;
			GameMoveBLL = gameMoveBLL;
		}
		GameQueue Queue { get; set; }
		Dictionary<Guid, BoardGameState<TFigure>> ActiveGames { get; set; }

		IGameBLL GameBLL { get; set; }
		IGameMoveBLL GameMoveBLL { get; set; }

		public Guid? QueueForGame(Guid playerId)
		{
			var gameId = Queue.QueueForGame(playerId);
			// if game is found but is not started yet, start a game
			if (gameId != null)
			{
				if (!ActiveGames.ContainsKey(gameId.Value))
				{
					var players = Queue.GetPlayerForGame(gameId.Value);
					GameBLL.StartGame(gameId.Value, players[0], players[1]);
					ActiveGames[gameId.Value] = new BoardGameState<TFigure>(gameId.Value, players[0], new TRules()); 
				}
			}
			return gameId;
		}

		public BoardGameMoveResult<TFigure> MakeMove(Guid gameId, Guid playerId, string from, string to)
		{
			var game = ActiveGames[gameId];
			var rules = new TRules();
			var moveResult = rules.MakeMove(game, from, to);
            var players = Queue.GetPlayerForGame(gameId);
            var otherPlayer = players.Where(x => x != playerId).First();

            if (moveResult.Ok)
			{
				moveResult.State.CurrentPlayer = otherPlayer;
				GameMoveBLL.AddMove(gameId, playerId, from, to);
				moveResult.State.Winner = rules.GetWinner(moveResult.State, players[0], players[1]);
				if (moveResult.State.Winner != null)
				{
					GameBLL.EndGame(gameId, moveResult.State.Winner.Value);
					Queue.DequeuePlayers(players[0], players[1]);
				}
			}
			return moveResult;
		}

		public BoardGameState<TFigure> GetState(Guid game_id)
		{
			return ActiveGames[game_id];
		}

	}
}