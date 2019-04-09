using TypographyServiceDAL.Interfaces;
using TypographyServiceImplementDataBase.Implementations;
using TypographyServiceImplementDataBase;
using System;
using System.Windows.Forms;
using System.Data.Entity;
using Unity;
using Unity.Lifetime;

namespace TypographyView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, TypographyDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerService, CustomerServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPartService, PartServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IItemService, ItemServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceDB>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
