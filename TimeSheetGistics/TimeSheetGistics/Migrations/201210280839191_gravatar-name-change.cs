namespace TimeSheetGistics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gravatarnamechange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyBranchUsers", "GravatarUrl", c => c.String());
            DropColumn("dbo.CompanyBranchUsers", "GravatarUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CompanyBranchUsers", "GravatarUrl", c => c.String());
            DropColumn("dbo.CompanyBranchUsers", "GravatarUrl");
        }
    }
}
