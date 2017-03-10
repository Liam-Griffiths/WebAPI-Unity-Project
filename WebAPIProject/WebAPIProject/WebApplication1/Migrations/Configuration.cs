namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.WebApplication1Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WebApplication1.Models.WebApplication1Context";
        }

        protected override void Seed(WebApplication1.Models.WebApplication1Context context)
        {

            byte[] saltSeed = new byte[1];

            context.AccountsModel.AddOrUpdate(p => p.username,
                new AccountsModel
                {
                    username = "test",
                    Salt = saltSeed,
                    SaltedAndHashedPassword = saltSeed,
                    email = "test@test.com",
                    organizationId = 0
                },
                new AccountsModel
                {
                    username = "test2",
                    Salt = saltSeed,
                    SaltedAndHashedPassword = saltSeed,
                    email = "test2@test.com",
                    organizationId = 0
                }

            );
        }
    }
}
