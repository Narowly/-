using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ProjectViewModels
{
    public class RegionVm : ObservableObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _name = null!; // 使用null!来表示这个字段确实可以为null，并且你已经明确知道这一点  
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}