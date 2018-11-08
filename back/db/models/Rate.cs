using System;
using System.Data.Entity;

namespace db
{
	/// <summary>
	/// Курс на дату
	/// </summary>
	public class Rate
	{
		/// <summary>
		/// Индекс
		/// </summary>
		public int Id { get; set;}
		/// <summary>
		/// Дата
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// Валюта (внешний ключ)
		/// </summary>
		public int? CurrencyId { get; set; }
		/// <summary>
		/// Валюта
		/// </summary>
		public virtual Currency Currency { get; set; }
		/// <summary>
		/// Зачение на дату
		/// </summary>
		public decimal Value { get; set; }
	}
}
