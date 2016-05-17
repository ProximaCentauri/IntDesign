using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using View;
using Model.Controls;
using Model;
using ViewModel;

namespace Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Resources.MergedDictionaries.Add(LoadComponent(new Uri("SkinContent;component/SkinContent.xaml", UriKind.Relative)) as ResourceDictionary);
            this.viewModel = new MainViewModel();
            View.MainWindow main = new View.MainWindow(viewModel);
            main.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {

        }

        IMainViewModel viewModel;
    }
}
