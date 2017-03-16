namespace BookShopSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelatedBooksTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Book_Id", "dbo.Books");
            DropIndex("dbo.Books", new[] { "Book_Id" });
            CreateTable(
                "dbo.BookRelatedBooks",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        RelatedBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.RelatedBookId })
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.Books", t => t.RelatedBookId)
                .Index(t => t.BookId)
                .Index(t => t.RelatedBookId);
            
            DropColumn("dbo.Books", "Book_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Book_Id", c => c.Int());
            DropForeignKey("dbo.BookRelatedBooks", "RelatedBookId", "dbo.Books");
            DropForeignKey("dbo.BookRelatedBooks", "BookId", "dbo.Books");
            DropIndex("dbo.BookRelatedBooks", new[] { "RelatedBookId" });
            DropIndex("dbo.BookRelatedBooks", new[] { "BookId" });
            DropTable("dbo.BookRelatedBooks");
            CreateIndex("dbo.Books", "Book_Id");
            AddForeignKey("dbo.Books", "Book_Id", "dbo.Books", "Id");
        }
    }
}
