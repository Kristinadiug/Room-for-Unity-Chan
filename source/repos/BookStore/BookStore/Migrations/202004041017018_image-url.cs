namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
            DropColumn("dbo.Products", "ImageBytes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageBytes", c => c.Binary());
            DropColumn("dbo.Products", "ImageUrl");
        }
    }
}
