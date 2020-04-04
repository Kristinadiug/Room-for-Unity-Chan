namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userdrop : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "Sellers");
            CreateTable(
                "dbo.Buyers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Sellers", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sellers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.Buyers");
            RenameTable(name: "dbo.Sellers", newName: "Users");
        }
    }
}
