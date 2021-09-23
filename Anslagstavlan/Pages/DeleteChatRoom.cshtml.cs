using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Anslagstavlan.Models;
using Anslagstavlan.Database;

namespace Anslagstavlan.Pages
{
    public class DeleteChatRoomModel : PageModel
    {
        private readonly AppDbContext database;

        public ChatUserModel CurrentUser { get; set; }
        public List<ChatRoomModel> Rooms { get; set; }

        public DeleteChatRoomModel(AppDbContext context)
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

        public IActionResult OnPost(int id)
        {
            //Delete room from database
            ChatRoomModel room = database.Rooms.Where(room => room.ChatRoomId == id).FirstOrDefault();

            database.Remove(room);
            database.SaveChanges();

            return RedirectToPage("/Index", new { id = room.ChatRoomOwner});
        }
    }
}
