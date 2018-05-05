﻿// <copyright file="AirportsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Foundation.Contracts;

    public class AirportsController : Controller
    {
        public AirportsController(IAirportRepository airportRepository)
        {
            this.AirportRepository = airportRepository;
        }

        private IAirportRepository AirportRepository { get; set; }

        [HttpGet]
        [OutputCache(Duration = int.MaxValue, VaryByParam = "none")]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [OutputCache(Duration = int.MaxValue, VaryByParam = "none")]
        public ActionResult Distance()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<JsonResult> CalculateDistance(string source, string destination)
        {
            var distance = await this.AirportRepository.CalculateDistance(source, destination);
            return this.Json(distance, JsonRequestBehavior.AllowGet);
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