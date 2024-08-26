using CommunityToolkit.Mvvm.Messaging;
using Project.Common;
using Project.Services;
using Project.ViewModel;
using Project.Views;
//using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using Project.Views.UserControls;
using System.Configuration;

namespace Project.Views.Windows
{
    public partial class MainWindow : Window
    {
        private readonly IContainer? _container = (IContainer?)App.Current.Properties[MessageToken.AppContainer];

        public MainWindow()
        {
            InitializeComponent();
            SideMenuHost.Content = _container?.Resolve<SideMenuView>();
            UpdateMainContent();
        }

        private void UpdateMainContent()
        {
            WeakReferenceMessenger.Default.Register(this, MessageToken.LoadShowContent, (MessageHandler<MainWindow, object>)((obj, v) =>
            {
                if (v is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                SwitchContent((Type)v);
            }));
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private void SwitchContent(Type type)
        {
            if (_container == null) return;
            var autoView = _container.Resolve(type);
            MainContentHost.Content = autoView;
        }
    }
}