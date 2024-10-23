using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace GroupAddress.Core
{
    public class AppDbContext : DbContext
    {
        public DbSet<MainGroup> MainGroups { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GA>(x => {
                x.ComplexProperty(y => y.Addresse, y => { y.IsRequired(); });
            });
        }

        public void InitData()
        {
            InitItemTemplates();

            InitMainGroups();

            InitItem();

        }

        public void InitMainGroups()
        {
            string guidBase = "DA6C8DDB-BC59-4805-84C9-E81A3DF7CB";

            var subGroupNames = new string[] {
                "Zentral",
                "Zentral Status",
                "Schalten",
                "Schalten Status",
                "SET Wert",
                "GET Wert",
                "SET Misc",
                "GET Misc"
                };

            var templates = new List<MainGroup>() {
                new MainGroup(1, "Licht allgemein", subGroupNames,10),
                new MainGroup(2, "Licht dimmbar", subGroupNames,10),
                new MainGroup(3, "Licht TW", subGroupNames,10),
                new MainGroup(4, "Licht RGBW #1", subGroupNames,50)
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

            if(Items.Any()) return;

            var mGroup1 = MainGroups.FirstOrDefault(x => x.SubAddress==1);
            if(mGroup1 ==  null) return;

            mGroup1.AddItem(DefaultItemTemplates.Light, "EG_HWR_Licht_Decke");

            SaveChanges();

        }


    }
}
