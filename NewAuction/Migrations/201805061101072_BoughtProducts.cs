namespace NewAuction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoughtProducts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Buyer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Products", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Products", new[] { "Buyer_Id" });
            AddColumn("dbo.Products", "Buyer", c => c.String());
            DropColumn("dbo.Products", "Buyer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Buyer_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Products", "Buyer");
            CreateIndex("dbo.Products", "Buyer_Id");
            CreateIndex("dbo.Products", "ApplicationUser_Id");
            AddForeignKey("dbo.Products", "Buyer_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
