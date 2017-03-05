namespace _3.Code_First__Advanced_
{
    class Startup
    {
        static void Main()
        {
            var context = new SalesContext();
            context.Database.Initialize(true);
        }
    }
}
