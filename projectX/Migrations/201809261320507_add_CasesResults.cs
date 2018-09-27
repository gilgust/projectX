namespace projectX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_CasesResults : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaseResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Coment = c.String(),
                        Condition = c.String(),
                        Case_Id = c.Int(),
                        Proect_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cases", t => t.Case_Id)
                .ForeignKey("dbo.Proects", t => t.Proect_Id)
                .Index(t => t.Case_Id)
                .Index(t => t.Proect_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CaseResults", "Proect_Id", "dbo.Proects");
            DropForeignKey("dbo.CaseResults", "Case_Id", "dbo.Cases");
            DropIndex("dbo.CaseResults", new[] { "Proect_Id" });
            DropIndex("dbo.CaseResults", new[] { "Case_Id" });
            DropTable("dbo.CaseResults");
        }
    }
}
