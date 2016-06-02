using System;
using System.Windows;
using ViewModel;
using log4net;
using System.Reflection;
using System.Diagnostics;

namespace InteriorDesign
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
            System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();
            if (Process.GetProcessesByName(assembly.Name).Length > 1)
            {
                Log.ErrorFormat("{0} is already running.", assembly.Name);
                Current.Shutdown();
            }
            else
            {
                this.viewModel = new MainViewModel();
                View.MainWindow main = new View.MainWindow(viewModel);
                main.Show();
            }            
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;
            string error = "CurrentDomain_UnhandledException() - " + exception.ToString();
            while (null != exception.InnerException)
            {
                exception = exception.InnerException;
                error += " : Inner Exception = " + exception.Message;
            }
            Log.Error(error);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Log.Info("Exit Application...");
        }

        IMainViewModel viewModel;
    }
}
