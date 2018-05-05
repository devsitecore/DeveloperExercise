
namespace AirportsFeedReader.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Foundation.Contracts;

    public class CountriesController : Controller
    {
        private ICountryRepository CountryRepository { get; set; }

        public CountriesController(ICountryRepository countryRepository)
        {
            this.CountryRepository = countryRepository;
        }

        [OutputCache(Duration = int.MaxValue, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(Duration = int.MaxValue, VaryByParam = "none")]
        public async Task<JsonResult> GetCountriesList()
        {
            var countries = await this.CountryRepository.GetCountries();
            return this.Json(countries, JsonRequestBehavior.AllowGet);
        }
    }
}