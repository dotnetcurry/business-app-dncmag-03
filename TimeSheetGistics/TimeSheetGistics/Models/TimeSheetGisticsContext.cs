using System.Data.Entity;
using TimeSheetGistics.Data;

namespace TimeSheetGistics.Models
{
    public class TimeSheetGisticsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<TimeSheetGistics.Models.TimeSheetGisticsContext>());

        public TimeSheetGisticsContext() : base("name=TimeSheetGisticsContext")
        {
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<TimeSheet> TimeSheets { get; set; }

        public DbSet<TimeCard> TimeCards { get; set; }

        public DbSet<CompanyBranchUser> CompanyBranchUsers { get; set; }
    }
}
