namespace Photographers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAlbumsPictures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BackgroundColor = c.String(nullable: false),
                        AlbumsType = c.Int(nullable: false),
                        Owner_Id = c.Int(nullable: false),
                        Photographer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photographers", t => t.Owner_Id, cascadeDelete: true)
                .ForeignKey("dbo.Photographers", t => t.Photographer_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Photographer_Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Caption = c.String(nullable: false, maxLength: 30),
                        Path = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PictureAlbums",
                c => new
                    {
                        Picture_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Picture_Id, t.Album_Id })
                .ForeignKey("dbo.Pictures", t => t.Picture_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Picture_Id)
                .Index(t => t.Album_Id);
            
            AddColumn("dbo.Photographers", "Album_Id", c => c.Int());
            CreateIndex("dbo.Photographers", "Album_Id");
            AddForeignKey("dbo.Photographers", "Album_Id", "dbo.Albums", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "Photographer_Id", "dbo.Photographers");
            DropForeignKey("dbo.PictureAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.PictureAlbums", "Picture_Id", "dbo.Pictures");
            DropForeignKey("dbo.Photographers", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.Albums", "Owner_Id", "dbo.Photographers");
            DropIndex("dbo.PictureAlbums", new[] { "Album_Id" });
            DropIndex("dbo.PictureAlbums", new[] { "Picture_Id" });
            DropIndex("dbo.Albums", new[] { "Photographer_Id" });
            DropIndex("dbo.Albums", new[] { "Owner_Id" });
            DropIndex("dbo.Photographers", new[] { "Album_Id" });
            DropColumn("dbo.Photographers", "Album_Id");
            DropTable("dbo.PictureAlbums");
            DropTable("dbo.Pictures");
            DropTable("dbo.Albums");
        }
    }
}
