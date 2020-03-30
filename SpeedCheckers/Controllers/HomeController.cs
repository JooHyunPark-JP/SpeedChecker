using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedCheckers.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Game(Guid game_id)
		{
			ViewBag.game_id = game_id;
			var players = new BLL.GameBLL().GetPlayersForGame(game_id);

			ViewBag.white_player_name = players[0].Name;
			ViewBag.black_player_name = players[1].Name;

			var state = SpeedCheckersController.GameService.Value.GetState(game_id);
			ViewBag.state = Newtonsoft.Json.JsonConvert.SerializeObject(state);
			return View();
		}

		public ActionResult Result(Guid game_id)
		{
			ViewBag.game_id = game_id;
			var result = new BLL.GameBLL().GetGameResults(game_id);
			ViewBag.winner = result.Winner.Name;
			ViewBag.loser = result.Loser.Name;
			return View();
		}

		public ActionResult History()
		{
			var finishedGames = new BLL.GameBLL().GetFinishedGames();
			ViewBag.data = finishedGames;
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}