using Project.ViewModel;
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

namespace Project.Views.UserControls
{
    /// <summary>
    /// ApplicationHandleView.xaml 的交互逻辑
    /// </summary>
    public partial class ApplicationHandleView : UserControl
    {
        public ApplicationHandleView(ApplicationHandleViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
