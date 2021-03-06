﻿namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkp : DbMigration
    {
        public override void Up()
        {
          

        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "SellerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "SellerId");
            AddForeignKey("dbo.Books", "SellerId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
