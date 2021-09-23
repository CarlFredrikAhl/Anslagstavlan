using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Anslagstavlan.Models;
using Anslagstavlan.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Anslagstavlan.Pages
{
    [BindProperties]
    public class SignUpModel : PageModel
    {
        private readonly AppDbContext database;

        [Required]
        public ChatUserModel User { get; set; }

        public SignUpModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

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
