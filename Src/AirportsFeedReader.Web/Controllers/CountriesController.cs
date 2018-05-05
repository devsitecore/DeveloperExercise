// <copyright file="CountriesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Foundation.Contracts;

    public class CountriesController : Controller
    {
        public CountriesController(ICountryRepository countryRepository)
        {
            this.CountryRepository = countryRepository;
        }

        private ICountryRepository CountryRepository { get; set; }

        [OutputCache(Duration = int.MaxValue, VaryByParam = "none")]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [OutputCache(Duration = int.MaxValue, VaryByParam = "none")]
        public JsonResult GetCountriesList()
        {
            var countries = this.CountryRepository.GetCountries();
            return this.Json(countries, JsonRequestBehavior.AllowGet);
        }
    }
}