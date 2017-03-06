using ExcercisesOnMigrations.Models;
using System;

namespace ExcercisesOnMigrations
{
    class Startup
    {
        static void Main()
        {
            //Revert to InitialCreate migration first. After that Initialize the database after that make the migrations one by one. Comment the INITIALIZER FOR SAFE WORK WITH MIGRATIONS!!! 
            var context = new SalesContext();
            context.Database.Initialize(true);
        }
    }
}
