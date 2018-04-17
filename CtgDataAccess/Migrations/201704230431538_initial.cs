namespace CtgDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Verified = c.Boolean(nullable: false),
                        Details_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Details_Id)
                .Index(t => t.Details_Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LegionId = c.Int(nullable: false),
                        ForumHandle = c.String(),
                        Email = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LegionCostumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Prefix = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Town = c.String(),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JoinMemberRanks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Member_Id = c.Int(),
                        Rank_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Member_Id)
                .ForeignKey("dbo.MemberRanks", t => t.Rank_Id)
                .Index(t => t.Member_Id)
                .Index(t => t.Rank_Id);
            
            CreateTable(
                "dbo.MemberRanks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Verifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ApiTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Token = c.Guid(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LegionCostumeMembers",
                c => new
                    {
                        LegionCostume_Id = c.Int(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LegionCostume_Id, t.Member_Id })
                .ForeignKey("dbo.LegionCostumes", t => t.LegionCostume_Id, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .Index(t => t.LegionCostume_Id)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.EventMembers",
                c => new
                    {
                        Event_Id = c.Int(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_Id, t.Member_Id })
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .Index(t => t.Event_Id)
                .Index(t => t.Member_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApiTokens", "UserId", "dbo.Accounts");
            DropForeignKey("dbo.Verifications", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "Details_Id", "dbo.Members");
            DropForeignKey("dbo.JoinMemberRanks", "Rank_Id", "dbo.MemberRanks");
            DropForeignKey("dbo.JoinMemberRanks", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.EventMembers", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.EventMembers", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.LegionCostumeMembers", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.LegionCostumeMembers", "LegionCostume_Id", "dbo.LegionCostumes");
            DropIndex("dbo.EventMembers", new[] { "Member_Id" });
            DropIndex("dbo.EventMembers", new[] { "Event_Id" });
            DropIndex("dbo.LegionCostumeMembers", new[] { "Member_Id" });
            DropIndex("dbo.LegionCostumeMembers", new[] { "LegionCostume_Id" });
            DropIndex("dbo.ApiTokens", new[] { "UserId" });
            DropIndex("dbo.Verifications", new[] { "Id" });
            DropIndex("dbo.JoinMemberRanks", new[] { "Rank_Id" });
            DropIndex("dbo.JoinMemberRanks", new[] { "Member_Id" });
            DropIndex("dbo.Accounts", new[] { "Details_Id" });
            DropTable("dbo.EventMembers");
            DropTable("dbo.LegionCostumeMembers");
            DropTable("dbo.ApiTokens");
            DropTable("dbo.Verifications");
            DropTable("dbo.MemberRanks");
            DropTable("dbo.JoinMemberRanks");
            DropTable("dbo.Events");
            DropTable("dbo.LegionCostumes");
            DropTable("dbo.Members");
            DropTable("dbo.Accounts");
        }
    }
}
