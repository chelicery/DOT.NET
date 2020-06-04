namespace DOT.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodaniePolaOpisSkrocony : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Przedmiot", "OpisSkrocony", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Przedmiot", "OpisSkrocony");
        }
    }
}
