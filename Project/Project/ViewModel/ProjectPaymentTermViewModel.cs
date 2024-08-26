using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.Common;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public partial class ProjectPaymentTermViewModel : ObservableObject
    {
        private readonly ProjectPaymentTermService _projectPaymentTermService;
        private Guid? _projectId;
        
        private ObservableCollection<ProjectPaymentTermVm>? _paymentTermList;
        public ObservableCollection<ProjectPaymentTermVm>? PaymentTermList
        {
            get => _paymentTermList;
            set => SetProperty(ref _paymentTermList, value);
        }
        public ProjectPaymentTermViewModel(Guid? projectId,ObservableCollection<ProjectPaymentTermVm> projectPyamentTerms, ProjectPaymentTermService projectPaymentTermService)
        {
            _projectPaymentTermService = projectPaymentTermService;
            PaymentTermList = projectPyamentTerms;
            if (PaymentTermList == null||PaymentTermList.Count==0) AddTerm();
            _projectId = projectId;
            //Task.Run(LoadDataAsync);
        }

        //private async Task LoadDataAsync()
        //{
        //    //await LoadProjectPaymentTerms();
        //}

        private async Task LoadProjectPaymentTerms()
        {
            if(_projectId != null)
            {
                var list = await _projectPaymentTermService.GetProjectPaymentTermList(_projectId.Value);
                PaymentTermList = new ObservableCollection<ProjectPaymentTermVm>(list);
            }
            else
            {
                AddTerm();
            }        
        }
        [RelayCommand]
        private void RemoveTerm(ProjectPaymentTermVm vm)
        {
            if (PaymentTermList == null) return;
            var item = PaymentTermList.FirstOrDefault(m => m == vm);
            if (item != null)
            {
                PaymentTermList.Remove(item);
            }
        }
        [RelayCommand]
        private void AddTerm()
        {
            if (PaymentTermList == null) PaymentTermList = new ObservableCollection<ProjectPaymentTermVm>();
            PaymentTermList.Add(new ProjectPaymentTermVm
            {
                Remarks = string.Empty,
                WorkloadPercentage = 0
            });
        }
        [RelayCommand]
        private void RetrunProjectProcess()
        {
            foreach (var item in PaymentTermList)
            {
                if (item.WorkloadPercentage == 0)
                {
                    PaymentTermList.Remove(item);
                }
            }
            WeakReferenceMessenger.Default.Send(PaymentTermList, MessageToken.ReturnProjectPaymentTerms);
        }

    }
}
