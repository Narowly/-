using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class TextVm : ObservableObject
    {
        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private object _value;
        public object Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
    public class ComboxVm : ObservableObject
    {
        private string _text = null!;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private int _value;
        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}
