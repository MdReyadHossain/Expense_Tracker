namespace PerkyRabbit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status_column_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catagories", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catagories", "Status");
        }
    }
}
