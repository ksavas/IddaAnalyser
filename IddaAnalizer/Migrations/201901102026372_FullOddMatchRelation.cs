namespace IddaAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullOddMatchRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "FullOddId", c => c.Int());
            CreateIndex("dbo.Matches", "FullOddId");
            AddForeignKey("dbo.Matches", "FullOddId", "dbo.FullOdds", "FullOddId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "FullOddId", "dbo.FullOdds");
            DropIndex("dbo.Matches", new[] { "FullOddId" });
            DropColumn("dbo.Matches", "FullOddId");
        }
    }
}
