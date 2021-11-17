using Microsoft.AspNet.Identity;
using MusicTwitter.Data;
using MusicTwitter.Models;
using MusicTwitter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicTwitter.WebMVC.Controllers
{
    public class SongPostController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: SongPost
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongPostService(userId);
            var model = service.GetSongPosts();

            return View(model);
        }

        //Add method here
        //GET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SongPostCreate model)
        {
            var service = CreateSongPostService();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (service.CreateSongPost(model))
            {
                TempData["SaveResult"] = "Your post was created.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {

            var detail = _db.SongPosts.Find(id);

            var model = new SongPostEdit
            {
                Message = detail.Message,
                SongPostId = detail.SongPostId,
                SongTitle = detail.SongTitle,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SongPostEdit model)
        {
            if (ModelState.IsValid)
            {
                var service = CreateSongPostService();

                service.EditSongPost(model);
                TempData["SaveResult"] = "Post successfully edited";
                return RedirectToAction("Index");
            }

            if (model.SongPostId != id)
            {
                ModelState.AddModelError("", "Id mismatch");
                return View(model);
            }

            ModelState.AddModelError("", "Your post could not be updated");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var service = CreateSongPostService();
            var model = service.GetSongPostById(id);

            new SongPostDetail
            {
                Message = model.Message,
                CreatedUtc = model.CreatedUtc,
                ModifiedUtc = model.ModifiedUtc,
                SongPostId = model.SongPostId,
                SongTitle = model.SongTitle
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSongPost(int id)
        {
            var service = CreateSongPostService();

            if (service.DeleteSongPost(id))
            {
                TempData["SaveResult"] = "Note successfully deleted";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could not be deleted");
            return View();
        }

        private SongPostService CreateSongPostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongPostService(userId);
            return service;
        }
    }
 

    
}