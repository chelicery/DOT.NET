namespace DOT.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DaneUzytkownika_Nip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DaneUzytkownika_Nip");
        }
    }
}
