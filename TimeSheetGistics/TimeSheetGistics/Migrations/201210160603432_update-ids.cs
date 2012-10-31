namespace TimeSheetGistics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateids : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentCompany_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.ParentCompany_Id)
                .Index(t => t.ParentCompany_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Branches", new[] { "ParentCompany_Id" });
            DropForeignKey("dbo.Branches", "ParentCompany_Id", "dbo.Companies");
            DropTable("dbo.Branches");
        }
    }
}
