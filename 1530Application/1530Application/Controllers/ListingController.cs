
﻿using _1530Application.HelperClasses;
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
    public class ListingController : Controller
    {
        // GET: Listing
        public ActionResult Index()
        {
            return View();
        }
	
	[HttpPost]
        public ActionResult Upvote(int listID, int voteType)
        {
	        DbConnection1530 dbcon = new DbConnection1530();
            dbcon.UpvoteListing(listID);
            return null;
        }
	
	[HttpPost]
        public ActionResult Downvote(int listID, int voteType)
        {
	        DbConnection1530 dbcon = new DbConnection1530();
            dbcon.DownvoteListing(listID);
            return null;
        }

        public ActionResult GrabListings()
        {
            DbConnection1530 db = new DbConnection1530();
            string listings = db.SearchMapListings();
            string ser = JsonConvert.SerializeObject(listings);
            Debug.WriteLine(ser);
            return Content(ser);
        }


        [HttpPost]
        public ActionResult InsertListing(string Type, string Lat, string Lng, string Tags, string Details)
        {
            Dictionary<string, string> entries = new Dictionary<string, string>();
            if(Lat.Equals(""))
            {
                Lat = "45";
                Lng = "-75";
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

