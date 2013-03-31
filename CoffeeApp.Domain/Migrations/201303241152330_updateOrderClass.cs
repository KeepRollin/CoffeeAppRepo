namespace CoffeeApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrderClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderCartLines", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderCartLines", "CoffeeId", "dbo.Coffees");
            DropIndex("dbo.OrderCartLines", new[] { "OrderId" });
            DropIndex("dbo.OrderCartLines", new[] { "CoffeeId" });
            DropTable("dbo.OrderCartLines");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderCartLines",
                c => new
                    {
                        OrderCardLineId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        CoffeeId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderCardLineId);
            
            CreateIndex("dbo.OrderCartLines", "CoffeeId");
            CreateIndex("dbo.OrderCartLines", "OrderId");
            AddForeignKey("dbo.OrderCartLines", "CoffeeId", "dbo.Coffees", "CoffeeID", cascadeDelete: true);
            AddForeignKey("dbo.OrderCartLines", "OrderId", "dbo.Orders", "OrderID", cascadeDelete: true);
        }
    }
}
