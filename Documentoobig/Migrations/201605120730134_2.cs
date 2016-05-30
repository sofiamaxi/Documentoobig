namespace Documentoobig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Client_Id = c.Int(),
                        From_Id = c.Int(),
                        FromDep_Id = c.Int(),
                        Staff_Id = c.Int(),
                        To_Id = c.Int(),
                        ToDep_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .ForeignKey("dbo.Cities", t => t.From_Id)
                .ForeignKey("dbo.Departments", t => t.FromDep_Id)
                .ForeignKey("dbo.Staffs", t => t.Staff_Id)
                .ForeignKey("dbo.Cities", t => t.To_Id)
                .ForeignKey("dbo.Departments", t => t.ToDep_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.From_Id)
                .Index(t => t.FromDep_Id)
                .Index(t => t.Staff_Id)
                .Index(t => t.To_Id)
                .Index(t => t.ToDep_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ToDep_Id", "dbo.Departments");
            DropForeignKey("dbo.Orders", "To_Id", "dbo.Cities");
            DropForeignKey("dbo.Orders", "Staff_Id", "dbo.Staffs");
            DropForeignKey("dbo.Orders", "FromDep_Id", "dbo.Departments");
            DropForeignKey("dbo.Orders", "From_Id", "dbo.Cities");
            DropForeignKey("dbo.Orders", "Client_Id", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "ToDep_Id" });
            DropIndex("dbo.Orders", new[] { "To_Id" });
            DropIndex("dbo.Orders", new[] { "Staff_Id" });
            DropIndex("dbo.Orders", new[] { "FromDep_Id" });
            DropIndex("dbo.Orders", new[] { "From_Id" });
            DropIndex("dbo.Orders", new[] { "Client_Id" });
            DropTable("dbo.Orders");
        }
    }
}
