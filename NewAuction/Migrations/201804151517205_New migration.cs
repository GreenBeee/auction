namespace NewAuction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        TimeStamp = c.Long(nullable: false),
                        Product_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Product_ID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bets", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bets", "Product_ID", "dbo.Products");
            DropIndex("dbo.Bets", new[] { "User_Id" });
            DropIndex("dbo.Bets", new[] { "Product_ID" });
            DropTable("dbo.Bets");
        }
    }
}
