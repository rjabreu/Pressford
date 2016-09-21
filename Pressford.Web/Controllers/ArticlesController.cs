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
        // GET: CreateArticle
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

        private void DeleteArticle(int id)
        {         
            var db = new ArticlesDb();

            db.Articles.Remove(db.Articles.Find(id));
            db.SaveChanges();
        }

        private void UpdateArticle(Article updatedArticle)
        {
            var db = new ArticlesDb();

            var storedArticle = db.Articles.SingleOrDefault(b => b.Id == updatedArticle.Id);
            if (storedArticle != null)
            {
                storedArticle = updatedArticle;
                db.SaveChanges();
            }
        }
    }
}