using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Services
{
    public class SideMenuService : ISideMenuService
    {
        public ObservableCollection<SideMenuItem> LoadMenuItems(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<ObservableCollection<SideMenuItem>>(jsonString) ?? new ObservableCollection<SideMenuItem>();
            }
            else
            {
                return new ObservableCollection<SideMenuItem>();
            }
        }

        public void SaveMenuItems(ObservableCollection<SideMenuItem> menuItems, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(menuItems);
            File.WriteAllText(filePath, jsonString);
        }
    }
}
