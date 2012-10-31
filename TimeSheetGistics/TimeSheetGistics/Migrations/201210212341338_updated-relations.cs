namespace TimeSheetGistics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedrelations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Description", c => c.String());
            AddColumn("dbo.Projects", "BranchId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Projects", "BranchId", "dbo.Branches", "Id", cascadeDelete: true);
            CreateIndex("dbo.Projects", "BranchId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Projects", new[] { "BranchId" });
            DropForeignKey("dbo.Projects", "BranchId", "dbo.Branches");
            DropColumn("dbo.Projects", "BranchId");
            DropColumn("dbo.Companies", "Description");
        }
    }
}
