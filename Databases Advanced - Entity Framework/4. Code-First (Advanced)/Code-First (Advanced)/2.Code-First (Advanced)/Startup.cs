namespace _2.Code_First__Advanced_
{
    class Startup
    {
        static void Main()
        {
            var context = new LocalStoreContext();
            context.Database.Initialize(true);
        }
    }
}
