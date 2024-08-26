using CommunityToolkit.Mvvm.ComponentModel;

namespace ProjectViewModels
{
    public class CustomerVm : ObservableObject
    {
        private Guid _customerId;
        public Guid CustomerId
        {
            get => _customerId;
            set => SetProperty(ref _customerId, value);
        }

        private string _customerName = null!;
        public string CustomerName
        {
            get => _customerName;
            set => SetProperty(ref _customerName, value);
        }

        private string _custNo = null!;
        public string CustNo
        {
            get => _custNo;
            set => SetProperty(ref _custNo, value);
        }

        private string? _custAddress;
        public string? CustAddress
        {
            get => _custAddress;
            set => SetProperty(ref _custAddress, value);
        }
    }
}
