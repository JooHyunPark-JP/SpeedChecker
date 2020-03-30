using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.Contracts
{
	public class FinishedGame
	{
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string WhitePlayerName { get; set; }
		public string BlackPlayerName { get; set; }
		public string WinnerName { get; set; }
	}
}
