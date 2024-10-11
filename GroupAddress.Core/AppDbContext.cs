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
        public DbSet<ItemPart> ItemParts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemTemplate> ItemTemplates { get; set; }
        public DbSet<ItemPartTemplate> ItemPartTemplates { get; set; }


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
            InitItemTemplates();

            InitMainGroups();

            InitItem();

        }

        public void InitMainGroups()
        {
            string guidBase = "DA6C8DDB-BC59-4805-84C9-E81A3DF7CB";

            var templates = new List<MainGroup>() {
                new MainGroup(1, "Licht allgemein"),
                new MainGroup(2, "Licht dimmbar"),
                new MainGroup(3, "Licht TW"),
                new MainGroup(4, "Licht RGBW #1")
            };

            for (int i = 0; i < templates.Count; i++)
            {
                var guid = guidBase + i.ToString("D2");
                if (MainGroups.Any(t => t.Id == guid)) continue;

                var template = templates[i];
                template.Id = guid;
                MainGroups.Add(template);
            }
            SaveChanges();

        }

        public void InitItemTemplates()
        {
            string guidBase = "DA6C8DDB-BC59-4805-84C9-E81A3DF7CB";

            var templates = new List<ItemTemplate>() {
                DefaultItemTemplates.Light,
                DefaultItemTemplates.LightDimm,
                DefaultItemTemplates.LightTW,
                DefaultItemTemplates.LightRGBW
            };

            for (int i = 0; i < templates.Count; i++)
            {
                var guid = guidBase + i.ToString("D2");
                if (ItemTemplates.Any(t => t.Id == guid)) continue;
                
                var template = templates[i];
                template.Id = guid;
                ItemTemplates.Add(template);

            }
            SaveChanges();
        }


        public void InitItem()
        {
            if(ItemParts.Any()) return;

            var mGroup1 = MainGroups.FirstOrDefault();
            if(mGroup1 ==  null) return;

            var newItem = DefaultItemTemplates.Light.CreateItem([mGroup1], "EG_HWR_Licht_Decke");

            SaveChanges();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
