using System;
using SpeedCheckers.Models;

namespace SpeedCheckers.Contracts
{
	public interface IGameMoveBLL
	{
		GameMove AddMove(Guid gameId, Guid playerId, string from, string to);
	}
}