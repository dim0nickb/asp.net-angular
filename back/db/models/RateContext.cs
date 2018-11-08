using System.Data.Entity;


namespace db
{
	public class RateContext : DbContext
	{
		public RateContext() : base("DbConnection")
		{
			// 
		}

		public DbSet<Rate> Rates { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Rate>()
						.Property(p => p.Value)
						.HasPrecision(10, 3);
		}
	}
}
