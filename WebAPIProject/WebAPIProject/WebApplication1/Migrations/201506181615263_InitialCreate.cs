namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.AccountsModels",
                c => new
                    {
                        primaryKey = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 30),
                        Salt = c.Binary(nullable: false),
                        SaltedAndHashedPassword = c.Binary(nullable: false),
                        email = c.String(nullable: false),
                        organization = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.primaryKey);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountsModels");
        }
    }
}
