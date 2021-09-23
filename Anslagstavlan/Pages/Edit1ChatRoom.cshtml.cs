using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anslagstavlan.Database;
using Anslagstavlan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Anslagstavlan.Pages
{
    public class Edit1ChatRoomModel : PageModel
    {
        private readonly AppDbContext database;

        public ChatUserModel CurrentUser { get; set; }
        public List<ChatRoomModel> Rooms { get; set; }

        public Edit1ChatRoomModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet(int id)
        {
            CurrentUser = database.Users.Where(user => user.ChatUserId == id).FirstOrDefault();

            if (CurrentUser != null)
            {
                Rooms = database.Rooms.Where(room => room.ChatRoomOwner == CurrentUser.ChatUserId).ToList();
            }
        }

        public IActionResult OnPost(int id, int userId)
        {
            return RedirectToPage("Edit2ChatRoom", new { id = id, userId = userId});
        }
    }
}
