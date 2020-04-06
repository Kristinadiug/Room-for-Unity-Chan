namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "SellerId", "dbo.Users");
            DropIndex("dbo.Books", new[] { "SellerId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Books", "SellerId");
            AddForeignKey("dbo.Books", "SellerId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
