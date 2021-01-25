namespace MyShoppingWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "UserId", "dbo.TblUsers");
            DropIndex("dbo.OrderDetails", new[] { "UserId" });
            AddColumn("dbo.Orders", "UserId", c => c.Int());
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.TblUsers", "Id");
            DropColumn("dbo.OrderDetails", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "UserId", c => c.Int());
            DropForeignKey("dbo.Orders", "UserId", "dbo.TblUsers");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropColumn("dbo.Orders", "UserId");
            CreateIndex("dbo.OrderDetails", "UserId");
            AddForeignKey("dbo.OrderDetails", "UserId", "dbo.TblUsers", "Id");
        }
    }
}
