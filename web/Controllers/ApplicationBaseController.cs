using Entt.Ers.Models;
using System.Linq;
using System.Web.Mvc;

namespace Entt.Ers.Controllers
{
    public class ApplicationBaseController : Controller
    {
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