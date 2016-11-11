using System;
using System.Windows.Forms;
using Autofac;
using SemaphoreTest.Presenter;
using SemaphoreTest.View;

namespace SemaphoreTest
{
    static class Program
    {
        public static IContainer Container { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();
            builder.RegisterType<PresenterMain>().As<IPresenterMain>();
            builder.RegisterType<PresenterSemaphore>().As<IPresenterSemaphore>();
            builder.RegisterType<FormMain>().As<IViewMain>();
            builder.RegisterType<UctrlSemaphore>().As<IViewSemaphore>();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope()) {
                var presenterMain = scope.Resolve<IPresenterMain>(
                    new NamedParameter("viewMain", scope.Resolve<IViewMain>()));
                presenterMain.Run();
            }

        }


    }
}
