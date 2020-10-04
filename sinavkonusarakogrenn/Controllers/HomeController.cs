using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sinavkonusarakogrenn.Data;
using sinavkonusarakogrenn.Data.Entites;
using sinavkonusarakogrenn.Models;
using sinavkonusarakogrenn.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sinavkonusarakogrenn.Controllers
{
    //    [UserFilter]
    public class HomeController : Controller
    {
        public readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            string url = $"https://www.wired.com/most-recent/";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var ul = doc.DocumentNode.SelectNodes("//ul[@class='archive-list-component__items']");
            var li = ul.Descendants("li").Skip(5).ToList();

            List<PostModel> posts = new List<PostModel>();
            foreach (var item in li)
            {
                var h2 = item.Descendants("h2").First();
                var p = item.Descendants("p").First();

                posts.Add(new PostModel
                {
                    Title = h2.InnerText,
                    Description = p.InnerText
                });
            }

            ViewBag.Posts = JsonConvert.SerializeObject(posts);

            return View();
       }

        [HttpPost]
        [Route("")]
        public IActionResult Index(CreatePostViewModel model)
        {
            var post = new Post
            {
                Title = model.Title,
                Description = model.Description,
                Question = model.Questions.Select(q => new Question //özellikleri filtrelemek için
                {
                    Description = q.Description,
                    A = q.A,
                    B = q.B,
                    C = q.C,
                    D = q.D,
                    Answer = q.Answer
                }).ToList(),
                CreatedDate = DateTime.Now
            };

            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

   

        [HttpGet]
        [Route("Post/List")]
        public ActionResult PostList()
        {
            var posts = _dbContext.Posts.OrderByDescending(w => w.CreatedDate).ToList();
            return View(posts);
        }
        [Route("Post/Delete")]
        public ActionResult Delete(int id)
        {
           var post = _dbContext.Posts.Find(id);      
           _dbContext.Posts.Remove(post);
           _dbContext.SaveChanges();
           return RedirectToAction("PostList", "Home");
        }

        [HttpGet]
        [Route("Exeam/{id:int}")]
        public ActionResult Exeam(int id)
        {
            var post = _dbContext.Posts
                .Where(x => x.Id == id)
                .Select(x => new Post
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    Question = x.Question
                }).FirstOrDefault();

            var model = new ExeamViewModel();
            model.Id = post.Id;
            model.Title = post.Title;
            model.Description = post.Description;
            model.Questions = post.Question;

            return View(model);
        }

        [HttpGet]
        [Route("ExeamResult/{id:int}")]
        public ActionResult ExeamResult(int id)
        {
            var post = _dbContext.Posts
                .Where(x => x.Id == id)
                .Select(x => new Post
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    Question = x.Question
                }).FirstOrDefault();

            return Ok(post.Question.Select(x => new
            {
                x.Id,
                x.Answer
            }));
            //değişik tilerde döndürülebiliyor
        }
    }
}
