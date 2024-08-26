using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public interface ISideMenuService
    {
        ObservableCollection<SideMenuItem> LoadMenuItems(string filePath);
        void SaveMenuItems(ObservableCollection<SideMenuItem> menuItems, string filePath);
    }
}
