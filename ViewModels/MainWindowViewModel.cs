using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System;
using PrismSnakeGame.Models;
using Prism.Regions;
using PrismSnakeGame.Views;

namespace PrismSnakeGame.ViewModels { 
public class MainWindowViewModel : BindableBase
{
        private readonly IRegionManager _regionManager;
    public MainWindowViewModel(IRegionManager regionManager)
    {
            
            _regionManager = regionManager;
            //regionManager.RegisterViewWithRegion("ContentRegion", typeof(SnakeGame));
            //regionManager.RequestNavigate("ContentRegion", "SnakeGame");

        }
}
}