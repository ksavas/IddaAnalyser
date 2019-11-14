namespace IddaAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissingXXX : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FullOdds",
                c => new
                    {
                        FullOddId = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.FullOddId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FullOdds");

        }
    }
}
