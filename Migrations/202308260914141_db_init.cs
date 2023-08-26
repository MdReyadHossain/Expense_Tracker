namespace PerkyRabbit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db_init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catagories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Catagory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Catagories", t => t.Catagory_Id, cascadeDelete: true)
                .Index(t => t.Catagory_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "Catagory_Id", "dbo.Catagories");
            DropIndex("dbo.Expenses", new[] { "Catagory_Id" });
            DropTable("dbo.Expenses");
            DropTable("dbo.Catagories");
        }
    }
}
