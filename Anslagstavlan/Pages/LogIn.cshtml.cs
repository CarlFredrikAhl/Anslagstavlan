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
    public class LogInModel : PageModel
    {
        private readonly AppDbContext database;

        [Required]
        public ChatUserModel User { get; set; }

        public string Message { get; set; }

        public LogInModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet(string message)
        {
            Message = message;
        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                //Check if user is signed up 
                ChatUserModel signedUpUser = database.Users.Where(user => user.Username == User.Username).FirstOrDefault();

                if(signedUpUser != null)
                {
                    return RedirectToPage("/Index", new { id = signedUpUser.ChatUserId});
                }
            } 
            
            return RedirectToPage("/LogIn", new { message = "User not signed up, try again"});
        }
    }
}
