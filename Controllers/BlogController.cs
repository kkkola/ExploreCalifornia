using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCaliforniaNow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExploreCaliforniaNow.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogContext _db;
        public BlogController(BlogContext db)
        {
            _db = db;
        }
        [Route("")]
        public IActionResult Index(int page=0)
        {
            var pageSize = 2;
            var totalPosts = _db.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previouspage = page - 1;
            var nextpage = page + 1;

            ViewBag.PreviousPage = previouspage;
            ViewBag.HasPreviousPage = previouspage >= 0;
            ViewBag.NextPage = nextpage;
            ViewBag.HasNextPage = nextpage < totalPages;
            var posts = _db.Posts.OrderByDescending(x => x.DT).Skip(pageSize*page).Take(pageSize).ToArray();
            return View(posts);
        }
        

        [Route("{year}/{month:range(1,12)}/{key?}")]
        public IActionResult Post(int year,int month,string key)
        {
            Post post = _db.Posts.FirstOrDefault(x=>x.Key==key);
            
            return View(post);
        }
        [HttpGet, Route("create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, Route("create")]
        public IActionResult Create(Post post)
        {
            //if (!ModelState.IsValid)
              //  return View();
            post.Author = "Kishore";
            post.DT = DateTime.Now;
            _db.Add(post);
            _db.SaveChanges();
            return RedirectToAction("Post","Blog",new { 
                year= post.DT.Year,
                month=post.DT.Month,
                key=post.Key
            });
        }
    }
}
