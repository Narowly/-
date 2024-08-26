﻿using ProjectViewModels;
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
    /// ProjectStaff.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectStaffUc : UserControl
    {
        public ProjectStaffUc()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var p = Window.GetWindow(this);
            if (p != null) p.Close();
        }

        
    }
}
