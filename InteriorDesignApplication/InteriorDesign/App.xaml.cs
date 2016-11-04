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
        private View.Login login = null;
        private View.MainWindow main = null;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string dir = System.IO.Directory.GetCurrentDirectory();
            string finalDir = dir.Substring(0, dir.LastIndexOf('\\'));
            log4net.GlobalContext.Properties["LogFileName"] = finalDir + "\\log\\IntDesign";
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
                viewModel.PropertyChanged += viewModel_PropertyChanged;
                login = new View.Login(viewModel);                
                login.Show();
            }            
        }

        private void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("AppUser"))
            {
                login.Hide();
                if (main == null)
                {
                    main = new View.MainWindow(viewModel);
                }
                main.Show();
            }
            else if (e.PropertyName.Equals("LogoutUser"))
            {
                main.Close();
                login.Show();
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
            login.Close();
            if (main != null)
            {
                main.Close();
            }            
        }

        IMainViewModel viewModel;
    }
}
