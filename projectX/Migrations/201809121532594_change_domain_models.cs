namespace projectX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_domain_models : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Marks", "Case_Id", "dbo.Cases");
            DropIndex("dbo.Marks", new[] { "Case_Id" });
            CreateTable(
                "dbo.MarkCases",
                c => new
                    {
                        Mark_Id = c.Int(nullable: false),
                        Case_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Mark_Id, t.Case_Id })
                .ForeignKey("dbo.Marks", t => t.Mark_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cases", t => t.Case_Id, cascadeDelete: true)
                .Index(t => t.Mark_Id)
                .Index(t => t.Case_Id);
            
            AddColumn("dbo.Proects", "Mark_Id", c => c.Int());
            CreateIndex("dbo.Proects", "Mark_Id");
            AddForeignKey("dbo.Proects", "Mark_Id", "dbo.Marks", "Id");
            DropColumn("dbo.Marks", "Case_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Marks", "Case_Id", c => c.Int());
            DropForeignKey("dbo.Proects", "Mark_Id", "dbo.Marks");
            DropForeignKey("dbo.MarkCases", "Case_Id", "dbo.Cases");
            DropForeignKey("dbo.MarkCases", "Mark_Id", "dbo.Marks");
            DropIndex("dbo.MarkCases", new[] { "Case_Id" });
            DropIndex("dbo.MarkCases", new[] { "Mark_Id" });
            DropIndex("dbo.Proects", new[] { "Mark_Id" });
            DropColumn("dbo.Proects", "Mark_Id");
            DropTable("dbo.MarkCases");
            CreateIndex("dbo.Marks", "Case_Id");
            AddForeignKey("dbo.Marks", "Case_Id", "dbo.Cases", "Id");
        }
    }
}
