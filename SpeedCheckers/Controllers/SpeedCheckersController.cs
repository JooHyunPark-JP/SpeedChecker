using SpeedCheckers.BLL;
using SpeedCheckers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpeedCheckers.Controllers
{
	public class SpeedCheckersController : ApiController
	{
		public static Lazy<SpeedCheckersGameService> GameService = new Lazy<SpeedCheckersGameService>(() => new SpeedCheckersGameService());

		public class QueueRequest
		{
			public string black_player_name { get; set; }
			public string white_player_name { get; set; }

		}

		public class QueueResponse
		{
			public Guid? gameId { get; set; }
		}

		[Route("api/speedcheckers/queue")]
		[HttpPost]
		public QueueResponse Queue(QueueRequest request) {
			var whitePlayer = new UserBLL().CreateUser(request.white_player_name);
			var blackPlayer = new UserBLL().CreateUser(request.black_player_name);
			
			GameService.Value.QueueForGame(whitePlayer);
			var game_id = GameService.Value.QueueForGame(blackPlayer);

			return new QueueResponse {
				gameId = game_id,
			};
		}

		public class StateRequest {
			public Guid game_id { get; set; }
		}

		[Route("api/speedcheckers/state")]
		[HttpPost]
		public BoardGameState<BLL.SpeedCheckersRules.Checkers> GetState(StateRequest request)
		{
			return GameService.Value.GetState(request.game_id);
		}

		public class MoveRequest {
			public Guid game_id { get; set; }
			public Guid user_id { get; set; }
			public string from { get; set; }
			public string to{ get; set; }
		}

		[Route("api/speedcheckers/move")]
		[HttpPost]
		public BoardGameMoveResult<BLL.SpeedCheckersRules.Checkers> MakeMove(MoveRequest request)
		{
			return GameService.Value.MakeMove(request.game_id, request.user_id, request.from, request.to);
		}
	}
}