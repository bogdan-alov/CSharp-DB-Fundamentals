namespace BookShopSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelatedBooks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Book_Id", c => c.Int());
            CreateIndex("dbo.Books", "Book_Id");
            AddForeignKey("dbo.Books", "Book_Id", "dbo.Books", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Book_Id", "dbo.Books");
            DropIndex("dbo.Books", new[] { "Book_Id" });
            DropColumn("dbo.Books", "Book_Id");
        }
    }
}
