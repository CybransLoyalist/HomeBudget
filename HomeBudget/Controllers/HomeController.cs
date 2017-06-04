using System.Web.Mvc;
using HomeBudget.Models.Repositories;

namespace HomeBudget.Controllers
{
    public class HomeController : Controller
    {
        private readonly YearSheetRepository _yearSheetRepository;

        public HomeController(YearSheetRepository yearSheetRepository)
        {
            _yearSheetRepository = yearSheetRepository;
        }

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var yearSheet = _yearSheetRepository.GetForUser(User);
            if (yearSheet == null)
            {
                return RedirectToAction("NoYearSheetPresent", "YearSheet");
            }

            return RedirectToAction("Current", "YearSheet");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}