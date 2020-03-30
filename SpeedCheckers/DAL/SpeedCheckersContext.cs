using SpeedCheckers.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SpeedCheckers.DAL
{
	public class SpeedCheckersContext : DbContext
	{
		public SpeedCheckersContext() : base("SpeedCheckersContext")
        {
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Game> Games { get; set; }
		public DbSet<GameMove> Moves { get; set; }

		// disable cascade delete
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
		}

		/*		protected override void OnModelCreating(DbModelBuilder modelBuilder)
				{
					modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
				}
		*/
	}
}