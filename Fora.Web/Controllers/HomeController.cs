using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fora.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Fora.Domain.Managers;
using Fora.Domain.Models;
using System.Security.Claims;

namespace Fora.Web.Controllers
{
    public class HomeController : Controller
    {
        private TopicManager _topic;

        public HomeController(TopicManager post)
        {
            _topic = post;
        }
        public IActionResult Index(string search, int page = 1)
        {
            var pageSize = 20;
            var posts = _topic.GetTopics(search, page, pageSize);
            return View(posts);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(TopicModel model)
        {
            if (ModelState.IsValid)
            {
                //Get Currently Logged In User Id
                model.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                var createTopic = _topic.Create(model);
                if (createTopic.Succeeded)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    ModelState.AddModelError("ServerError", createTopic.Message);
                }
            }
            return View(model);
        }

        public IActionResult Details(int id = 0)
        {
            var topic = _topic.GetTopic(id);
            if (topic == null) return RedirectToAction("index");
            return View(topic);
        }
    }
}
