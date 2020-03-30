using SpeedCheckers.Contracts;
using SpeedCheckers.GameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.BLL
{
	public class SpeedCheckersGameService
		: BaseBOardGameService<SpeedCheckersRules.Checkers, SpeedCheckersRules.SpeedCheckersRules>
	{
		public SpeedCheckersGameService() : base(new GameBLL(), new GameMoveBLL()) {
		}

	}
}