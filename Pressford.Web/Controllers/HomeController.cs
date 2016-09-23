using Pressford.Web.DatabaseContext;
using Pressford.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pressford.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomePageViewModel vm = new HomePageViewModel
            {
                Articles = new ArticlesDb().Articles.OrderByDescending(x => x.PublishedDate).Take(3).ToList()
            };

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Feel free to reach us anytime.";

            return View();
        }
    }
}