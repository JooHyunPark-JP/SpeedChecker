using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.Contracts
{
	public interface IBoardGameService<TFigure>
	{
		BoardGameMoveResult<TFigure> MakeMove(Guid gameId, Guid playerId, string from, string to);
		Guid? QueueForGame(Guid playerId);
	}
}