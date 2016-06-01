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
using log4net;

namespace Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            log4net.GlobalContext.Properties["LogFileName"] = System.IO.Directory.GetCurrentDirectory() + "\\log\\IntDesign";
            log4net.Config.XmlConfigurator.Configure();
            Resources.MergedDictionaries.Add(LoadComponent(new Uri("SkinContent;component/SkinContent.xaml", UriKind.Relative)) as ResourceDictionary);
            Log.Info("Startup Application...");
            this.viewModel = new MainViewModel();
            View.MainWindow main = new View.MainWindow(viewModel);
            main.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Log.Info("Exit Application...");
        }

        IMainViewModel viewModel;
    }
}
