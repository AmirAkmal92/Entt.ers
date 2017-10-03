using Entt.Ers.Models;
using System.Web.Mvc;

namespace Entt.Ers.Controllers
{
    [Authorize]
    public class HomeController : ApplicationBaseController
    {
        private ApplicationDbContext m_context = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
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