﻿using Anslagstavlan.Database;
using Anslagstavlan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anslagstavlan.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;

        public ChatUserModel CurrentUser { get; set; }
        public List<ChatUserModel> CreatedRoomUsers { get; set; }
        public List<ChatRoomModel> Rooms { get; set; }

        public IndexModel(AppDbContext context)
        {
            database = context;
        }

        public void OnGet(int id)
        {
            CurrentUser = database.Users.Where(user => user.ChatUserId == id).FirstOrDefault();
            CreatedRoomUsers = new List<ChatUserModel>();

            if (CurrentUser != null)
            {
                //Change this so that all users can see all rooms created by all users
                Rooms = database.Rooms.ToList();

                foreach (var room in Rooms)
                {
                    ChatUserModel user = database.Users.Where(user => user.ChatUserId == room.ChatRoomOwner)
                        .FirstOrDefault();
                    CreatedRoomUsers.Add(user);
                }
            }
        }
    }
}
