namespace ProjektASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration13 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
        }
    }
}
