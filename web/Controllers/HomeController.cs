﻿using Entt.Ers.Models;
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

                    string[] colors = { "#66BB6A", "#9575CD", "#FF7043", "#29B6F6", "#EF5350", "#81C784", "#999", "#DCEDC8", "#C5E1A5", "#9CCC65", "#7CB342", "#558B2F"};
                    var acceptanceByCategories = await m_enttContext.GetBranchAcceptanceData(DateTime.Today, user.BranchCode);
                    ViewBag.AcceptanceByCategories = acceptanceByCategories.Select(a => new { browser = a.CategoryName, value = a.Count, color = colors[a.Index], icon = "<i class='icon-file-empty position-left'></i>" });
                }
            }
            return View(model);
        }

        public ActionResult Search()
        {
            var vm = new HomeSearchViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(HomeIndexViewModel model)
        {
            var searchKey = model.SearchKey;
            if (string.IsNullOrEmpty(searchKey)) return View(new HomeSearchViewModel { Acceptance = null, SearchKey = searchKey });
            var acceptance = m_enttContext.SearchAcceptance(searchKey.Trim());
            var vm = new HomeSearchViewModel { Acceptance = acceptance, SearchKey = searchKey };
            return View(vm);
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