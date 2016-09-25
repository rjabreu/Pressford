using Microsoft.AspNet.Identity;
using Pressford.Web.DatabaseContext;
using Pressford.Web.Models;
using Pressford.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pressford.Web.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        [Authorize(Roles = "Employee, Admin")]
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

        [Authorize(Roles = "Employee, Admin")]
        public ActionResult ArticleDetail(int articleId)

        { 
            var article = GetArticleById(articleId);

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

            return db.Articles.Find(id);
        }

        public ActionResult CreateArticle(Article article)
        {
          

            if (HttpContext.User.Identity.IsAuthenticated)
            {

                article.Author = HttpContext.User.Identity.Name;
            }
            else
            {
                article.Author = "Ricardo"; //this is when testing and not logged in
            }
            


            var db = new ArticlesDb();
            article.PublishedDate = DateTime.Now;
            db.Articles.Add(article);
            db.SaveChanges();
            TempData["success"] = "Article created successfully";
            return RedirectToAction("Index");

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

            var storedArticle = GetArticleById(updatedArticle.Id);
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

        [Authorize(Roles = "Employee, Admin")]
        public  async Task<ActionResult> AddLike(int id)
        {
            var db = new ArticlesDb();
            var likeDb = new LikesDb();
            var article =  db.Articles.Find(id);
            var userId = HttpContext.User.Identity.GetUserId();
            var like = likeDb.Likes.Where(x => x.UserId == userId && x.ArticleId == article.Id).FirstOrDefault();
            //user already liked this so remove like
            if (like != null)
            {
                if (article != null)
                {
                    likeDb.Likes.Remove(like);
                    await likeDb.SaveChangesAsync();
                    article.Likes = await GetArticleLikes(id);
                    await db.SaveChangesAsync();
                }
            }else
            {
                if (article != null)
                {
                    like = new Like();
                    like.ArticleId = article.Id;
                    like.UserId = userId;
                    likeDb.Likes.Add(like);
                    await likeDb.SaveChangesAsync();
                    article.Likes =  await GetArticleLikes(id) ;
                    await db.SaveChangesAsync();

                }
            }

            return PartialView("~/Views/Articles/Partials/LikesCount.cshtml", article);

        }

        [Authorize(Roles = "Employee, Admin")]
        public  async Task<double> GetArticleLikes(int id)
        {
            var db = new LikesDb();
            double likesCount = db.Likes.Where(x => x.ArticleId == id).Count();
            return likesCount;
        }


        [Authorize(Roles = "Employee, Admin")]
        public async Task<ActionResult> AddComment(Comment comment)
        {
            var articlesDb = new ArticlesDb();
            var article = await articlesDb.Articles.FindAsync(comment.ArticleId);
            var commentsDb = new CommentsDb();

            comment.PublishedDate = DateTime.Now;
            comment.UserName = HttpContext.User.Identity.Name;
            commentsDb.Comments.Add(comment);

            await articlesDb.SaveChangesAsync();
            await commentsDb.SaveChangesAsync();

            return RedirectToAction("ArticleDetail",new { articleId = article.Id });
        }


        public PartialViewResult RenderComments(int id)
        {
            var commentsDb = new CommentsDb();
        
            return PartialView("~/Views/Articles/Partials/ArticleCommentsPartial.cshtml", 
                commentsDb.Comments.Where(x => x.ArticleId == id).ToList());
        }
    }
}

