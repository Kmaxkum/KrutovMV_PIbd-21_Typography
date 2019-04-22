namespace TypographyServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Bookings", "WorkerId", c => c.Int());
            CreateIndex("dbo.Bookings", "WorkerId");
            AddForeignKey("dbo.Bookings", "WorkerId", "dbo.Workers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "WorkerId", "dbo.Workers");
            DropIndex("dbo.Bookings", new[] { "WorkerId" });
            DropColumn("dbo.Bookings", "WorkerId");
            DropTable("dbo.Workers");
        }
    }
}
