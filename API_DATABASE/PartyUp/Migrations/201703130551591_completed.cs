namespace PartyUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class completed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Address_1 = c.String(),
                        Address_2 = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        PostalCode = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                    })
                .PrimaryKey(t => t.LocationId);
            
            AddColumn("dbo.Events", "EventLocation_LocationId", c => c.Int());
            CreateIndex("dbo.Events", "EventLocation_LocationId");
            AddForeignKey("dbo.Events", "EventLocation_LocationId", "dbo.Locations", "LocationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "EventLocation_LocationId", "dbo.Locations");
            DropIndex("dbo.Events", new[] { "EventLocation_LocationId" });
            DropColumn("dbo.Events", "EventLocation_LocationId");
            DropTable("dbo.Locations");
        }
    }
}
