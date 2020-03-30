using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.Contracts
{
	using System;

	namespace SpeedCheckers.BLL
	{
		public interface IBoardGameRules<TFigure>
		{
			TFigure[,] GetInitialBoard();
			Guid? GetWinner(BoardGameState<TFigure> state, Guid white, Guid black);
			BoardGameMoveResult<TFigure> MakeMove(BoardGameState<TFigure> state, string from, string to);
		}
	}
}