using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    internal class AssemblyHelper
    {
        // 获取当前程序集的命名空间  
        private static readonly string NameSpaceStr = typeof(AssemblyHelper).Assembly.GetName().Name;

        // 用于缓存已注册的实例  
        private static readonly Dictionary<string, object> CacheDic = new Dictionary<string, object>();

        // 注册实例方法，允许通过键来存储和检索实例  
        public static void Register(string name, object instance)
        {
            CacheDic[name] = instance;
        }

        // 通过键解析实例方法，如果找到则返回实例，否则返回null  
        public static object ResolveByKey(string key)
        {
            if (CacheDic.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

        // 创建内部实例方法，根据类名创建并返回实例，如果失败则返回null  
        public static object CreateInternalInstance(string className)
        {
            try
            {
                // 构建完整的类型名称  
                var type = Type.GetType($"{NameSpaceStr}.{className}");

                // 如果类型不为null，则创建并返回实例  
                return type != null ? Activator.CreateInstance(type) : null;
            }
            catch
            {
                // 如果发生任何异常，则返回null  
                return null;
            }
        }

        public static Type GetType(string className)
        {
            try
            {
                return Type.GetType($"{NameSpaceStr}.{className}");
            }
            catch
            {
                return null;
            }
        }
    }
}
