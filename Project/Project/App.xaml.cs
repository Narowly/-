using Autofac;
using Autofac.Core;
using CommunityToolkit.Mvvm.Messaging;
using Project.Services;
using Project.Views;
using Project.ViewModel;
using RestSharp;
using System.Windows;
using Project.Views.UserControls;
using Project.Views.Windows;
using Project.Common;
using Project.Services.DataServices;
using ProjectViewModels;
using System.Net.NetworkInformation;

namespace Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer _container = null!;
        public static IMessenger Messenger { get; } = new WeakReferenceMessenger();

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            AutofacBuilder();
            // 解析并使用LoginViewModel，例如设置为MainWindow的数据上下文  
            var loginWindow = new LoginWindow();
            loginWindow.DataContext = _container.Resolve<LoginViewModel>();
            loginWindow.Show();
        }

        private IRestClient CreateAndConfigureRestClient()
        {            
            var baseUrl = "http://localhost:5299/";
            //var baseUrl = "http://localhost:8080/";
            //var baseUrl = "http://192.168.1.158:8080/";
            App.Current.Properties[MessageToken.RestClientBaseUrl] = baseUrl;
            return new RestClient(baseUrl);
        }

        private void AutofacBuilder()
        {
            var builder = new ContainerBuilder();

            // 注册你的类型，例如：
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<UserService>().AsSelf();
            builder.RegisterType<SideMenuView>().AsSelf();
            builder.RegisterType<SideMenuViewModel>().AsSelf();
            builder.Register(c => CreateAndConfigureRestClient()).As<IRestClient>().SingleInstance();
            builder.RegisterType<SideMenuService>().As<ISideMenuService>().SingleInstance();
            builder.RegisterType<ProjectService>().SingleInstance();
            builder.RegisterType<ContractViewModel>().AsSelf();
            builder.RegisterType<ContractView>().AsSelf();
            builder.RegisterType<ContractNameTbViewModel>().AsSelf();
            builder.RegisterType<ContractNameTbService>().SingleInstance();
            builder.RegisterType<ContractNameTb>()
                .WithParameter(
                    (pi, ctx) => pi.Name == "vm",
                    (pi, ctx) => ctx.Resolve<ContractNameTbViewModel>()
                ).AsSelf();
            
            //Project Approval
            builder.RegisterType<ProjectApprovalService>().SingleInstance();
            builder.RegisterType<ProjectApprovalViewModel>().AsSelf();
            builder.RegisterType<ProjectApproval>()
                .WithParameter(
                    (pi, ctx) => pi.Name == "vm",
                    (pi, ctx) => ctx.Resolve<ProjectApprovalViewModel>()
                ).AsSelf();

            builder.RegisterType<DictService>().SingleInstance();
            builder.RegisterType<StaffService>().SingleInstance();

            builder.RegisterType<ProcessService>().SingleInstance();
            builder.RegisterType<ProcessViewModel>().WithParameter
                (
                    (pi, ctx) => pi.Name == "service",
                    (pi, ctx) => ctx.Resolve<ProcessService>()
                ).AsSelf();
            builder.RegisterType<ProjectProcessView>().AsSelf();


            builder.RegisterType<ProjectStaffSetViewModel>().AsSelf();
            builder.RegisterType<ProjectStaffUc>().AsSelf();

            builder.RegisterType<DeviceService>().SingleInstance();
            builder.RegisterType<ProjectDeviceViewModel>().AsSelf();

            builder.RegisterType<ProjectListViewModel>().AsSelf();
            builder.RegisterType<ProjectListView>().AsSelf();

            builder.RegisterType<ProjectSettingsViewModel>().AsSelf();

            builder.RegisterType<ProcessTemplateViewModel>().AsSelf();
            builder.RegisterType<ProcessTemplate>().WithParameter(
                (pi, ctx) => pi.Name == "vm",
                (pi, ctx) => ctx.Resolve<ProcessTemplateViewModel>()
                ).AsSelf();
            
            builder.RegisterType<ProcessTemplateListViewModel>().AsSelf();
            builder.RegisterType<ProcessTemplateListUc>().AsSelf();

            builder.RegisterType<DailyProcessService>().SingleInstance();
            builder.RegisterType<ProjectBonusService>().SingleInstance();
            builder.RegisterType<EarlyWarningService>().SingleInstance();
            builder.RegisterType<AttachmentsService>().SingleInstance();
            
            builder.RegisterType<ProjectDynamicsViewModel>().AsSelf();
            builder.RegisterType<ProjectDynamics>().AsSelf();

            builder.RegisterType<EarlyWarningHistory>().AsSelf();
            builder.RegisterType<EarlyWarningHistoryViewModel>().AsSelf();

            builder.RegisterType<ProjectAcceptanceListView>().AsSelf();
            builder.RegisterType<ProjectAcceptanceViewModel>().AsSelf();

            builder.RegisterType<AttachmentsView>().AsSelf();
            builder.RegisterType<AttachmentsViewModel>().AsSelf();

            builder.RegisterType<ProjectPaymentTermService>().SingleInstance();
            builder.RegisterType<ProjectPaymentTermViewModel>().AsSelf();
            
            builder.RegisterType<HandlingWarningViewModel>().AsSelf();
            builder.RegisterType<HandlingWarning>().AsSelf();

            builder.RegisterType<ProjectDailyWorkService>().SingleInstance();
            builder.RegisterType<ProjectDailyWorkList>().AsSelf();
            builder.RegisterType<ProjectDailyWorkViewModel>().AsSelf();

            builder.RegisterType<AddProjectDailyWork>().AsSelf();
            builder.RegisterType<AddProjectDailyWorkViewModel>().AsSelf();

            builder.RegisterType<DailyWorkSummaryViewModel>().AsSelf();
            builder.RegisterType<DailyWorkSummary>().AsSelf();

            builder.RegisterType<DeviceStockViewModel>().AsSelf();
            builder.RegisterType<DeviceStock>().AsSelf();

            builder.RegisterType<AddDeviceViewModel>().AsSelf();
            builder.RegisterType<AddDeviceView>().AsSelf();

            builder.RegisterType<DeviceTransferHistoryViewModel>().AsSelf();
            builder.RegisterType<DeviceTransferHistoryView>().AsSelf();

            builder.RegisterType<DeviceTypeListViewModel>().AsSelf();
            builder.RegisterType<DeviceTypeView>().AsSelf();

            builder.RegisterType<AddDeviceTypeViewModel>().AsSelf();
            builder.RegisterType<AddDeviceType>().AsSelf();

            builder.RegisterType<ConsumableService>().SingleInstance();
            builder.RegisterType<ConsumableTypeListViewModel>().AsSelf();
            builder.RegisterType<ConsumableTypeView>().AsSelf();

            builder.RegisterType<AddConsumableTypeViewModel>().AsSelf();
            builder.RegisterType<AddConsumableType>().AsSelf();

            builder.RegisterType<ConsumableListViewModel>().AsSelf();
            builder.RegisterType<ConsumableView>().AsSelf();

            builder.RegisterType<AddConsumableViewModel>().AsSelf();
            builder.RegisterType<AddConsumableView>().AsSelf();

            builder.RegisterType<StockInBoundViewModel>().AsSelf();
            builder.RegisterType<StockInBoundView>().AsSelf();

            builder.RegisterType<StockOutBoundViewModel>().AsSelf();
            builder.RegisterType<StockOutBoundView>().AsSelf();

            builder.RegisterType<ConsumableBoundHistoryViewModel>().AsSelf();
            builder.RegisterType<ConsumableBoundHistoryView>().AsSelf();

            builder.RegisterType<UpdateConsumableInBoundViewModel>().AsSelf();
            builder.RegisterType<UpdateConsumableOutBoundViewModel>().AsSelf();
            builder.RegisterType<UpdateConsumableBoundView>().AsSelf();
            
            builder.RegisterType<ProjectUpdateScheduleViewModel>().AsSelf();
            builder.RegisterType<ProjectUpdateScheduleView>().AsSelf();

            builder.RegisterType<AddProjectUpdateScheduleViewModel>().AsSelf();
            builder.RegisterType<AddProjectUpdateScheduleView>().AsSelf();
            builder.RegisterType<ProjectUpdateScheduleService>().SingleInstance();

            builder.RegisterType<ProjectUpdateScheduleHistoryViewModel>().AsSelf();
            builder.RegisterType<ProjectUpdateScheduleHistoryView>().AsSelf();

            builder.RegisterType<ProcessStaffRelatedViewModel>().AsSelf();
            builder.RegisterType<ProcessStaffRelatedView>().AsSelf();

            builder.RegisterType<AddBatchProjectDailyWorkViewModel>().AsSelf();
            builder.RegisterType<AddBatchProjectDailyWorkView>().AsSelf();

            builder.RegisterType<AddBatchDeviceViewModel>().AsSelf();
            builder.RegisterType<AddBatchDeviceView>().AsSelf();

            builder.RegisterType<AddDeviceHandleByViewModel>().AsSelf();
            builder.RegisterType<AddDeviceHandleBy>().AsSelf();

            builder.RegisterType<ApplicationService>().SingleInstance();
            builder.RegisterType<ApplicationViewModel>().AsSelf();
            builder.RegisterType<ApplicationView>().AsSelf();

            builder.RegisterType<ApplicationViewModel>().AsSelf();
            builder.RegisterType<ApplicationView>().AsSelf();

            builder.RegisterType<AddApplicationViewModel>().AsSelf();
            builder.RegisterType<AddApplicationView>().AsSelf();
            
            builder.RegisterType<ApplicationApproveViewModel>().AsSelf();
            builder.RegisterType<ApplicationApproveView>().AsSelf();

            builder.RegisterType<ApplicationHandleViewModel>().AsSelf();
            builder.RegisterType<ApplicationHandleView>().AsSelf();

            builder.RegisterType<HandlingApplicationViewModel>().AsSelf();
            builder.RegisterType<HandlingApplicationView>().AsSelf();

            builder.RegisterType<PatrolService>().SingleInstance();
            builder.RegisterType<PatrolViewModel>().AsSelf();
            builder.RegisterType<PatrolView>().AsSelf();

            builder.RegisterType<AddPatrolViewModel>().AsSelf();
            builder.RegisterType<AddPatrolView>().AsSelf();

            builder.RegisterType<AchievementBonusView>().AsSelf();
            builder.RegisterType<AchievementBonusViewModel>().AsSelf();
            
            builder.RegisterType<AchievementProjectBonusViewModel>().AsSelf();
            builder.RegisterType<AchievementProjectBonusView>().AsSelf();

            builder.RegisterType<ConsumableAskForService>().SingleInstance();
            builder.RegisterType<ConsumableAskForViewModel>().AsSelf();
            builder.RegisterType<ConsumableAskForView>().AsSelf();

            builder.RegisterType<AddConsumableAskForViewModel>().AsSelf();
            builder.RegisterType<AddConsumableAskForView>().AsSelf();

            builder.RegisterType<WorkAttendanceService>().SingleInstance();
            builder.RegisterType<WorkAttendanceViewModel>().AsSelf();
            builder.RegisterType<WorkAttendanceView>().AsSelf();

            builder.RegisterType<AddPlaceOnFileViewModel>().AsSelf();
            builder.RegisterType<AddPlaceOnFileView>().AsSelf();

            builder.RegisterType<PlaceOnFileViewModel>().AsSelf();
            builder.RegisterType<PlaceOnFileView>().AsSelf();

            _container = builder.Build();
            App.Current.Properties[MessageToken.AppContainer] = _container;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // 处理异常  
            HandyControl.Controls.MessageBox.Warning("发生未处理的异常：" + e.Exception.Message);
            e.Handled = true; // 设置为 true 表示异常已处理  fo
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // 处理异常  
            HandyControl.Controls.MessageBox.Warning("应用程序域级别未处理的异常：" + ((Exception)e.ExceptionObject).Message);
        }
        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            // 处理异常  
            HandyControl.Controls.MessageBox.Warning("未观察的任务异常：" + e.Exception.InnerException?.Message);
            e.SetObserved(); // 标记异常为已观察，防止应用程序崩溃  
        }
    }
}
