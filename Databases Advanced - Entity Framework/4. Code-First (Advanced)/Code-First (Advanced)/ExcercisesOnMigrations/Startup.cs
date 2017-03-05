namespace ExcercisesOnMigrations
{
    class Startup
    {
        static void Main(string[] args)
        {
            var ctx = new SalesContext();
            ctx.Database.Initialize(true);
            
            
        }
    }
}
