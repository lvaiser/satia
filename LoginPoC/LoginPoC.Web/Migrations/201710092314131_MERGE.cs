namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MERGE : DbMigration
    {
        public override void Up()
        {
            // Se borró el contenido a propósito debido a que la última migration se creó sin correr la anterior.

            // Si vuelve a pasar, SOLUCION:
            // 1) Add -Migration MERGE 
            // 2) Abrir el archivo <TIMESTAMP>_MERGE.cs y comentar todo el código duplicado de migraciones anteriores.

            // explicación en: https://stackoverflow.com/questions/17921886/update-database-fails-due-to-pending-changes-but-add-migration-creates-a-duplic?rq=1
        }

        public override void Down()
        {

        }
    }
}
