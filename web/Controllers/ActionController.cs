using System.Web.Mvc;

namespace Entt.Ers.Controllers
{
    public class ActionController : ApplicationBaseController
    {
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}