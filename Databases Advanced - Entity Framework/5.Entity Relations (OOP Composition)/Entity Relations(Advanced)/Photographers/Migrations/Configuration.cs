namespace Photographers.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Photographers.PhotographersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Photographers.PhotographersContext";
        }

        protected override void Seed(Photographers.PhotographersContext context)
        {
            //  This method will be called after migrating to the latest version.
            var ctx = new PhotographersContext();
            ctx.Tags.AddOrUpdate(p => p.TagText,

                new Tag { TagText = "#WTF" },
                new Tag { TagText = "#LoLisGood" },
                new Tag { TagText = "#WhySoSerious?!" }
                );
            ctx.Albums.AddOrUpdate(p => p.Name,
                new Album { Name = "Abiturientski Bal 2016", BackgroundColor = "Brown", AlbumsType = Album.AlbumType.Private, Owner = ctx.Photographers.Where(p=> p.Id == 1).FirstOrDefault() },
                new Album { Name = "Abiturientski Bal 2015", BackgroundColor = "White", AlbumsType = Album.AlbumType.Private, Owner = ctx.Photographers.Where(p => p.Id == 2).FirstOrDefault() },
                new Album { Name = "SoftUniada 2017", BackgroundColor = "Blue", AlbumsType = Album.AlbumType.Public,
                    Owner = ctx.Photographers.Where(p => p.Id == 3).FirstOrDefault()
                }
                );
            ctx.Pictures.AddOrUpdate(p => p.Title,
                new Picture { Title = "Helloguys", Caption = "D3000S" },
                new Picture { Title = "Helloguys1", Caption = "D3001S" },
                new Picture { Title = "Helloguys2", Caption = "D3002S" },
                new Picture { Title = "Helloguys3", Caption = "D3003S" },
                new Picture { Title = "Helloguys4", Caption = "D3004S" },
                new Picture { Title = "Helloguys5", Caption = "D3005S" },
                new Picture { Title = "Helloguys6", Caption = "D3006S" },
                new Picture { Title = "Helloguys7", Caption = "D3007S" },
                new Picture { Title = "Helloguys8", Caption = "D3007S" },
                new Picture { Title = "Helloguys9", Caption = "D3007S" },
                new Picture { Title = "Helloguys10", Caption = "D3007S" }
                );

            //ctx.Roles.AddOrUpdate(r => r.RoleName,
            //    new Role { RoleName = "Viewer" },
            //    new Role { RoleName = "Owner" }
            //    );


            ctx.SaveChanges();
            base.Seed(ctx);
        }
    }
}
