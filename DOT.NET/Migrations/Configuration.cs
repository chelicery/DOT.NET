﻿namespace DOT.NET.Migrations
{
    using DOT.NET.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<DOT.NET.DAL.PrzedmiotyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DOT.NET.DAL.PrzedmiotyContext";
        }

        protected override void Seed(DOT.NET.DAL.PrzedmiotyContext context)
        {
            PrzedmiotyInitializer.SeedPrzedmiotyData(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
