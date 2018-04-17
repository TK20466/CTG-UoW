namespace CtgDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberNameChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "FirstName", c => c.String());
            AddColumn("dbo.Members", "LastName", c => c.String());
            DropColumn("dbo.Members", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Name", c => c.String());
            DropColumn("dbo.Members", "LastName");
            DropColumn("dbo.Members", "FirstName");
        }
    }
}
