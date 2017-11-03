using Entt.Ers.Models;
using System.Web.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Entt.Ers.Controllers
{
    public class DemoController : ApplicationBaseController
    {
        public ActionResult NearMe()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NearMe(StandardReportViewModel model)
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> NearbyBranch(decimal lattitude, decimal longitude)
        {
            var branches = await m_enttContext.SearchNearbyBranch(lattitude, longitude, 3);
            return Json(new { status = "OK", result = branches.Select(b => new { name = b.Name, distance = b.Distance }) }, JsonRequestBehavior.AllowGet);
        }
    }

}