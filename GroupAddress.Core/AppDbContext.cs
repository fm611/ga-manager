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
        public DbSet<MainGroup> MainGroups { get; set; }
        public DbSet<SubGroup> SubGroups { get; set; }
        public DbSet<GA> GAs { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemTemplate> ItemTemplates { get; set; }


        public string DbPath { get; }

        public AppDbContext()
        {
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            DbPath = System.IO.Path.Join(folder, "data.db");
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public void InitData()
        {
            string guidBase = "DA6C8DDB-BC59-4805-84C9-E81A3DF7CB";

            var templates = new List<ItemTemplate>() {
                ItemTemplate.Light,
                ItemTemplate.LightDimm,
                ItemTemplate.LightTW,
                ItemTemplate.LightRGBW
            };

            for (int i = 0; i < templates.Count; i++)
            {
                var guid = guidBase + i.ToString("D2");
                var template = templates[i];
                template.Id = guid;

                if (ItemTemplates.Any(t => t.Id == guid)) continue;

                ItemTemplates.Add(template);

                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
