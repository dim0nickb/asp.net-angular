using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using db;

namespace back.Controllers
{
	[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
	public class RatesController : ApiController
    {


		private RateContext database = new RateContext();

		// GET: api/Rates/currencyId/date
		public string GetRates(int currencyId, string date)
		{
			DateTime parsedDate = DateTime.Now;
			if (DateTime.TryParse(date, out parsedDate))
			{
				parsedDate = Tools.GetPrevWorkingDay(parsedDate);
				var data = database.Rates.FirstOrDefault(_ => _.CurrencyId == currencyId && _.Date == parsedDate);
				if (data == null)
					throw new HttpResponseException(HttpStatusCode.NotFound);
				return data.Value.ToString();
			} else
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
		}

		// GET: api/Rates
		public IQueryable<Rate> GetRates()
        {
            return database.Rates;
        }

        // GET: api/Rates/5
        [ResponseType(typeof(Rate))]
        public async Task<IHttpActionResult> GetRate(int id)
        {
            Rate rate = await database.Rates.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

            return Ok(rate);
        }

        // PUT: api/Rates/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRate(int id, Rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rate.Id)
            {
                return BadRequest();
            }

			database.Entry(rate).State = EntityState.Modified;

            try
            {
                await database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rates
        [ResponseType(typeof(Rate))]
        public async Task<IHttpActionResult> PostRate(Rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			database.Rates.Add(rate);
            await database.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rate.Id }, rate);
        }

        // DELETE: api/Rates/5
        [ResponseType(typeof(Rate))]
        public async Task<IHttpActionResult> DeleteRate(int id)
        {
            Rate rate = await database.Rates.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

			database.Rates.Remove(rate);
            await database.SaveChangesAsync();

            return Ok(rate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				database.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RateExists(int id)
        {
            return database.Rates.Count(e => e.Id == id) > 0;
        }
    }
}