using CommunityToolkit.Mvvm.ComponentModel;
namespace ProjectViewModels
{
    public class CustomerContactVm : ObservableObject
    {
        private Guid _customerContactId;
        public Guid CustomerContactId
        {
            get => _customerContactId;
            set => SetProperty(ref _customerContactId, value);
        }

        private string _contactName = null!;
        public string ContactName
        {
            get => _contactName;
            set => SetProperty(ref _contactName, value);
        }

        private string? _mobile;
        public string? Mobile
        {
            get => _mobile;
            set => SetProperty(ref _mobile, value);
        }
    }
}
