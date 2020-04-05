namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Buyers", newName: "Users");
            DropForeignKey("dbo.Purchases", "SellerId", "dbo.Sellers");
            AddColumn("dbo.Users", "Role", c => c.String());
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Users", "Role");
            CreateIndex("dbo.Purchases", "SellerId");
            AddForeignKey("dbo.Purchases", "SellerId", "dbo.Sellers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Users", newName: "Buyers");
        }
    }
}
