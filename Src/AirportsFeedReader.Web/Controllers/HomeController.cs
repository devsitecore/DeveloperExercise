namespace AirportsFeedReader.Web.Controllers
{
    using Data;
    using Foundation.Contracts;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}