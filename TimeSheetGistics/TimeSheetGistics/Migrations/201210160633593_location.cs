namespace TimeSheetGistics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Branches", "ParentCompany_Id", "dbo.Companies");
            DropIndex("dbo.Branches", new[] { "ParentCompany_Id" });
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Zip = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Branches", "Description", c => c.String());
            AddColumn("dbo.Branches", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Branches", "Location_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Branches", "ParentCompany_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.Branches", "Location_Id", "dbo.Locations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Branches", "ParentCompany_Id", "dbo.Companies", "Id", cascadeDelete: true);
            CreateIndex("dbo.Branches", "Location_Id");
            CreateIndex("dbo.Branches", "ParentCompany_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Branches", new[] { "ParentCompany_Id" });
            DropIndex("dbo.Branches", new[] { "Location_Id" });
            DropForeignKey("dbo.Branches", "ParentCompany_Id", "dbo.Companies");
            DropForeignKey("dbo.Branches", "Location_Id", "dbo.Locations");
            AlterColumn("dbo.Branches", "ParentCompany_Id", c => c.Int());
            DropColumn("dbo.Branches", "Location_Id");
            DropColumn("dbo.Branches", "Name");
            DropColumn("dbo.Branches", "Description");
            DropTable("dbo.Locations");
            CreateIndex("dbo.Branches", "ParentCompany_Id");
            AddForeignKey("dbo.Branches", "ParentCompany_Id", "dbo.Companies", "Id");
        }
    }
}
