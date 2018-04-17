namespace CtgDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventUpgrade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Format = c.String(),
                        Url = c.String(),
                        ImageType = c.Int(nullable: false),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Members", "FeatureImage_Id", c => c.Int());
            AddColumn("dbo.Events", "State", c => c.String());
            AddColumn("dbo.Events", "ZipCode", c => c.String());
            AddColumn("dbo.Events", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "Website", c => c.String());
            AddColumn("dbo.Events", "Display", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "WeaponsAllowed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "LFLApproval", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "EventImage_Id", c => c.Int());
            AddColumn("dbo.Events", "Requestor_Id", c => c.Int());
            CreateIndex("dbo.Members", "FeatureImage_Id");
            CreateIndex("dbo.Events", "EventImage_Id");
            CreateIndex("dbo.Events", "Requestor_Id");
            AddForeignKey("dbo.Events", "EventImage_Id", "dbo.Images", "Id");
            AddForeignKey("dbo.Events", "Requestor_Id", "dbo.Contacts", "Id");
            AddForeignKey("dbo.Members", "FeatureImage_Id", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "FeatureImage_Id", "dbo.Images");
            DropForeignKey("dbo.Events", "Requestor_Id", "dbo.Contacts");
            DropForeignKey("dbo.Images", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "EventImage_Id", "dbo.Images");
            DropIndex("dbo.Images", new[] { "Event_Id" });
            DropIndex("dbo.Events", new[] { "Requestor_Id" });
            DropIndex("dbo.Events", new[] { "EventImage_Id" });
            DropIndex("dbo.Members", new[] { "FeatureImage_Id" });
            DropColumn("dbo.Events", "Requestor_Id");
            DropColumn("dbo.Events", "EventImage_Id");
            DropColumn("dbo.Events", "LFLApproval");
            DropColumn("dbo.Events", "Status");
            DropColumn("dbo.Events", "WeaponsAllowed");
            DropColumn("dbo.Events", "Display");
            DropColumn("dbo.Events", "Website");
            DropColumn("dbo.Events", "EndDate");
            DropColumn("dbo.Events", "ZipCode");
            DropColumn("dbo.Events", "State");
            DropColumn("dbo.Members", "FeatureImage_Id");
            DropTable("dbo.Contacts");
            DropTable("dbo.Images");
        }
    }
}
