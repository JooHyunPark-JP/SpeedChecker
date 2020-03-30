using SpeedCheckers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.BLL
{// in charge of moving the pieces
	public class GameMoveBLL : IGameMoveBLL
	{
        // the function accepts the unique game id and the unique player id and the moveement details and saves it to the db moves history
		public Models.GameMove AddMove(Guid gameId, Guid playerId, string from, string to) {
			var id = Guid.NewGuid();
			using (var db = new SpeedCheckers.DAL.SpeedCheckersContext())
			{
				var lastMove = db.Moves.OrderByDescending(x => x.Number).FirstOrDefault();
				var number = lastMove?.Number + 1 ?? 1;

                var move = new Models.GameMove {
                    ID = id,
                    GameId = gameId,
                    MoveFrom = from,
                    MoveTo = to,
                    Number = number,
                    PlayerId = playerId,
				};
				db.Moves.Add(move);
				db.SaveChanges();
				return move;
			}
		}
	}
}