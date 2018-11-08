using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db
{
	public static class Convertor
	{
		/// <summary>
		/// Парсинг текста в объекты курса валют
		/// </summary>
		public static List<Rate> GetRates(string data)
		{
			List<Rate> res = new List<Rate>();
			List<Currency> curTemps = new List<Currency>();
			int cursCount = 0;
			string[] lines = data.Split('\n');
			for (int i = 0; i < lines.Length; i++)
			{
				string[] values = lines[i].Split('|');
				if (i == 0) {
					for (int j = 1; j < values.Length; j++)
					{
						string name = values[j].Substring(values[j].IndexOf(' ') + 1);
						string amountstr = values[j].Substring(0, values[j].IndexOf(' '));
						int amount = 0;
						if (int.TryParse(amountstr, out amount))
						{
							curTemps.Add(new Currency { Name = name, Amount = amount });
						} else
						{
							throw new Exception("parsing error: amount");
						}
					}
					cursCount = curTemps.Count;
				} else
				{
					DateTime date = DateTime.Now;
					if (DateTime.TryParse(values[0], out date))
					{
						for (int j = 1; j < values.Length; j++)
						{
							string valuestr = values[j];
							decimal decimalValue = 0;
							if (decimal.TryParse(valuestr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalValue) && cursCount >= j)
							{
								Rate r = new Rate { Date = date, Value = decimalValue, Currency = curTemps[j-1] };
								res.Add(r);
							}
							else
							{
								throw new Exception("parsing error: decimalValue");
							}
						}
						
					}
				}
			}
			return res;
		}
	}
}
