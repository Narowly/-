using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProcessTemplateVm : ObservableObject
    {
        private int? _id;
        public int? Id
        {
            get => _id; set => SetProperty(ref _id, value);
        }
        private string _name = null!;
        public string Name
        {
            get => _name; set => SetProperty(ref _name, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
        private ObservableCollection<ProcessTemplateDetailVm>? _processTemplateDetails;
        public ObservableCollection<ProcessTemplateDetailVm>? ProcessTemplateDetails
        {
            get => _processTemplateDetails;
            set => SetProperty(ref _processTemplateDetails, value);
        }
    }
    public class ProcessTemplateDetailVm : ObservableObject
    {
        private int? _id;
        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private int? _templateId;
        public int? TemplateId
        {
            get => _templateId;
            set => SetProperty(ref _templateId, value);
        }
        private int _processUnitId;
        public int ProcessUnitId
        {
            get => _processUnitId;
            set => SetProperty(ref _processUnitId, value);
        }
        private decimal _weight;
        public decimal Weight
        {
            get=> _weight; set => SetProperty(ref _weight, value);
        }
        private int _sequence;
        public int Sequence
        {
            get => _sequence; set => SetProperty(ref _sequence, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
        private ProcessUnitVm? _processUnit = null!;
        public ProcessUnitVm? ProcessUnit
        {
            get => _processUnit;
            set => SetProperty(ref _processUnit, value);
        }

        
    }
}
