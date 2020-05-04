namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class puchase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Purchases", "SellerId");
            DropColumn("dbo.Purchases", "Adress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "Adress", c => c.String());
            AddColumn("dbo.Purchases", "SellerId", c => c.Int(nullable: false));
        }
    }
}
