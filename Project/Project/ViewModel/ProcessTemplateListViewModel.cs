using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Project.Services.DataServices;
using Project.Views.Windows;
using ProjectViewModels;
using Autofac;
using Project.Common;
using Project.Views.UserControls;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Project.ViewModel
{
    public partial class ProcessTemplateListViewModel : ObservableObject
    {
        private readonly IContainer _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private ProcessService _processService;
        [ObservableProperty]
        public ObservableCollection<ProcessTemplateVm> processTemplateList = null!;
        [ObservableProperty]
        public ProcessTemplateVm? selectedTemplate;
        private PopWindow? ProcessTemplateWindow;

        public ProcessTemplateListViewModel(ProcessService processService)
        {
            _processService = processService;
            Task.Run(LoadDataAsync);
            RegisterMessage();
        }

        private void RegisterMessage()
        {
            WeakReferenceMessenger.Default.Register(this, MessageToken.CloseProcessTemplate, (MessageHandler<object, string>)(async (obj, m) =>
            {
                if (ProcessTemplateWindow != null) ProcessTemplateWindow.Close();
                await LoadProcessTemplates();
            }));
        }
        private async Task LoadDataAsync()
        {
            await LoadProcessTemplates();
        }
        private async Task LoadProcessTemplates()
        {
            var templateList = await _processService.GetProcessTemplateList();
            ProcessTemplateList = new ObservableCollection<ProcessTemplateVm>(templateList);
        }
        [RelayCommand]
        private void OpenProcessTemplateWindow()
        {
            ProcessTemplateWindow = new PopWindow();
            var processTemplate = new ResolvedParameter(
                (pi, ctx) => pi.Name == "processTemplate",
                (pi, ctx) => SelectedTemplate);
            var vm = _container.Resolve<ProcessTemplateViewModel>(processTemplate);
            var view = new ProcessTemplate(vm);
            ProcessTemplateWindow.controlHost.Content = view;
            ProcessTemplateWindow.Show();
        }

    }
}
