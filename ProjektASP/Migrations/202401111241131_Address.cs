namespace ProjektASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZipCode = c.String(maxLength: 6),
                        City = c.String(),
                        StreetAndBuildingNumber = c.String(),
                        ApartmentNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Surrname", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "AddressId");
            AddForeignKey("dbo.AspNetUsers", "AddressId", "dbo.AddressModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "AddressId", "dbo.AddressModels");
            DropIndex("dbo.AspNetUsers", new[] { "AddressId" });
            DropColumn("dbo.AspNetUsers", "AddressId");
            DropColumn("dbo.AspNetUsers", "Surrname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.AddressModels");
        }
    }
}
