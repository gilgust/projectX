namespace projectX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_dbset_Img : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Imgs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        src = c.String(),
                        CaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cases", t => t.CaseId, cascadeDelete: true)
                .Index(t => t.CaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Imgs", "CaseId", "dbo.Cases");
            DropIndex("dbo.Imgs", new[] { "CaseId" });
            DropTable("dbo.Imgs");
        }
    }
}
