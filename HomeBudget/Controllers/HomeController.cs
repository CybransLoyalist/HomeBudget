using System.Web.Mvc;
using HomeBudget.Models.Repositories;
using Microsoft.AspNet.Identity;

namespace HomeBudget.Controllers
{
    public class HomeController : Controller
    {
        private readonly YearSheetsRepository _yearSheetsRepository;

        public HomeController(YearSheetsRepository yearSheetsRepository)
        {
            _yearSheetsRepository = yearSheetsRepository;
        }

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            var yearSheet = _yearSheetsRepository.GetForUser(User.Identity.GetUserId());
            if (yearSheet == null)
            {
                return RedirectToAction("NoYearSheetPresent", "YearSheet");
            }

            return RedirectToAction("Current", "YearSheet");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}