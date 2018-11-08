using db;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace back.Controllers
{
	[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
	public class MinDateController : ApiController
	{
		private RateContext database = new RateContext();

		// GET: api/MinDate
		public string GetMinDate()
		{
			var MinDate = (from d in database.Rates select d.Date).Min();
			return MinDate.ToString("yyyy-MM-dd");
		}
	}
}