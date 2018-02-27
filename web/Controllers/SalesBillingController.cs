using System.Web.Mvc;

namespace Entt.Ers.Controllers
{
    [Authorize(Roles = "SNB")]
    public class SalesBillingController : ApplicationBaseController
    {
        public ActionResult Financial()
        {
            return View();
        }

        public ActionResult Management()
        {
            return View();
        }
    }
}