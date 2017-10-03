using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Entt.Ers.Controllers
{
    public class HelperController : ApplicationBaseController
    {
        public ActionResult GetStates()
        {
            var states = new List<SelectListItem>();
            states.Add(new SelectListItem { Text = "Johor", Value = "Johor" });
            states.Add(new SelectListItem { Text = "Kedah", Value = "Kedah" });
            states.Add(new SelectListItem { Text = "Kelantan", Value = "Kelantan" });
            states.Add(new SelectListItem { Text = "Melaka", Value = "Melaka" });
            states.Add(new SelectListItem { Text = "Negeri Sembilan", Value = "Negeri Sembilan" });
            states.Add(new SelectListItem { Text = "Pahang", Value = "Pahang" });
            states.Add(new SelectListItem { Text = "Perak", Value = "Perak" });
            states.Add(new SelectListItem { Text = "Perlis", Value = "Perlis" });
            states.Add(new SelectListItem { Text = "Pulau Pinang", Value = "Pulau Pinang" });
            states.Add(new SelectListItem { Text = "Sabah", Value = "Sabah" });
            states.Add(new SelectListItem { Text = "Sarawak", Value = "Sarawak" });
            states.Add(new SelectListItem { Text = "Selangor", Value = "Selangor" });
            states.Add(new SelectListItem { Text = "Terengganu", Value = "Terengganu" });
            states.Add(new SelectListItem { Text = "W.P Kuala Lumpur", Value = "W.P Kuala Lumpur" });
            states.Add(new SelectListItem { Text = "W.P Labuan", Value = "W.P Labuan" });
            states.Add(new SelectListItem { Text = "W.P Putrajaya", Value = "W.P Putrajaya" });

            return Json(states, JsonRequestBehavior.AllowGet);
        }
    }
}