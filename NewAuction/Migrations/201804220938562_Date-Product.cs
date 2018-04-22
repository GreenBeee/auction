namespace NewAuction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "StartAuction", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "StartAuction");
        }
    }
}
