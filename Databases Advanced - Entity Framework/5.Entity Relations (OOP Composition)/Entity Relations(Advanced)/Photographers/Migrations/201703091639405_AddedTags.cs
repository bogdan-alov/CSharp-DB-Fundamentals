namespace Photographers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TagName, unique: true, name: "Tag");
            
            AddColumn("dbo.Albums", "Tag_Id", c => c.Int());
            CreateIndex("dbo.Albums", "Tag_Id");
            AddForeignKey("dbo.Albums", "Tag_Id", "dbo.Tags", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.Tags", "Tag");
            DropIndex("dbo.Albums", new[] { "Tag_Id" });
            DropColumn("dbo.Albums", "Tag_Id");
            DropTable("dbo.Tags");
        }
    }
}
