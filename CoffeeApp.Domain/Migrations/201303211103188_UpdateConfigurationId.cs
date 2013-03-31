namespace CoffeeApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConfigurationId : DbMigration
    {
        public override void Up()
        {

        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Configurations", new[] { "ConfigurationId" });
            DropColumn("dbo.Configurations", "ConfigurationId");
            AddColumn("dbo.Configurations", "ConfigurationId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Configurations", "ConfigurationId");
        }
    }
}
