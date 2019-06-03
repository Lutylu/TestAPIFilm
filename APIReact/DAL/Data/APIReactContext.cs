using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class APIReactContext : DbContext
    {
        public APIReactContext(DbContextOptions options) : base(options) { }
                
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
