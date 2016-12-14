using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPerform.App_Code;
using WebPerform.Models;

namespace WebPerform.Controllers
{
    public class MainController : Controller
    {
       static List<SiteInfo> sitesInfo = new List<SiteInfo>();
        ModelSiteContext db;
        public MainController()
        {
            db = new ModelSiteContext();
        }
        // GET: Main
        //[HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SiteParse(string urlName, List<string> urlArray)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            List<string> urls = new List<string>();
            HashSet<string> urls15 = new HashSet<string>();
            if (urlArray != null & urlName != null) urls = SiteMapper.BuildSiteMap(urlName, urlArray);
            //int j = 0;
            //if (urls.Count > 15)
            //{
            //    while (urls15.Count < 15)
            //    {
            //        int rand = r.Next(0, urls.Count);
            //        urls15.Add(urls.ElementAt(rand));
            //    }
            //}
            //else {
                foreach (var item in urls)
                {
                    urls15.Add(item);
                }
            //}
            return Json(urls15);
        }
        [HttpPost]
        public ActionResult DrawData(string urlName, List<string> chartInfo)
        {
            if (chartInfo != null)
            {
               sitesInfo.Clear();
                for (int i = 1; i < chartInfo.Count; i++)
                {
                    string[] chr = chartInfo[i].Split(',');
                    string st = chr[0];
                    SiteInfo tempInfo = new SiteInfo(urlName, chr[0], Int32.Parse(chr[1]), Int32.Parse(chr[2]));
                    sitesInfo.Add(tempInfo);
                     var  finder = db.Sites.Where(x => x.PageName == st)
                                      .FirstOrDefault();
                    if (finder == null)
                    {
                        db.Sites.Add(tempInfo);
                            db.SaveChanges();
                    }
                    else
                    {
                        finder.SpeedMax = tempInfo.SpeedMax;
                        finder.SpeedMin = tempInfo.SpeedMin;
                        db.Entry(finder).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    };

                }
               
            }
            return new EmptyResult();
        }
        [HttpPost]
        public JsonResult DrawChart()
        {
            List<string> temp = new List<string>();
            var sites = sitesInfo.OrderBy(d => d.PageName).ToList();
            foreach (var item in sites)
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