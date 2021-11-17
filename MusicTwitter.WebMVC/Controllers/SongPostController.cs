using Microsoft.AspNet.Identity;
using MusicTwitter.Data;
using MusicTwitter.Models;
using MusicTwitter.Models.CommentModels;
using MusicTwitter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicTwitter.WebMVC.Controllers
{
    [RoutePrefix("[controller]")]
    public class SongPostController : Controller
    {
        

        // GET: SongPost
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongPostService(userId);
            var model = service.GetSongPosts();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
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
            var service = CreateSongPostService();

            var detail = service.GetSongPostById(id);

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
            var service = CreateSongPostService();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(model.SongPostId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            if (service.EditSongPost(model))
            {
                TempData["SaveResult"] = "Post was successfully modified.";
                return RedirectToAction("Index");
            }

            
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var service = CreateSongPostService();
            var model = service.GetSongPostById(id);

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSongPost(int id)
        {
            var service = CreateSongPostService();

            if (service.DeleteSongPost(id))
            {
                TempData["SaveResult"] = "Post successfully deleted";
                return RedirectToAction("Index");
            }

            
            return View();
        }

        private SongPostService CreateSongPostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongPostService(userId);
            return service;
        }

        public ActionResult Comment(int id)
        {
            var service = CreateSongPostService();

            var viewModel = new CommentCreate();

            viewModel.SongPostDetail = service.GetSongPostById(id);
            viewModel.SongPostId = id;

            return View(viewModel);
        }

        [HttpPost, ActionName("Comment")]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(CommentCreate model)
        {
            var service = CreateCommentService();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (service.CreateComment(model))
            {
                TempData["SaveResult"] = "Comment added";
                return RedirectToAction("Comment", model.SongPostId);
            }

            return View(model);
        }

        public ActionResult CommentDelete(int id)
        {
            var cmtService = CreateCommentService();

            var comment = cmtService.GetCommentById(id);

            var viewModel = new CommentDetail
            {
                Content = comment.Content,
                SongPostDetail = comment.SongPostDetail,
                OwnerId = comment.OwnerId,
                SongPostId = comment.SongPostId,
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("CommentDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int id)
        {
            var service = CreateCommentService();

            if (service.DeleteComment(id))
            {
                TempData["SaveResult"] = "Comment deleted";
                return RedirectToAction("Index");
            }

            return View();
        }


        private CommentServices CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentServices(userId);
            return service;
        }
    }
 

    
}