using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualClinicWebApi.Controllers
{
    [Route("api/[controller]")]
    public class CountryStateCityController : Controller
    {
        // GET: CountryStateCity
        public ActionResult Index()
        {
            return View();
        }
    }
}