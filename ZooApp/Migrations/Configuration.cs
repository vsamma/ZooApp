namespace ZooApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ZooApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ZooApp.Models.ZooAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ZooApp.Models.ZooAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Species.AddOrUpdate(x => x.Id,
               new Species() { Id = 1, Name = "Koer" },
               new Species() { Id = 2, Name = "Kass" },
               new Species() { Id = 3, Name = "Kana" }
               );

            context.Animals.AddOrUpdate(x => x.Id,
                new Animal()
                {
                    Id = 1,
                    Name = "Muki",
                    YearOfBirth = 2015,
                    Age = 2,
                    CreationDate = DateTime.UtcNow,
                    SpeciesId = 1
                },
                new Animal()
                {
                    Id = 2,
                    Name = "Mr Whiskers",
                    YearOfBirth = 2017,
                    Age = 0,
                    CreationDate = DateTime.UtcNow,
                    SpeciesId = 2
                },
                new Animal()
                {
                    Id = 3,
                    Name = "Mr Peanutbutter",
                    YearOfBirth = 2016,
                    Age = 1,
                    CreationDate = DateTime.UtcNow,
                    SpeciesId = 1
                },
                new Animal()
                {
                    Id = 4,
                    Name = "Sir Barks-A-Lot",
                    YearOfBirth = 2010,
                    Age = 7,
                    CreationDate = DateTime.UtcNow,
                    SpeciesId = 1
                }
                );
        }
    }
}
