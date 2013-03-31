namespace CoffeeApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                        Address = c.String(nullable: false, maxLength: 15),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.OrderCartLines",
                c => new
                    {
                        OrderCardLineId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        CoffeeId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderCardLineId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Coffees", t => t.CoffeeId, cascadeDelete: false)
                .Index(t => t.OrderId)
                .Index(t => t.CoffeeId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.OrderCartLines", new[] { "CoffeeId" });
            DropIndex("dbo.OrderCartLines", new[] { "OrderId" });
            DropForeignKey("dbo.OrderCartLines", "CoffeeId", "dbo.Coffees");
            DropForeignKey("dbo.OrderCartLines", "OrderId", "dbo.Orders");
            DropTable("dbo.OrderCartLines");
            DropTable("dbo.Orders");
        }
    }
}
