using _1530Application.HelperClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace _1530Application.Controllers
{
    [Serializable]
    public class ListingController : Controller
    {
        // GET: Listing
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GrabListings()
        {
            DbConnection1530 db = new DbConnection1530();
            string listings = db.SearchMapListings();
            string ser = JsonConvert.SerializeObject(listings);
            Debug.WriteLine(ser);
            return Content(ser);
        }

    }
}