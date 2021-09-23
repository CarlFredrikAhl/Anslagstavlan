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
    public class Edit2ChatRoomModel : PageModel
    {
        private readonly AppDbContext database;

        [BindProperty]
        public ChatRoomModel Room { get; set; }

        public Edit2ChatRoomModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet(int id, int userId)
        {
            Room = database.Rooms.Where(room => room.ChatRoomId == id).FirstOrDefault();
            Room.ChatRoomOwner = userId;
        }

        public IActionResult OnPostEdit()
        {
            //Edit the room
            database.Update(Room);
            database.SaveChanges();

            int userId = Room.ChatRoomOwner;

            return RedirectToPage("Index", new { id = userId});
        }
    }
}
