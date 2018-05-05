using AirportsFeedReader.Foundation.Contracts;
using AirportsFeedReader.Foundation.Extensions;
using AirportsFeedReader.Foundation.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AirportsFeedReader.Web.Controllers
{
    public class AirportsController : Controller
    {
        private IAirportRepository AirportRepository { get; set; }

        public AirportsController(IAirportRepository airportRepository)
        {
            this.AirportRepository = airportRepository;
        }

        [HttpGet]
        [OutputCache(Duration = int.MaxValue, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [FromFeedHeader]
        public async Task<JsonResult> GetAirportsList()
        {
            var airports = await this.AirportRepository.GetAirports();
            return this.Json(airports, JsonRequestBehavior.AllowGet);
        }
    }
}