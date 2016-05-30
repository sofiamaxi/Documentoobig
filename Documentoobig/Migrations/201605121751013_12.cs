namespace Documentoobig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Cost", c => c.Double(nullable: false));
           
        }
        
        public override void Down()
        {
            
            DropColumn("dbo.Orders", "Cost");
        }
    }
}
