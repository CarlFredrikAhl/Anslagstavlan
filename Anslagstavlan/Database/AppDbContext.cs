using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Anslagstavlan.Models;
using System.Threading.Tasks;

namespace Anslagstavlan.Database
{
    public class AppDbContext : DbContext 
    {
        public DbSet<ChatUserModel> Users { get; set; }
        public DbSet<ChatRoomModel> Rooms { get; set; }

        public DbSet<ChatMessageModel> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
