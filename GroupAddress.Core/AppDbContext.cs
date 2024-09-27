using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public class AppDbContext : DbContext
    {
        //public DbSet<Blog> Blogs { get; set; }
        //public DbSet<Post> Posts { get; set; }

        public string DbPath { get; }

        public AppDbContext()
        {
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(folder, "data.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
