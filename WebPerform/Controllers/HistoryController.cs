using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPerform.App_Code;
using WebPerform.Models;

namespace WebPerform.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        // static IList<SiteInfo> sitesInfo = new List<SiteInfo>();
        ModelSiteContext db;
       
        public HistoryController()
        {
            db = new ModelSiteContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<string> sitestemp = (from site in db.Sites
                                     select site.Site).ToList();
            HashSet<string> sites = new HashSet<string>(sitestemp);
            ViewBag.Sites = new SelectList(sites); 
           return View(new List< SiteInfo>());
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            List<string> sitestemp = (from site in db.Sites
                                      select site.Site).ToList();
            HashSet<string> sites = new HashSet<string>(sitestemp);
            ViewBag.Sites = new SelectList(sites);
            List<SiteInfo> sitesdata = null;
            string st = form["Site"].ToString();
            if ( st!= null) {
                sitesdata = (from site in db.Sites
                                        where site.Site == st
                                        orderby site.SpeedMax descending
                                        select site).ToList();
                                  }
            if(sitesdata!=null)
                 return View(sitesdata); 
            else 
               return View();
        }
        public JsonResult DrawChart(string site)
        {
            List<string> temp = new List<string>();
            List<SiteInfo> sitesInfo = new List<SiteInfo>();
            if (site != null)
            {
                sitesInfo = (from item in db.Sites
                             where item.Site == site
                             orderby item.SpeedMax descending 
                             select item).ToList();
            }
            foreach (var item in sitesInfo)
            {
                temp.Add(item.ToString());
            }
            return Json(temp, JsonRequestBehavior.AllowGet);
        }
        private bool disposed = false;
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposed);
        }
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}