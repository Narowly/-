using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProjectPaymentTermVm : ObservableObject
    {
        private long? _paymentTermsId;
        public long? PaymentTermsId
        {
            get => _paymentTermsId; set => SetProperty(ref _paymentTermsId, value);
        }

        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId; set => SetProperty(ref _projectId, value);
        }

        private double _workloadPercentage;
        public double WorkloadPercentage
        {
            get => _workloadPercentage; set => SetProperty(ref _workloadPercentage, value);
        }

        private string? _remarks;
        public string? Remarks
        {
            get => _remarks; set => SetProperty(ref _remarks, value);
        }
    }
}
