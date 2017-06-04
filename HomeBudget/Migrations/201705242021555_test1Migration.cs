namespace HomeBudget.Migrations
{
    using System.Data.Entity.Migrations;

    [ExcludeFromCoverage]
    public partial class test1Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sheets", "Name2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sheets", "Name2");
        }
    }
}
