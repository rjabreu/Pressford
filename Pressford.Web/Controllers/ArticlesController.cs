using Pressford.Web.DatabaseContext;
using Pressford.Web.Models;
using Pressford.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pressford.Web.Controllers
{
    public class ArticlesController : Controller
    {
       
        public ActionResult Index()
        {
            var viewModel = new ArticlePageViewModel
            {
                Title = "Articles",
                Articles = GetAllArticles()
            };

            return View(viewModel);
        }

        public ActionResult Create(Article newArticle)
        {

            return View();
        }

        public ActionResult ArticleDetail(int articleId)
        {
            var db = new ArticlesDb();
            var article = db.Articles.Where(x => x.Id == articleId).FirstOrDefault();
            ArticleDetailPageViewModel viewModel = new ArticleDetailPageViewModel
            {
                Article = article
            };

            return View(viewModel);
        }

        public ActionResult EditArticle(int articleId)
        {
            var db = new ArticlesDb();
            var article = db.Articles.Where(x => x.Id == articleId).FirstOrDefault();
            ArticleDetailPageViewModel viewModel = new ArticleDetailPageViewModel
            {
                Article = article
            };

            return View(viewModel);
        }

        private List<Article> GetAllArticles()
        {
            var db = new ArticlesDb();

            return db.Articles.ToList();
        }

        private Article GetArticleById(int id)
        {
            var db = new ArticlesDb();

            return db.Articles.Where(x => x.Id == id).FirstOrDefault();
        }

        public ActionResult CreateArticle(Article article)
        {
            var db = new ArticlesDb();
            article.PublishedDate = DateTime.Now;
            db.Articles.Add(article);
            db.SaveChanges();

            return Content("new item creted");

        }

        public ActionResult DeleteArticle(int id)
        {         
            var db = new ArticlesDb();

            var article = db.Articles.Find(id);
            if (article != null)
            {

                db.Articles.Remove(article);

            }else
            {
                Response.StatusCode = 404;
                return Content("Article not found.") ;
            }

            TempData["success"] = "Article deleted successfully";
            db.SaveChanges();

            
            return RedirectToAction("Index");
        }

        public ActionResult UpdateArticle(Article updatedArticle)
        {
            var db = new ArticlesDb();

            var storedArticle = db.Articles.Find(updatedArticle.Id);
            if (storedArticle != null)
            {
                storedArticle.Content = updatedArticle.Content;
                storedArticle.Title = storedArticle.Title;
                db.SaveChanges();
                TempData["success"] = "Article updated";
                return RedirectToAction("Index");
            }
            Response.StatusCode = 404;
            return Content("Article not found");
        }
    }
}