
namespace RASmartOffice.Core.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RASmartOffice.Core.Context.RASOConext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RASmartOffice.Core.Context.RASOConext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //initial Role
            context.Role.AddOrUpdate(
                new Entities.Role { ID = 1, RoleCode = "admin", Description = "This is admin role." },
                new Entities.Role { ID = 2, RoleCode = "user", Description = "This is user role." }
            );

            //initial amdin user
            context.User.AddOrUpdate(
                new Entities.User { ID = 1, UserName = "admin", Password = "1000:ThJMsiBMSaAFTLInb+u5SU/vIqDKsChs:wlo5v9dGHYKVSpgtZDpNPg==", DisplayName = "Admin" }
            );

            //initial user role mapping
            context.UserRole.AddOrUpdate(
                new Entities.UserRole { ID = 1, UserID = 1, RoleID = 1 }
            );
        }
    }
}
