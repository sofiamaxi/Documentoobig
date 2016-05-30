namespace Documentoobig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Height", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "Width", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "Length", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "Weight", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Weight");
            DropColumn("dbo.Orders", "Length");
            DropColumn("dbo.Orders", "Width");
            DropColumn("dbo.Orders", "Height");
        }
    }
}
