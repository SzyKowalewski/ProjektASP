﻿namespace ProjektASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Categories", name: "CategoryId", newName: "Id");
        }
        
        public override void Down()
        {}
    }
}
