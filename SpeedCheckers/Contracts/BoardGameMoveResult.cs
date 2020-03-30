using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.Contracts
{
	public class BoardGameMoveResult<TFigure>
	{
        internal static bool ok;

        public BoardGameState<TFigure> State { get; set; }
		public bool Ok { get; set; }
	}
}