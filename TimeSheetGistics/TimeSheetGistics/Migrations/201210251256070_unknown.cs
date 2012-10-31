namespace TimeSheetGistics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unknown : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        TimeSheetBy_Id = c.Int(),
                        TimeSheetApprover_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.CompanyBranchUsers", t => t.TimeSheetBy_Id)
                .ForeignKey("dbo.CompanyBranchUsers", t => t.TimeSheetApprover_Id)
                .Index(t => t.ProjectId)
                .Index(t => t.TimeSheetBy_Id)
                .Index(t => t.TimeSheetApprover_Id);
            
            CreateTable(
                "dbo.TimeCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hours = c.Int(nullable: false),
                        TimeSheetId = c.Int(nullable: false),
                        TaskDetails = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TimeSheets", t => t.TimeSheetId, cascadeDelete: true)
                .Index(t => t.TimeSheetId);
            
            CreateTable(
                "dbo.CompanyBranchUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchId = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CompanyBranchUsers", new[] { "BranchId" });
            DropIndex("dbo.TimeCards", new[] { "TimeSheetId" });
            DropIndex("dbo.TimeSheets", new[] { "TimeSheetApprover_Id" });
            DropIndex("dbo.TimeSheets", new[] { "TimeSheetBy_Id" });
            DropIndex("dbo.TimeSheets", new[] { "ProjectId" });
            DropForeignKey("dbo.CompanyBranchUsers", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.TimeCards", "TimeSheetId", "dbo.TimeSheets");
            DropForeignKey("dbo.TimeSheets", "TimeSheetApprover_Id", "dbo.CompanyBranchUsers");
            DropForeignKey("dbo.TimeSheets", "TimeSheetBy_Id", "dbo.CompanyBranchUsers");
            DropForeignKey("dbo.TimeSheets", "ProjectId", "dbo.Projects");
            DropTable("dbo.CompanyBranchUsers");
            DropTable("dbo.TimeCards");
            DropTable("dbo.TimeSheets");
        }
    }
}
