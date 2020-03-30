using SpeedCheckers.Contracts;
using SpeedCheckers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.BLL
{
	public class GameBLL : IGameBLL
	{
		public Guid StartGame(Guid id, Guid playerWhite, Guid playerBlack) {
			using (var db = new SpeedCheckers.DAL.SpeedCheckersContext())
			{
				db.Games.Add(new Models.Game
				{
					ID = id,
					StartTime = DateTime.Now,
					PlayerWhiteId = playerWhite,
					PlayerBlackId = playerBlack,
				});
				db.SaveChanges();
			}
			return id;
		}

		public Guid EndGame(Guid gameId, Guid winnerId)
		{
			var id = Guid.NewGuid();
			using (var db = new SpeedCheckers.DAL.SpeedCheckersContext())
			{
				var game = db.Games.First(x => x.ID == gameId);
				game.EndTime = DateTime.Now;
				game.WinnerId = winnerId;
				db.SaveChanges();
			}
			return id;
		}

		public GameResult GetGameResults(Guid gameId)
		{
			using (var db = new SpeedCheckers.DAL.SpeedCheckersContext())
				{
				var game = db.Games.First(x => x.ID == gameId);
				return new GameResult
				{
					Winner = game.Winner,
					Loser = game.WinnerId == game.PlayerWhiteId ? game.PlayerBlack : game.PlayerWhite
				};
			}
		}
        //loads games data into list and lists it on game page
		public List<FinishedGame> GetFinishedGames()
		{
			using (var db = new SpeedCheckers.DAL.SpeedCheckersContext())
			{
				var games = db.Games
					.Where(x=>x.EndTime!=null)
					.OrderBy(x=>x.StartTime)
					.Select(x => new FinishedGame
					{
						StartTime = x.StartTime,
						EndTime = x.EndTime.Value,
						WhitePlayerName = x.PlayerWhite.Name,
						BlackPlayerName = x.PlayerBlack.Name,
						WinnerName = x.Winner.Name
					}).ToList(); 
				return games;
			}
		}

		public List<User> GetPlayersForGame(Guid gameId) {
			using (var db = new SpeedCheckers.DAL.SpeedCheckersContext())
			{
				var game = db.Games.First(x => x.ID == gameId);
				return new List<User> {
					game.PlayerWhite,
					game.PlayerBlack
				};
			}
		}

	}
}