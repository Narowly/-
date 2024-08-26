using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ProjectViewModels
{
    public class ContractVm : ObservableObject
    {
        private Guid _contractId;
        public Guid ContractId
        {
            get => _contractId;
            set => SetProperty(ref _contractId, value);
        }

        private string _contractNumber = null!;
        public string ContractNumber
        {
            get => _contractNumber;
            set => SetProperty(ref _contractNumber, value);
        }

        private string _contractName = null!;
        public string ContractName
        {
            get => _contractName;
            set => SetProperty(ref _contractName, value);
        }

        private decimal _contractAmount;
        public decimal ContractAmount
        {
            get => _contractAmount;
            set => SetProperty(ref _contractAmount, value);
        }

        private decimal _contractPayAmount;
        public decimal ContractPayAmount
        {
            get => _contractPayAmount;
            set => SetProperty(ref _contractPayAmount, value);
        }

        private DateTime _contractStartDate;
        public DateTime ContractStartDate
        {
            get => _contractStartDate;
            set => SetProperty(ref _contractStartDate, value);
        }

        private DateTime _contractEndDate;
        public DateTime ContractEndDate
        {
            get => _contractEndDate;
            set => SetProperty(ref _contractEndDate, value);
        }

        private CustomerVm? _customer;
        public CustomerVm? Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }

        private CustomerContactVm? _customerContact;
        public CustomerContactVm? CustomerContact
        {
            get => _customerContact;
            set => SetProperty(ref _customerContact, value);
        }

        private Guid? _customerId;
        public Guid? CustomerId
        {
            get => _customerId;
            set => SetProperty(ref _customerId, value);
        }

        private Guid? _salesManagerId;
        public Guid? SalesManagerId
        {
            get => _salesManagerId;
            set => SetProperty(ref _salesManagerId, value);
        }

        private StaffVm? _salesManager;
        public StaffVm? SalesManager
        {
            get => _salesManager;
            set => SetProperty(ref _salesManager, value);
        }
    }
}