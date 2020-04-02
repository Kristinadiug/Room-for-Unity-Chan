namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            
                
      
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Password = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
           
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "SellerId", "dbo.Profiles");
            DropForeignKey("dbo.Products", "SellerId", "dbo.Profiles");
            DropIndex("dbo.Purchases", new[] { "SellerId" });
            DropIndex("dbo.Products", new[] { "SellerId" });
            DropTable("dbo.Purchases");
            DropTable("dbo.Profiles");
            DropTable("dbo.Products");
        }
    }
}
