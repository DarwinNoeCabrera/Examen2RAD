namespace Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actalizaciontablas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 10),
                        DNI = c.String(nullable: false, maxLength: 25),
                        Nombres = c.String(nullable: false, maxLength: 80),
                        Apellidos = c.String(nullable: false, maxLength: 80),
                        FechaIngreso = c.DateTime(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        ReservacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId)
                .ForeignKey("dbo.Reservaciones", t => t.ReservacionId, cascadeDelete: true)
                .Index(t => t.ReservacionId);
            
            CreateTable(
                "dbo.Reservaciones",
                c => new
                    {
                        ReservacionId = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 10),
                        Descripcion = c.String(nullable: false, maxLength: 150),
                        FechaReservacion = c.DateTime(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReservacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clientes", "ReservacionId", "dbo.Reservaciones");
            DropIndex("dbo.Clientes", new[] { "ReservacionId" });
            DropTable("dbo.Reservaciones");
            DropTable("dbo.Clientes");
        }
    }
}
