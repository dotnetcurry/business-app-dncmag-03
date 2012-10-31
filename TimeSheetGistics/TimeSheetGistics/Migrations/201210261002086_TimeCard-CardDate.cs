namespace TimeSheetGistics.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeCardCardDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeCards", "CardDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeCards", "CardDate");
        }
    }
}
