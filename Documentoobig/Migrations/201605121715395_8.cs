namespace Documentoobig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Receivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DirectorFName = c.String(),
                        DirectorLName = c.String(),
                        DirectorPName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "ReceiverID", c => c.Int(nullable: false));
            AddColumn("dbo.Staffs", "CompanyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ReceiverID");
            CreateIndex("dbo.Staffs", "CompanyId");
            AddForeignKey("dbo.Orders", "ReceiverID", "dbo.Receivers", "Id");
            AddForeignKey("dbo.Staffs", "CompanyId", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Orders", "ReceiverID", "dbo.Receivers");
            DropIndex("dbo.Staffs", new[] { "CompanyId" });
            DropIndex("dbo.Orders", new[] { "ReceiverID" });
            DropColumn("dbo.Staffs", "CompanyId");
            DropColumn("dbo.Orders", "ReceiverID");
            DropTable("dbo.Companies");
            DropTable("dbo.Receivers");
        }
    }
}
