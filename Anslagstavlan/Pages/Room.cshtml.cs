using Anslagstavlan.Database;
using Anslagstavlan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        [BindProperty]
        public List<ChatUserModel> CreatedMessageUsers { get; set; }
        
        [BindProperty]
        public ChatUserModel CreatedRoomUser { get; set; }

        public void OnGet(int id)
        {
            Room = database.Rooms.Include(x => x.Messages).Where(room => room.ChatRoomId == id).FirstOrDefault();
            
            CreatedMessageUsers = new List<ChatUserModel>();
            CreatedRoomUser = new ChatUserModel();
            CreatedRoomUser = database.Users.Where(user => user.ChatUserId == Room.ChatRoomOwner)
                .FirstOrDefault();

            foreach (var message in Room.Messages)
            {
                ChatUserModel user = database.Users.Where(user => user.ChatUserId == message.ChatUserId)
                    .FirstOrDefault();
                CreatedMessageUsers.Add(user);
            }
        }

        public IActionResult OnPost(int userId)
        {
            Room = database.Rooms.Include(x => x.Messages).Where(room => room.ChatRoomId == Room.ChatRoomId).FirstOrDefault();

            ChatUserModel user = database.Users.Where(user => user.ChatUserId == userId).FirstOrDefault();

            Message.Date = DateTime.Now;
            Message.ChatRoom = Room;
            Message.ChatUser = user;

            database.Messages.Add(Message);
            database.SaveChanges();

            return RedirectToPage("Room", new { id = Room.ChatRoomId, userId = user.ChatUserId });
        }
    }
}
