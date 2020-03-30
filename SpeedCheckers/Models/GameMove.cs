using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.Models
{
	public class GameMove
	{
		public Guid ID { get; set; }
		public virtual Game Game { get; set; }
		public Guid GameId { get; set; }
		public int Number { get; set; }
		public Guid PlayerId { get; set; }
		public virtual User Player { get; set; }
		public string MoveFrom { get; set; }
		public string MoveTo { get; set; }
	}
}