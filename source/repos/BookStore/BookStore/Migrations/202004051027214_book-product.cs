namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookproduct : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Products", newName: "Books");
            DropColumn("dbo.Books", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            RenameTable(name: "dbo.Books", newName: "Products");
        }
    }
}
