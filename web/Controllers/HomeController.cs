using Entt.Ers.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entt.Ers.Controllers
{
    [Authorize]
    public class HomeController : ApplicationBaseController
    {
        private EnttReportDataContext m_enttContext = new EnttReportDataContext();

        public async Task<ActionResult> Index()
        {
            var stats = await m_enttContext.GetDashboardData(DateTime.Today);
            var model = new HomeIndexViewModel { Date = DateTime.Today, Statistic = stats };
            return View(model);
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