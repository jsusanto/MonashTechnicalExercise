using FlickrNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MonashExercise.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MonashExercise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult FlickrList()
        {
            const string apiKey = "a9e27bff3b1f58e1274e643bb8f1834b";

            Flickr flickr = new Flickr(apiKey);
            string searchTerm = "mountain";
            var options = new PhotoSearchOptions
            { Tags = searchTerm, PerPage = 20, Page = 1 };
            PhotoCollection photos = flickr.PhotosSearch(options);

            return View(photos);
        }
    }
}
