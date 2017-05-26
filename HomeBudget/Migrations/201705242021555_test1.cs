namespace HomeBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
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
