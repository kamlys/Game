using Autofac;
using Game.Dal;
using Game.Dal.Model;
using Game.Dal.Repository;
using Game.Service;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Resolver
{
    public class Resolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Repository<Admins>>().As<IRepository<Admins>>();
            builder.RegisterType<Repository<Bans>>().As<IRepository<Bans>>();
            builder.RegisterType<Repository<Buildings>>().As<IRepository<Buildings>>();
            builder.RegisterType<Repository<Maps>>().As<IRepository<Maps>>();
            builder.RegisterType<Repository<Products>>().As<IRepository<Products>>();
            builder.RegisterType<Repository<UserBuildings>>().As<IRepository<UserBuildings>>();
            builder.RegisterType<Repository<UserProducts>>().As<IRepository<UserProducts>>();
            builder.RegisterType<Repository<Users>>().As<IRepository<Users>>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<BuildingService>().As<IBuildingService>();
        }
    }
}
