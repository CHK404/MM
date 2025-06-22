using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Material_Management.Models;
using Material_Management.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Material_Management.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserList> Users { get; set; }
        public DbSet<Material> Materials { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder opts)
        {
            if (!opts.IsConfigured)
            {
                opts.UseSqlServer("Server=서버주소;Database=DB이름;User Id=ID;Password=PW;");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
