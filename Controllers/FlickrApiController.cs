using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlickrNet;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonashExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlickrApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private const int FLICKR_PER_PAGE = 20;

        public FlickrApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // To call recent photos: api/flickrapi
        // To search: /api/flickrapi?tagSearch=cafe
        // GET: api/<FlickrApiController>
        [HttpGet]
        public string Get(string tagSearch = "")
        {
            Flickr flickr = new Flickr(_configuration.GetValue<string>("FlickrApiKey"));
            var options = new PhotoSearchOptions
            { Tags = tagSearch, PerPage = FLICKR_PER_PAGE, Page = 1 };

            PhotoCollection photos;
            if (tagSearch.Length < 1)
            {
                photos = flickr.PhotosGetRecent();
            }
            else {
                photos = flickr.PhotosSearch(options);
            }
            var customerJson = JsonConvert.SerializeObject(photos);
            return customerJson;
        }
    }
}
