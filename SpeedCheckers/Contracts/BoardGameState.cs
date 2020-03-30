using SpeedCheckers.Contracts.SpeedCheckers.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.Contracts
{
	public class BoardGameState<TFigure>
	{
		public BoardGameState(Guid gameId, Guid player1, IBoardGameRules<TFigure> rules)
		{
			GameId = GameId;
			IsStarted = true;
			CurrentPlayer = player1;
			WhitePlayerId = player1;
			Winner = null;
			Board = rules.GetInitialBoard();
		}

		public Guid WhitePlayerId { get; set; }
		public Guid GameId { get; set; }
		public bool IsStarted { get; set; }
		public TFigure[,] Board { get; set; }
		public Guid CurrentPlayer { get; set; }
		public Guid? Winner { get; set; }
	}
}