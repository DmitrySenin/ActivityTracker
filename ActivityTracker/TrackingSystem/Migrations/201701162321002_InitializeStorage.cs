namespace TrackingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeStorage : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EndDate = c.DateTime(nullable: false),
                        ProcessName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        WindowTitle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            this.DropTable("dbo.Activities");
        }
    }
}
