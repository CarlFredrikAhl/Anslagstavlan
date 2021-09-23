using Anslagstavlan.Database;
using Anslagstavlan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Anslagstavlan.Pages
{
    public class RoomModel : PageModel
    {
        public readonly AppDbContext database;

        public RoomModel(AppDbContext context)
        {
            database = context;
        }

        [BindProperty]
        public ChatRoomModel Room { get; set; }

        [BindProperty]
        public ChatMessageModel Message { get; set; }

        public void OnGet(int id)
        {
            Room = database.Rooms.Include(x => x.Messages).Where(room => room.ChatRoomId == id).FirstOrDefault();
        }

        public void OnPost()
        {
            Room = database.Rooms.Include(x => x.Messages).Where(room => room.ChatRoomId == Room.ChatRoomId).FirstOrDefault();

            Message.Date = DateTime.Now;
            Message.ChatRoom = Room;
            Message.ChatUser = database.Users.Where(x => x.ChatUserId == Room.ChatRoomOwner).FirstOrDefault();

            database.Messages.Add(Message);
            database.SaveChanges();
        }
    }
}
