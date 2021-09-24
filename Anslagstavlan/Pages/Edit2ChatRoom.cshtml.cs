using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Anslagstavlan.Models;
using Anslagstavlan.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Anslagstavlan.Pages
{
    public class Edit2ChatRoomModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public ChatRoomModel Room { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public Edit2ChatRoomModel(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            database = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnGet(int id, int userId)
        {
            Room = database.Rooms.Where(room => room.ChatRoomId == id).FirstOrDefault();
            Room.ChatRoomOwner = userId;
        }

        public IActionResult OnPostEdit()
        {
            if (Image != null)
            {
                string folder = Path.Combine(webHostEnvironment.WebRootPath, "imgs");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string ext = Path.GetExtension(Image.FileName);

                string uniqueFileName = String.Concat(Guid.NewGuid().ToString(), "-", Room.ChatRoomName + ext);

                string uploadFolder = Path.Combine(folder, uniqueFileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }

                Room.Img = uniqueFileName;
            } 

            //Edit the room
            database.Update(Room);
            database.SaveChanges();

            int userId = Room.ChatRoomOwner;

            return RedirectToPage("Index", new { id = userId});
        }
    }
}
