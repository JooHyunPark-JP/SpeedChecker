using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.GameService
{
	public class GameQueue
	{
		public GameQueue()
		{
			playerToGameMap = new Dictionary<Guid, Guid?>();
		}

		// dictionary that holds gameId for player. null if game wa not found yet
		Dictionary<Guid, Guid?> playerToGameMap { get; set; }

		// adds player to queue and returns id for game if 2 players are in queue
		public Guid? QueueForGame(Guid playerId)
		{
			Guid? gameId = null;
			// if player is already in queue, return value from map
			if (playerToGameMap.ContainsKey(playerId))
			{
				return playerToGameMap[playerId];
			}
			// otherwise add him to queue
			playerToGameMap[playerId] = null;
			// check if 2 players are in queue
			var playersInQueue = playerToGameMap.Where(x=>x.Value==null).Select(x=>x.Key).ToList();
			if (playersInQueue.Count == 2)
			{
				gameId = Guid.NewGuid();
				foreach (var player in playersInQueue)
				{
					playerToGameMap[player] = gameId;
				}
			}
			return gameId;
		}

		public void DequeuePlayers(Guid playerWhite, Guid playerBlack)
		{
			playerToGameMap.Remove(playerWhite);
			playerToGameMap.Remove(playerBlack);
		}

		public List<Guid> GetPlayerForGame(Guid gameId)
		{
			return playerToGameMap.Where(kv => kv.Value == gameId).Select(kv => kv.Key).ToList();
		}
	}
}