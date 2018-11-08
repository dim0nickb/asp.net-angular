using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace back.Controllers
{
	/// <summary>
	/// Инструменты
	/// </summary>
	public static class Tools
	{
		private static bool IsWeekEnd(DateTime date)
		{
			return date.DayOfWeek == DayOfWeek.Saturday
				|| date.DayOfWeek == DayOfWeek.Sunday;
		}

		/// <summary>
		/// Получение предшествующего рабочего дня
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static DateTime GetPrevWorkingDay(DateTime date)
		{
			while (IsWeekEnd(date))
			{
				date = date.AddDays(-1);
			}
			return date;
		}
	}
}