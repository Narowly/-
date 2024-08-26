using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    internal class Messagebus
    {
    }
    public class OpenMainWindowMessage
    {

        // 构造函数  
        public OpenMainWindowMessage()
        {
        }
    }

    public class NavigateMessage
    {
        public string PageKey { get; set; }

        public NavigateMessage(string pageKey)
        {
            PageKey = pageKey;
        }
    }
}
