using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace db
{
	public static class HttpTools
	{
		private static readonly HttpClient m_httpClient = new HttpClient();
		/// <summary>
		/// Получение данных с ресурса в вие строки
		/// </summary>
		/// <param name="url">Адрес ресурса</param>
		/// <returns>Данные</returns>
		public static async Task<string> GetText(string url)
		{
			try
			{
				using (var result = await m_httpClient.GetAsync(url))
				{
					string content = await result.Content.ReadAsStringAsync();
					return content;
				}
			}
			catch
			{
				return "";
			}
		}
	}
}
