namespace ExcercisesOnMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesAddDefaultDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Sales", "Date", s => s.String(defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("Sales", "Date", s => s.String(defaultValueSql: "NULL"));
        }
    }
}
