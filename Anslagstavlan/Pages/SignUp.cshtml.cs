using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Anslagstavlan.Models;
using Anslagstavlan.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Anslagstavlan.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly IWebHostEnvironment webHostEnvironment;

        [Required]
        [BindProperty]
        public ChatUserModel User { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public SignUpModel(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            database = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Image != null)
            {
                string folder = Path.Combine(webHostEnvironment.WebRootPath, "imgs");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string ext = Path.GetExtension(Image.FileName);

                string uniqueFileName = String.Concat(Guid.NewGuid().ToString(), "-", User.Username + ext);

                string uploadFolder = Path.Combine(folder, uniqueFileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }

                User.Img = uniqueFileName;
            }

            if (ModelState.IsValid)
            {
                //Write user to database
                database.Users.Add(User);
                database.SaveChanges();
                database.Dispose();
            
            } else
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("/Index", new { id = User.ChatUserId});
        }
    }
}
