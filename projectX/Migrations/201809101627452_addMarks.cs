namespace projectX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMarks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Case_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cases", t => t.Case_Id)
                .Index(t => t.Case_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marks", "Case_Id", "dbo.Cases");
            DropIndex("dbo.Marks", new[] { "Case_Id" });
            DropTable("dbo.Marks");
        }
    }
}
