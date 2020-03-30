using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedCheckers.Models
{
	public class Game
	{
		public Guid ID { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public virtual User PlayerWhite { get; set; }
		public virtual User PlayerBlack { get; set; }
		public virtual User Winner { get; set; }
		public Guid PlayerWhiteId { get; set; }
		public Guid PlayerBlackId { get; set; }
		public Guid? WinnerId { get; set; }
		public virtual List<GameMove> Moves { get; set; }
	}
}
