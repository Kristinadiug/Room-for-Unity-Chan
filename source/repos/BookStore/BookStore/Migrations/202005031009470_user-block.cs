namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userblock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Blocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Blocked");
        }
    }
}
