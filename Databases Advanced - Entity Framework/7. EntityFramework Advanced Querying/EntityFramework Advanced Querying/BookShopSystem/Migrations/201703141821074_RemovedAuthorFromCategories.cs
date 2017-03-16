namespace BookShopSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAuthorFromCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Categories", new[] { "Author_Id" });
            DropColumn("dbo.Categories", "Author_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Author_Id", c => c.Int());
            CreateIndex("dbo.Categories", "Author_Id");
            AddForeignKey("dbo.Categories", "Author_Id", "dbo.Authors", "Id");
        }
    }
}
