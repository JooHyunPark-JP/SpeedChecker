using SpeedCheckers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.Contracts
{
	public class GameResult
	{
		public User Winner { get; set; }
		public User Loser { get; set; }
	}
}