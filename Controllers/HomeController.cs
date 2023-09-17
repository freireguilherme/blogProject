﻿using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Blog.Web.Repositories;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}