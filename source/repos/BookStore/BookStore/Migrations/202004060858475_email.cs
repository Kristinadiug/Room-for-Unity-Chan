namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class email : DbMigration
    {
        public override void Up()
        {          
            
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Users", "PasswordConfirm", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "SellerId", "dbo.Users");
            DropIndex("dbo.Books", new[] { "SellerId" });
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Books", "Description", c => c.String());
            AlterColumn("dbo.Books", "Author", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
            DropColumn("dbo.Users", "PasswordConfirm");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Books", "SellerId");
        }
    }
}
