namespace TimeSheetGistics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class branchfk : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Branches", name: "Location_Id", newName: "LocationId");
            RenameColumn(table: "dbo.Branches", name: "ParentCompany_Id", newName: "CompanyId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Branches", name: "CompanyId", newName: "ParentCompany_Id");
            RenameColumn(table: "dbo.Branches", name: "LocationId", newName: "Location_Id");
        }
    }
}
