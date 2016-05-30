namespace Documentoobig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "CityID", "dbo.Cities");
            DropIndex("dbo.Orders", new[] { "Client_Id" });
            DropIndex("dbo.Orders", new[] { "From_Id" });
            DropIndex("dbo.Orders", new[] { "FromDep_Id" });
            DropIndex("dbo.Orders", new[] { "Staff_Id" });
            DropIndex("dbo.Orders", new[] { "To_Id" });
            DropIndex("dbo.Orders", new[] { "ToDep_Id" });
            RenameColumn(table: "dbo.Orders", name: "Client_Id", newName: "ClientID");
            RenameColumn(table: "dbo.Orders", name: "From_Id", newName: "FromCityID");
            RenameColumn(table: "dbo.Orders", name: "FromDep_Id", newName: "FromDepID");
            RenameColumn(table: "dbo.Orders", name: "Staff_Id", newName: "StaffID");
            RenameColumn(table: "dbo.Orders", name: "To_Id", newName: "ToCityID");
            RenameColumn(table: "dbo.Orders", name: "ToDep_Id", newName: "ToDepID");
            AlterColumn("dbo.Orders", "ClientID", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "FromCityID", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "FromDepID", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "StaffID", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ToCityID", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "ToDepID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "FromCityID");
            CreateIndex("dbo.Orders", "FromDepID");
            CreateIndex("dbo.Orders", "ToCityID");
            CreateIndex("dbo.Orders", "ToDepID");
            CreateIndex("dbo.Orders", "ClientID");
            CreateIndex("dbo.Orders", "StaffID");
            AddForeignKey("dbo.Departments", "CityID", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "CityID", "dbo.Cities");
            DropIndex("dbo.Orders", new[] { "StaffID" });
            DropIndex("dbo.Orders", new[] { "ClientID" });
            DropIndex("dbo.Orders", new[] { "ToDepID" });
            DropIndex("dbo.Orders", new[] { "ToCityID" });
            DropIndex("dbo.Orders", new[] { "FromDepID" });
            DropIndex("dbo.Orders", new[] { "FromCityID" });
            AlterColumn("dbo.Orders", "ToDepID", c => c.Int());
            AlterColumn("dbo.Orders", "ToCityID", c => c.Int());
            AlterColumn("dbo.Orders", "StaffID", c => c.Int());
            AlterColumn("dbo.Orders", "FromDepID", c => c.Int());
            AlterColumn("dbo.Orders", "FromCityID", c => c.Int());
            AlterColumn("dbo.Orders", "ClientID", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "ToDepID", newName: "ToDep_Id");
            RenameColumn(table: "dbo.Orders", name: "ToCityID", newName: "To_Id");
            RenameColumn(table: "dbo.Orders", name: "StaffID", newName: "Staff_Id");
            RenameColumn(table: "dbo.Orders", name: "FromDepID", newName: "FromDep_Id");
            RenameColumn(table: "dbo.Orders", name: "FromCityID", newName: "From_Id");
            RenameColumn(table: "dbo.Orders", name: "ClientID", newName: "Client_Id");
            CreateIndex("dbo.Orders", "ToDep_Id");
            CreateIndex("dbo.Orders", "To_Id");
            CreateIndex("dbo.Orders", "Staff_Id");
            CreateIndex("dbo.Orders", "FromDep_Id");
            CreateIndex("dbo.Orders", "From_Id");
            CreateIndex("dbo.Orders", "Client_Id");
            AddForeignKey("dbo.Departments", "CityID", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
