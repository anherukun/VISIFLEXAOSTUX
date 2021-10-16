namespace VisiflexAOSTUX.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using VisiflexAOSTUX.Application;

    internal sealed class Configuration : DbMigrationsConfiguration<VisiflexAOSTUX.Models.VisiflexContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(VisiflexAOSTUX.Models.VisiflexContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.UserRols.AddOrUpdate(new Models.UserRol
            {
                IDUserRol = "00983C02-FDB7-4533-A4ED-D852C3E7EBEB",
                UserLevel = 1,
                UserRolName = "ASISTENTE",
                UserRolPermisions = "+ CREAR NUEVOS ASUNTOS LABORALES\n+ VER EL SEGUMIENTO DE LOS ASUNTOS"
            });
            context.UserRols.AddOrUpdate(new Models.UserRol
            {
                IDUserRol = "AA397230-EA71-479C-B69F-DE4D55B47FB1",
                UserLevel = 4,
                UserRolName = "OPERATIVO",
                UserRolPermisions = "+ AGREGAR MAS ENTRADAS A LOS SEGUIMIENTOS\n" +
                "+ VER EL SEGUMIENTO DE LOS ASUNTOS"
            });
            context.UserRols.AddOrUpdate(new Models.UserRol
            {
                IDUserRol = "D9909DDF-1477-4E3E-95F9-30B9E50E7E26",
                UserLevel = 5,
                UserRolName = "ENCARGADO",
                UserRolPermisions = "+ CREAR NUEVOS ASUNTOS LABORALES\n" +
                "+ VER EL SEGUMIENTO DE LOS ASUNTOS\n+ REASIGNAR ASUNTOS\n+ AGREGAR Y ELIMINAR CUENTAS\n+ ENTRAR A CONFIGURACIONES"
            });
            context.UserRols.AddOrUpdate(new Models.UserRol
            {
                IDUserRol = "6FAC81E5-C092-431E-B521-FFCEFFA6D267",
                UserLevel = 10,
                UserRolName = "SUPERUSR",
                UserRolPermisions = "+ TODOS LOS PERMISOS HABILITADOS"
            });

            context.Accounts.AddOrUpdate(new Models.Account
            {
                IDAccount = "28656403-0676-4520-AB42-347A2C31DCC3",
                Email = "angel.gerardo.jimenezr@pemex.com",
                Name = "ANGEL GERARDO",
                LastName = "JIMENEZ REYES",
                Username = "582509",
                PasswordHash = Security.SHA256Hash("Doodlebundle"),
                IDUserRol = "6FAC81E5-C092-431E-B521-FFCEFFA6D267",
                CreatedAt = DateTime.Now
            });
        }
    }
}
