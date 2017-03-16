using Photographers.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace Photographers
{
    class Startup
    {
        static void Main()
        {
            var ctx = new PhotographersContext();
            //ctx.Database.Initialize(true);


            //InsertSomeData(ctx);


            
            InsertSomeTag(ctx);

            

        }

        private static void InsertSomeTag(PhotographersContext ctx)
        {
            Tag tag = new Tag();
            Console.WriteLine("Give me some tag, please!");
            tag.TagText = Console.ReadLine();
            try
            {
                ctx.Tags.Add(tag);
                ctx.SaveChanges();
                Console.WriteLine($"{tag.TagText} successfully added to database!");
            }
            catch (DbEntityValidationException)
            {
                tag.TagText = TagTransformer.Transform(tag.TagText);
                ctx.SaveChanges();
                Console.WriteLine($"{tag.TagText} successfully converted and added to database!");
            }
        }

        private static void InsertSomeData(PhotographersContext ctx)
        {
            var photographer1 = new Photographer()
            {
                Username = "aha332",
                Email = "aha@abv.bg",
                Password = "123321",
                RegisterDate = DateTime.Now,
                BirthDate = Convert.ToDateTime("12/10/1997")

            };


            var photographer2 = new Photographer()
            {
                Username = "ah2332",
                Email = "aha@abv.bg",
                Password = "123321",
                RegisterDate = DateTime.Now,
                BirthDate = Convert.ToDateTime("12/10/1997")
            };

            var photographer3 = new Photographer()
            {
                Username = "ah2311a332",
                Email = "aha@abv.bg",
                Password = "123321",
                RegisterDate = DateTime.Now,
                BirthDate = Convert.ToDateTime("12/10/1997")

            };

            ctx.Photographers.Add(photographer1);

            ctx.Photographers.Add(photographer2);

            ctx.Photographers.Add(photographer3);

            ctx.SaveChanges();
        }
    }
}
