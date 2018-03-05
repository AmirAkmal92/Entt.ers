using Entt.Ers.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entt.Ers.Controllers
{
    public class ConsignmentController : ApplicationBaseController
    {
        // GET: Consignment
        public async Task<ActionResult> Index(string cn)
        {
            var item = await m_enttContext.GetConsignmentInfoById(cn);
            var vm = new ConsignmentViewModel { Acceptance = item };
            return View(vm);
        }
    }
}