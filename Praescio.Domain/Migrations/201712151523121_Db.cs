namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignment", "MediumId", c => c.Int());
            CreateIndex("dbo.Assignment", "MediumId");
            AddForeignKey("dbo.Assignment", "MediumId", "dbo.Medium", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignment", "MediumId", "dbo.Medium");
            DropIndex("dbo.Assignment", new[] { "MediumId" });
            DropColumn("dbo.Assignment", "MediumId");
        }
    }
}
