namespace CtgDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventMembers", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.EventMembers", "Member_Id", "dbo.Members");
            DropIndex("dbo.EventMembers", new[] { "Event_Id" });
            DropIndex("dbo.EventMembers", new[] { "Member_Id" });
            CreateTable(
                "dbo.EventParticipations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        CostumeId = c.Int(),
                        Participated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LegionCostumes", t => t.CostumeId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.EventId)
                .Index(t => t.CostumeId);
            
            DropTable("dbo.EventMembers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventMembers",
                c => new
                    {
                        Event_Id = c.Int(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_Id, t.Member_Id });
            
            DropForeignKey("dbo.EventParticipations", "MemberId", "dbo.Members");
            DropForeignKey("dbo.EventParticipations", "EventId", "dbo.Events");
            DropForeignKey("dbo.EventParticipations", "CostumeId", "dbo.LegionCostumes");
            DropIndex("dbo.EventParticipations", new[] { "CostumeId" });
            DropIndex("dbo.EventParticipations", new[] { "EventId" });
            DropIndex("dbo.EventParticipations", new[] { "MemberId" });
            DropTable("dbo.EventParticipations");
            CreateIndex("dbo.EventMembers", "Member_Id");
            CreateIndex("dbo.EventMembers", "Event_Id");
            AddForeignKey("dbo.EventMembers", "Member_Id", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventMembers", "Event_Id", "dbo.Events", "Id", cascadeDelete: true);
        }
    }
}
