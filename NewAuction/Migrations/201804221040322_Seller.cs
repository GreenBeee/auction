namespace NewAuction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seller : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsSeller", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsSeller");
        }
    }
}
