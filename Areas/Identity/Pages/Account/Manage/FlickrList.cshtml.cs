using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FlickrNet;
using X.PagedList;
using Microsoft.Extensions.Configuration;

namespace MonashExercise.Areas.Identity.Pages.Account.Manage
{
    public partial class FlickrModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        private const int FLICKR_PER_PAGE = 20;

        public FlickrModel(IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public PhotoCollection Photos { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Photos")]
            public PhotoCollection Photos { get; set; }
        }

        private async Task LoadAsync(PhotoCollection photos)
        {
            Input = new InputModel
            {
                Photos = photos
            };
        }

        public async Task<IActionResult> OnGetAsync(string searchString = "mountain")
        {            
            Flickr flickr = new Flickr(_configuration.GetValue<string>("FlickrApiKey"));
            var options = new PhotoSearchOptions
            { Tags = searchString, PerPage = FLICKR_PER_PAGE, Page = 1 };

            PhotoCollection photos = flickr.PhotosSearch(options);

            await LoadAsync(photos);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            /*
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";*/
            return RedirectToPage();
        }
    }
}
