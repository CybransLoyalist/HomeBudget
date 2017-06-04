namespace HomeBudget.Migrations
{
    using System.Data.Entity.Migrations;

    [ExcludeFromCoverage]
    public partial class addingSheetModelMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sheets");
        }
    }
}
