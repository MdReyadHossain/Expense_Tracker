namespace PerkyRabbit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expenditure_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "Expenditure", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expenses", "Expenditure");
        }
    }
}
