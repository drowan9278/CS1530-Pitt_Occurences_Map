using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1530Application.Controllers
{
    public class ListingController : Controller
    {
        // GET: Listing
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertListing(string Type, string Lat, string Lng, string Tags, string Details)
        {
            Dictionary<string, string> entries = new Dictionary<string, string>();
            if(Lat.Equals(""))
            {
                Lat = "1111";
                Lng = "1111";
            }
            entries["Xcoord"] = Lat;
            entries["Ycoord"] = Lng;
            entries["Description"] = Details;
            entries["Image"] = "None";
            entries["Upvotes"] = "0";
            entries["Downvotes"] = "0";
            entries["Creator"] = "Anonymous@pitt.edu";
            entries["Tags"] = "None";
            DbConnection1530 dbcon = new DbConnection1530();
            dbcon.InsertMapListing(entries);

            return null;
        }
    }
}