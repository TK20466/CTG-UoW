using CtgModels.DataModels.Costume;

namespace CtgDataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CtgDataAccess.db.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CtgDataAccess.db.DataContext context)
        {
            context.Costumes.AddOrUpdate(x => x.Name, new LegionCostume
            {
                Name = "ANH Stunt Stormtrooper",
                Prefix = "TK"
            }, new LegionCostume
            {
                Name = "ESB Stormtrooper",
                Prefix = "TK"
            }, new LegionCostume
            {
                Name = "Sandtrooper",
                Prefix = "TD"
            }, new LegionCostume
            {
                Name = "ANH Darth Vader",
                Prefix = "SL"
            });
        }
    }
}
