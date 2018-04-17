namespace CtgDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "JoinDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "JoinDate", c => c.DateTime(nullable: false));
        }
    }
}
