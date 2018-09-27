namespace projectX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Proects", "Mark_Id", "dbo.Marks");
            DropIndex("dbo.Proects", new[] { "Mark_Id" });
            CreateTable(
                "dbo.ProectMarks",
                c => new
                    {
                        Proect_Id = c.Int(nullable: false),
                        Mark_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Proect_Id, t.Mark_Id })
                .ForeignKey("dbo.Proects", t => t.Proect_Id, cascadeDelete: true)
                .ForeignKey("dbo.Marks", t => t.Mark_Id, cascadeDelete: true)
                .Index(t => t.Proect_Id)
                .Index(t => t.Mark_Id);
            
            DropColumn("dbo.Proects", "Mark_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Proects", "Mark_Id", c => c.Int());
            DropForeignKey("dbo.ProectMarks", "Mark_Id", "dbo.Marks");
            DropForeignKey("dbo.ProectMarks", "Proect_Id", "dbo.Proects");
            DropIndex("dbo.ProectMarks", new[] { "Mark_Id" });
            DropIndex("dbo.ProectMarks", new[] { "Proect_Id" });
            DropTable("dbo.ProectMarks");
            CreateIndex("dbo.Proects", "Mark_Id");
            AddForeignKey("dbo.Proects", "Mark_Id", "dbo.Marks", "Id");
        }
    }
}
