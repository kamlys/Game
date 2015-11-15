using Autofac;
using Game.Dal;
using Game.Dal.Model;
using Game.Dal.Repository;
using Game.Service;
using Game.Service.Helpers;
using Game.Service.Interfaces;
using Game.Service.Interfaces.TableInterface;
using Game.Service.Table;
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
            builder.RegisterType<Repository<Dolars>>().As<IRepository<Dolars>>();
            builder.RegisterType<Repository<BuildingQueue>>().As<IRepository<BuildingQueue>>();


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<HashPass>().As<IHashPass>();
            builder.RegisterType<BuildingHelper>().As<IBuildingHelper>();

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<MapService>().As<IMapService>();
            builder.RegisterType<UserBuildingService>().As<IUserBuildingService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<AdminService>().As<IAdminService>();

            builder.RegisterType<AdminTableService>().As<IAdminTableService>();
            builder.RegisterType<DolarTableService>().As<IDolarTableService>();
            builder.RegisterType<BanTableService>().As<IBanTableService>();
            builder.RegisterType<BuildingTableService>().As<IBuildingTableService>();
            builder.RegisterType<MapTableService>().As<IMapTableService>();
            builder.RegisterType<ProductTableService>().As<IProductTableService>();
            builder.RegisterType<QueueTableService>().As<IQueueTableService>();
            builder.RegisterType<UserBuildingTableService>().As<IUserBuildingTableService>();
            builder.RegisterType<UserProductTableService>().As<IUserProductTableService>();
            builder.RegisterType<UserTableService>().As<IUserTableService>();





        }
    }
}
