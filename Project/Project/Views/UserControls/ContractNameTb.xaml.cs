using Project.Common;
using Project.ViewModel;
using Project.Views.Windows;
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
using Project.Services;

namespace Project.Views.UserControls
{
    /// <summary>
    /// ContractNameTb.xaml 的交互逻辑
    /// </summary>
    public partial class ContractNameTb : UserControl
    {
        
        public ContractNameTb(ContractNameTbViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
            
        }
    }
}
