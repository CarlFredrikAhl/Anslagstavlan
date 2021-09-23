using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Anslagstavlan.Models;
using Anslagstavlan.Database;
using System.ComponentModel.DataAnnotations;

namespace Anslagstavlan.Pages
{
    public class CreateChatRoomModel : PageModel
    {
        private readonly AppDbContext database;

        private ChatUserModel User { get; set; }
        
        [BindProperty]
        [Required]
        public ChatRoomModel ChatRoom { get; set; }

        public CreateChatRoomModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(int id)
        {
            User = database.Users.Where(user => user.ChatUserId == id).FirstOrDefault();

            ChatRoom.ChatRoomOwner = User.ChatUserId;
            ChatRoom.Messages = null;

            //Post ChatRoom to database
            database.Rooms.Add(ChatRoom);
            database.SaveChanges();
            
            return RedirectToPage("/Index", new { id = User.ChatUserId});
        }
    }
}
