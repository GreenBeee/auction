namespace NewAuction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isBanedUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsBanned", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "IsSeller");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsSeller", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "IsBanned");
        }
    }
}
