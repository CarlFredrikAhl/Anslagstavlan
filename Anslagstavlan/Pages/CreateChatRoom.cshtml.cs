using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Anslagstavlan.Models;
using Anslagstavlan.Database;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Anslagstavlan.Pages
{
    public class CreateChatRoomModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly IWebHostEnvironment webHostEnvironment;

        private ChatUserModel User { get; set; }
        
        [BindProperty]
        [Required]
        public ChatRoomModel ChatRoom { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public CreateChatRoomModel(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            database = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(int id)
        {
            User = database.Users.Where(user => user.ChatUserId == id).FirstOrDefault();
            
            if(Image != null)
            {
                string folder = Path.Combine(webHostEnvironment.WebRootPath, "imgs");

                if(!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string ext = Path.GetExtension(Image.FileName);
                
                string uniqueFileName = String.Concat(Guid.NewGuid().ToString(), "-", ChatRoom.ChatRoomName + ext);

                string uploadFolder = Path.Combine(folder, uniqueFileName);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }

                ChatRoom.Img = uniqueFileName;
            }

            ChatRoom.ChatRoomOwner = User.ChatUserId;
            ChatRoom.Messages = null;

            //Post ChatRoom to database
            database.Rooms.Add(ChatRoom);
            database.SaveChanges();
            
            return RedirectToPage("/Index", new { id = User.ChatUserId});
        }
    }
}
