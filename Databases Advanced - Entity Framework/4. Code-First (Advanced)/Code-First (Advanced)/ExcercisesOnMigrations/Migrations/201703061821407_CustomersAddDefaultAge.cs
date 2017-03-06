namespace ExcercisesOnMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomersAddDefaultAge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Age", c => c.Int(defaultValue: 20, defaultValueSql: "UPDATE Customers SET Age = 20 WHERE Age IS NULL ", nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Age");
        }
    }
}
