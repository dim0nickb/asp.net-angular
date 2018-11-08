namespace db
{
	/// <summary>
	/// Валюта
	/// </summary>
	public class Currency
	{
		/// <summary>
		/// Индекс
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Название
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Кол-во
		/// </summary>
		public int Amount { get; set; }
	}
}
