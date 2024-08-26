using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class ContentViewModelBase<T>: ObservableObject
    {
        /// <summary>
        ///     数据列表
        /// </summary>
        private IList<T> _dataList;

        /// <summary>
        ///     数据列表
        /// </summary>
        public IList<T> DataList
        {
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }
    }
}
