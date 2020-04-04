namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class byte_images : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageBytes", c => c.Binary());
            DropColumn("dbo.Products", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Image", c => c.Binary());
            DropColumn("dbo.Products", "ImageBytes");
        }
    }
}
