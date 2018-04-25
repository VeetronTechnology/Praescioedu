namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignment", "PublishedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assignment", "PublishedDate");
        }
    }
}
