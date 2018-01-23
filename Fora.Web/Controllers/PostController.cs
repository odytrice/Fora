using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fora.Domain.Managers;
using Fora.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fora.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private TopicManager _topic;

        public PostController(TopicManager topic)
        {
            _topic = topic;
        }

        public IActionResult Create(int id = 0)
        {
            var topic = _topic.GetTopic(id);
            if (topic == null) return RedirectToAction("details", "home", new { id });
            ViewBag.Topic = topic;
            return View();
        }

        [HttpPost]
        public IActionResult Create(PostModel model)
        {
            var topic = _topic.GetTopic(model.TopicId);
            if (topic == null) return RedirectToAction("details", "home", new { model.TopicId });
            ViewBag.Topic = topic;

            if (ModelState.IsValid)
            {
                model.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var addReply = _topic.AddReply(model);
                if (addReply.Succeeded)
                {
                    return RedirectToAction("details", "home", new { id = model.TopicId });
                }
                else
                {
                    ModelState.AddModelError("ServerError", addReply.Message);
                }
            }
            return View(model);
        }
    }
}