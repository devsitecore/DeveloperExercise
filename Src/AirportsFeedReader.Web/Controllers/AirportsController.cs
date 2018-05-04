using AirportsFeedReader.Foundation.Contracts;
using AirportsFeedReader.Foundation.Extensions;
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
        private readonly string FeedUrl = ConfigurationManager.AppSettings["AirportsFeed"];

        private IFeedReader FeedReader { get; set; }

        public AirportsController(IFeedReader feedReader)
        {
            this.FeedReader = feedReader;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [FromFeedHeader]
        public async Task<JsonResult> GetAirportsList()
        {
            var feedResult = await this.FeedReader.Read(this.FeedUrl);
            var airports = feedResult.Data.ToAirports();

            return this.Json(airports, JsonRequestBehavior.AllowGet);
        }
    }
}