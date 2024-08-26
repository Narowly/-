using HandyControl.Controls;
using Project.Common;
using Project.Services;
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
using Autofac;

namespace Project.Views.UserControls
{
    /// <summary>
    /// ContractView.xaml 的交互逻辑
    /// </summary>
    public partial class ContractView
    {
        public ContractView(ContractViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
