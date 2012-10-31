namespace TimeSheetGistics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companybranchuseraddproperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyBranchUsers", "Email", c => c.String());
            AddColumn("dbo.CompanyBranchUsers", "GravatarUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyBranchUsers", "GravatarUrl");
            DropColumn("dbo.CompanyBranchUsers", "Email");
        }
    }
}
