namespace ExcercisesOnMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsAddColumnDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Description", c => c.String(defaultValue: "No description.", defaultValueSql: "UPDATE Products SET Description = 'No description' WHERE Descrition IS NULL ", nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Description");
        }
    }
}
