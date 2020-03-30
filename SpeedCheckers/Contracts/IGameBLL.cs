using SpeedCheckers.Models;
using System;
using System.Collections.Generic;

namespace SpeedCheckers.Contracts
{
	public interface IGameBLL
	{
		Guid EndGame(Guid gameId, Guid winnerId);
		Guid StartGame(Guid id, Guid playerWhite, Guid playerBlack);
		GameResult GetGameResults(Guid gameId);
	}
}