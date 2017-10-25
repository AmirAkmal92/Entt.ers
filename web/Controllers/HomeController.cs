using Entt.Ers.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entt.Ers.Controllers
{
    [Authorize]
    public class HomeController : ApplicationBaseController
    {
        public async Task<ActionResult> Index()
        {
            var stats = await m_enttContext.GetDashboardData(DateTime.Today);
            var model = new HomeIndexViewModel { Date = DateTime.Today, Statistic = stats };
            if (User.IsInRole(Constansts.Roles.Versus))
            {
                var user = m_dbContext.Users.Single(u => u.UserName == User.Identity.Name);
                if (null != user && !string.IsNullOrEmpty(user.BranchCode) && user.BranchCode != "HQ")
                {
                    model.BranchStatistic = await m_enttContext.GetBranchDashboardData(DateTime.Today, user.BranchCode);
                    model.ShowBranchInfo = true;
                }
            }
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