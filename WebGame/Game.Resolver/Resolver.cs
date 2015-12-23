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
            builder.RegisterType<Repository<Market>>().As<IRepository<Market>>();
            builder.RegisterType<Repository<Messages>>().As<IRepository<Messages>>();
            builder.RegisterType<Repository<Friends>>().As<IRepository<Friends>>();
            builder.RegisterType<Repository<Notifications>>().As<IRepository<Notifications>>();
            builder.RegisterType<Repository<Deals>>().As<IRepository<Deals>>();
            builder.RegisterType<Repository<DealsBuildings>>().As<IRepository<DealsBuildings>>();
            builder.RegisterType<Repository<ProductRequirements>>().As<IRepository<ProductRequirements>>();
            builder.RegisterType<Repository<Ignored>>().As<IRepository<Ignored>>();



            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<HashPass>().As<IHashPass>();
            builder.RegisterType<BuildingHelper>().As<IBuildingHelper>();

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<MapService>().As<IMapService>();
            builder.RegisterType<UserBuildingService>().As<IUserBuildingService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<MarketService>().As<IMarketService>();
            builder.RegisterType<AdminService>().As<IAdminService>();
            builder.RegisterType<BanService>().As<IBanService>();
            builder.RegisterType<DolarService>().As<IDolarService>();
            builder.RegisterType<BuildingService>().As<IBuildingService>();
            builder.RegisterType<QueueService>().As<IQueueService>();
            builder.RegisterType<UserProductService>().As<IUserProductService>();
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<NotificationService>().As<INotificationService>();
            builder.RegisterType<DealService>().As<IDealService>();
            builder.RegisterType<ProductRequirementsService>().As<IProductRequirementsService>();








        }
    }
}
