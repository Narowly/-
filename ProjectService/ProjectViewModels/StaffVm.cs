using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ProjectViewModels
{
    public class StaffVm : ObservableObject
    {
        private Guid _staffId;
        public Guid StaffId
        {
            get => _staffId;
            set => SetProperty(ref _staffId, value);
        }

        private string _staffCode = null!;
        public string StaffCode
        {
            get => _staffCode;
            set => SetProperty(ref _staffCode, value);
        }

        private string _staffName = null!;
        public string StaffName
        {
            get => _staffName;
            set => SetProperty(ref _staffName, value);
        }

        private string? _staffPhone;
        public string? StaffPhone
        {
            get => _staffPhone;
            set => SetProperty(ref _staffPhone, value);
        }

        private string? _staffCard;
        public string? StaffCard
        {
            get => _staffCard;
            set => SetProperty(ref _staffCard, value);
        }

        private int _staffDuty;
        public int StaffDuty
        {
            get => _staffDuty;
            set => SetProperty(ref _staffDuty, value);
        }

        private int? _staffStatus;
        public int? StaffStatus
        {
            get => _staffStatus;
            set => SetProperty(ref _staffStatus, value);
        }

        private decimal _staffFees;
        public decimal StaffFees
        {
            get => _staffFees;
            set => SetProperty(ref _staffFees, value);
        }

        private int? _staffDepartment;
        public int? StaffDepartment
        {
            get => _staffDepartment;
            set => SetProperty(ref _staffDepartment, value);
        }

        private decimal? _staffWages;
        public decimal? StaffWages
        {
            get => _staffWages;
            set => SetProperty(ref _staffWages, value);
        }

        private decimal? _staffzzWages;
        public decimal? StaffzzWages
        {
            get => _staffzzWages;
            set => SetProperty(ref _staffzzWages, value);
        }

        private decimal? _staffInsuranceAmount;
        public decimal? StaffInsuranceAmount
        {
            get => _staffInsuranceAmount;
            set => SetProperty(ref _staffInsuranceAmount, value);
        }

        private decimal? _staffSubsidy;
        public decimal? StaffSubsidy
        {
            get => _staffSubsidy;
            set => SetProperty(ref _staffSubsidy, value);
        }

        private int? _staffGiveMoneyType;
        public int? StaffGiveMoneyType
        {
            get => _staffGiveMoneyType;
            set => SetProperty(ref _staffGiveMoneyType, value);
        }

        private int? _staffRecode;
        public int? StaffRecode
        {
            get => _staffRecode;
            set => SetProperty(ref _staffRecode, value);
        }

        private string? _inProjectName;
        public string? InProjectName
        {
            get => _inProjectName;
            set => SetProperty(ref _inProjectName, value);
        }
    }
}