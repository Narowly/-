using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using HandyControl.Data;
using Project.Common;
using Project.Services;
using System.Collections.ObjectModel;
using System.IO;

namespace Project.ViewModel
{
    public class SideMenuViewModel: ContentViewModelBase<SideMenuItem>
    {
        private readonly ISideMenuService _sideMenuService;
        public ObservableCollection<SideMenuItem> MenuItems { get; } = [];
        private HeaderedSimpleItemsControl? _currentSelectedItem;
        public HeaderedSimpleItemsControl? CurrentSelectedItem
        {
            get => _currentSelectedItem;
            set => SetProperty(ref _currentSelectedItem, value);
        }

        //public RelayCommand<SideMenuItem> SelectItemCommand { get; }
        private object? _contentTitle;
        public object? ContentTitle
        {
            get => _contentTitle;
            set => SetProperty(ref _contentTitle, value);
        }

        public RelayCommand<FunctionEventArgs<object>> SwitchContentCmd => new(SwitchContent);
        public SideMenuViewModel(ISideMenuService sideMenuService)
        {
            _sideMenuService = sideMenuService;
            LoadMenuItems();
        }

        private void LoadMenuItems()
        {
            string settingsFolderPath = "settings";
            string fileName = "sidemenu.json";
            string filePath = Path.Combine(settingsFolderPath, fileName);

            ObservableCollection<SideMenuItem> menuItems = _sideMenuService.LoadMenuItems(filePath);
            // ... 使用menuItems来更新你的ViewModel的属性或集合  
            foreach (SideMenuItem item in menuItems)
            {
                MenuItems.Add(item);
            }
        }

        private void SwitchContent(FunctionEventArgs<object>? e)
        {            
            if (e.Info == null) return;
            if (e.Info is HeaderedSimpleItemsControl item)
            {
                if (Equals(CurrentSelectedItem, item)) return;
                SwitchContent(item);
            }
        }

        private void SwitchContent(HeaderedSimpleItemsControl item)
        {
            CurrentSelectedItem = item;
            ContentTitle = item.Header;
            if (item.Tag == null) return;
            var type = AssemblyHelper.GetType($"Views.UserControls.{item.Tag}");
            WeakReferenceMessenger.Default.Send((object)type, MessageToken.LoadShowContent);
        }

        

    }
    public class FullSwitchMessage
    {
        public bool IsFull { get; set; }

        public FullSwitchMessage(bool isFull) => IsFull = isFull;
    }
    public class SideMenuItem
    {
        public string? Header { get; set; } // 侧边栏项的标题  
        public string? Icon { get; set; } // 侧边栏项的图标（可以是路径或资源名称）  
        public string? ContentView { get; set; } // 与侧边栏项关联的视图或视图模型  
    }
}
