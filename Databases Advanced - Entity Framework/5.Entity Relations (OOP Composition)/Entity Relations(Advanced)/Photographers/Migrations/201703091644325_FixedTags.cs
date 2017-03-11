namespace Photographers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedTags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.Albums", new[] { "Tag_Id" });
            CreateTable(
                "dbo.TagAlbums",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Album_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Album_Id);
            
            DropColumn("dbo.Albums", "Tag_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Tag_Id", c => c.Int());
            DropForeignKey("dbo.TagAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.TagAlbums", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagAlbums", new[] { "Album_Id" });
            DropIndex("dbo.TagAlbums", new[] { "Tag_Id" });
            DropTable("dbo.TagAlbums");
            CreateIndex("dbo.Albums", "Tag_Id");
            AddForeignKey("dbo.Albums", "Tag_Id", "dbo.Tags", "Id");
        }
    }
}
