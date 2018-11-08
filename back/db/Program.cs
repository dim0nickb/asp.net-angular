using System;
using System.Linq;

namespace db
{
	class Program
	{
		static void Main(string[] args)
		{
			/*
			using (RateContext db = new RateContext())
			{
				Console.WriteLine("data stored");

				Console.WriteLine("trying to fetch data from DB");
				var dbRates = db.Rates.Take(5).ToList();
				foreach (Rate rate in dbRates)
				{
					Console.WriteLine($"{ rate.Date} | {rate.Currency.Name} | {rate.Value}");
				}
			}
			Console.WriteLine("press any key....");
			Console.ReadKey();
			return;
			/**/
			string url = "https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/year.txt?year=";
			string year = DateTime.Now.Year.ToString();
			if (args.Length > 0 && args[0].Contains("--year="))
			{
				year = args[0].Substring(args[0].IndexOf("=") + 1);
				Console.WriteLine($"received year param: {year}");
				url += year;
			}

			Console.WriteLine("fetching data...");
			string content = HttpTools.GetText(url).Result;
			if (content.Length > 0)
			{
				Console.WriteLine($"complete\r\ncontent length = {content.Length} bytes\r\nlines = {content.Split('\n').Length}");
				var rates = Convertor.GetRates(content);
				Console.WriteLine("storing data...");
				using (RateContext db = new RateContext())
				{
					db.Rates.AddRange(rates);
					db.SaveChanges();

					Console.WriteLine("data stored");

					Console.WriteLine("fetching data from DB");
					var dbRates = db.Rates.Take(5);
					foreach (Rate rate in dbRates)
					{
						Console.WriteLine($"{ rate.Date} | {rate.Currency.Name} | {rate.Value}");
					}
				}
			} else
			{
				Console.WriteLine("parse errors !");
			}
			Console.WriteLine("press any key to exit...");
			Console.ReadKey();
		}
	}

}
