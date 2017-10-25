using Entt.Ers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Entt.Ers.Controllers
{
    public class ApplicationBaseController : Controller
    {
        protected EnttReportDataContext m_enttContext = new EnttReportDataContext();
        protected ApplicationDbContext m_dbContext = new ApplicationDbContext();

        public List<Branch> GetUserViewBranches()
        {
            var list = new List<Branch>();
            var pplList = m_enttContext.GetPplBranches();

            if (User.IsInRole(Constansts.Roles.VersusHQ))
            {
                pplList.Insert(0, new Branch { Code = "All", Name = "All" });
                list = pplList.ToList();
            }
            else
            {
                var user = m_dbContext.Users.Single(u => u.UserName == User.Identity.Name);
                list = pplList.Where(p => p.Code == user.BranchCode).ToList();
            }
            return list;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new ApplicationDbContext();
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == username);
                    ViewData.Add("FullName", user.FullName);
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}