using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using CommunityToolkit.Mvvm.Messaging;
using Project.Common;
using Project.Services;
using Project.ViewModel;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.Views.Windows
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            RegisterOpenWindowMessage();
        }

        private void RegisterOpenWindowMessage()
        {
            WeakReferenceMessenger.Default.Register(this, MessageToken.OpenMainWindow, (MessageHandler<LoginWindow, string>)((r, m) =>
            {
                var container = (IContainer?)App.Current.Properties[MessageToken.AppContainer];
                if(container == null)
                {
                    MessageBox.Warning("Container不存在", "系统错误");
                    return;
                }
                var sideMenuService = container.Resolve<ISideMenuService>();
                // 创建并显示 MainWindow  
                //var mainWindow = container.Resolve<MainWindow>(new NamedParameter("sideMenuService", sideMenuService));
                var mainWindow = container.Resolve<MainWindow>();
                mainWindow.Show();
                Close();
            }));
        }        

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
