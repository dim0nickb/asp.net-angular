using System.Data.Entity;


namespace db
{
	public class CurrencyContext : DbContext
	{
		public CurrencyContext() : base("DbConnection")
		{
			// 
		}

		public DbSet<Currency> Currencies { get; set; }

	}
}
