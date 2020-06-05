using DOT.NET.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DOT.NET.DAL
{
    public class PrzedmiotyContext : IdentityDbContext<ApplicationUser>
    {
        public PrzedmiotyContext() : base("AllegroDatabase")
        {

        }
        static PrzedmiotyContext()
        {
            Database.SetInitializer<PrzedmiotyContext>(new PrzedmiotyInitializer());
        }
        public DbSet<Przedmiot> Przedmioty { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<PozycjaZamowienia> PozycjeZamowienia  { get; set; }



        public static PrzedmiotyContext Create()
        {
            return new PrzedmiotyContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Properties<DateTime>()
            .Configure(c => c.HasColumnType("datetime2"));
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}