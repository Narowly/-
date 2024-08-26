namespace ProjectService.Helper
{
    public static class LogManager
    {
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(LogManager));

        // 在应用程序启动时调用此方法以配置log4net（如果需要）  
        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure(); // 或者使用具体的配置文件路径  
        }
    }
}
