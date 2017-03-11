namespace BankSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelations : DbMigration
    {
        public override void Up()
        {
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
        }
    }
}
