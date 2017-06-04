namespace HomeBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingYearSheetMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YearSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            DropTable("dbo.Sheets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Name2 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.YearSheets", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.YearSheets", new[] { "User_Id" });
            DropTable("dbo.YearSheets");
        }
    }
}
