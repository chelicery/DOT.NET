namespace DOT.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zamowienia : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Zamowienie", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Zamowienie", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            AddColumn("dbo.Zamowienie", "Adres", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.AspNetUsers", "DaneUzytkownika_Adres", c => c.String());
            AddColumn("dbo.AspNetUsers", "DaneUzytkownika_KodPocztowy", c => c.String());
            DropColumn("dbo.Zamowienie", "Ulica");
            DropColumn("dbo.AspNetUsers", "DaneUzytkownika_IAdres");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DaneUzytkownika_IAdres", c => c.String());
            AddColumn("dbo.Zamowienie", "Ulica", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.AspNetUsers", "DaneUzytkownika_KodPocztowy");
            DropColumn("dbo.AspNetUsers", "DaneUzytkownika_Adres");
            DropColumn("dbo.Zamowienie", "Adres");
            RenameIndex(table: "dbo.Zamowienie", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Zamowienie", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
