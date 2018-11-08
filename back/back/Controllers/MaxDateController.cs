using System.Data;
using System.Linq;
using System.Web.Http;
using db;
using System.Web.Http.Cors;

namespace back.Controllers
{
	[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
	public class MaxDateController : ApiController
	{
		private RateContext database = new RateContext();

		// GET: api/MaxDate
		public string GetMaxDate()
		{
			var MaxDate = (from d in database.Rates select d.Date).Max();
			return MaxDate.ToString("yyyy-MM-dd");
		}
	}
}