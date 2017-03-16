namespace BankSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CheckingAccounts", "User_Id", c => c.Int());
            AddColumn("dbo.SavingAccounts", "User_Id", c => c.Int());
            CreateIndex("dbo.CheckingAccounts", "User_Id");
            CreateIndex("dbo.SavingAccounts", "User_Id");
            AddForeignKey("dbo.CheckingAccounts", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.SavingAccounts", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavingAccounts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.CheckingAccounts", "User_Id", "dbo.Users");
            DropIndex("dbo.SavingAccounts", new[] { "User_Id" });
            DropIndex("dbo.CheckingAccounts", new[] { "User_Id" });
            DropColumn("dbo.SavingAccounts", "User_Id");
            DropColumn("dbo.CheckingAccounts", "User_Id");
            DropTable("dbo.Users");
        }
    }
}
