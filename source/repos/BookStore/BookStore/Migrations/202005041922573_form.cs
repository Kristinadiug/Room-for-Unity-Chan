namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class form : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Comment", c => c.String(nullable: false));
        }
    }
}
