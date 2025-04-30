using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using PrismSnakeGame.ViewModels;
using PrismSnakeGame.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PrismSnakeGame
{
    public partial class App
    {
        protected override Window CreateShell()
        {
            return null;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
    public class Bootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<SnakeGame>();
        }

        protected override void InitializeShell(DependencyObject shell)
        {
            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(SnakeGame));
            //regionManager.RequestNavigate("ContentRegion", "SnakeGame");
            Application.Current.MainWindow = (Window)shell;
            Application.Current.MainWindow.Show();
        }

    //    private void NavigationCallback(NavigationResult result)
    //    {
    //        if (result.Result == true)
    //        {
    //            var regionManager = Container.Resolve<IRegionManager>();
    //            var region = regionManager.Regions["MainRegion"];
    //            var view = region.ActiveViews.FirstOrDefault() as SnakeGame;
    //            view?.Focus();
    //            Keyboard.Focus(view);
    //        }
    //    }
    }
}