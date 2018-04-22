namespace NewAuction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyerForProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "User_Id", "dbo.AspNetUsers");
            AddColumn("dbo.Products", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Buyer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "ApplicationUser_Id");
            CreateIndex("dbo.Products", "Buyer_Id");
            AddForeignKey("dbo.Products", "Buyer_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Products", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "Buyer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Products", new[] { "Buyer_Id" });
            DropIndex("dbo.Products", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Products", "Buyer_Id");
            DropColumn("dbo.Products", "ApplicationUser_Id");
            AddForeignKey("dbo.Products", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
