using ProjectService.Db;
using ProjectViewModels;
using System.Collections.ObjectModel;
using System.Drawing;
using static PaginationExtensions;
namespace ProjectService.ViewModels
{
    public static class ViewModelExtensions
    {
        public static PaginatedList<TViewModel> ToViewModelPaginatedList<T, TViewModel>(this PaginatedList<T> source, Func<T, TViewModel> selector)
        {
            List<TViewModel> items = source.Items.Select(selector).ToList();
            return new PaginatedList<TViewModel>(items, source.TotalCount, source.PageIndex, source.PageSize);
        }

        public static StaffVm ToViewModel(this Staff staff)
        {
            var viewModel = new StaffVm
            {
                StaffId = staff.StaffId,
                StaffCode = staff.StaffCode,
                StaffName = staff.StaffName,
                StaffPhone = staff.StaffPhone,
                StaffCard = staff.StaffCard,
                StaffDuty = staff.StaffDuty,
                StaffStatus = staff.StaffStatus,
                StaffFees = staff.StaffFees,
                StaffDepartment = staff.StaffDepartment,
                StaffWages = staff.StaffWages,
                StaffzzWages = staff.StaffWages,
                StaffInsuranceAmount = staff.StaffInsuranceAmount,
                StaffSubsidy = staff.StaffSubsidy,
                StaffGiveMoneyType = staff.StaffGiveMoneyType,
                StaffRecode = staff.StaffRecode
            };
            return viewModel;
        }

        public static ContractVm ToViewModel(this Contract contract)
        {
            var viewModel = new ContractVm
            {
                ContractId = contract.ContractId,
                ContractNumber = contract.ContractNumber,
                ContractName = contract.ContractName,
                ContractAmount = contract.ContractAmount,
                ContractPayAmount = contract.ContractPayAmount,
                ContractStartDate = contract.ContractStartDate,
                ContractEndDate = contract.ContractEndDate,
                CustomerId = contract.CustomerId,
            };
            if (contract.Customer != null)
            {
                viewModel.Customer = contract.Customer.ToViewModel();
            }
            if (contract.CustomerContact != null)
            {
                viewModel.CustomerContact = contract.CustomerContact.ToViewModel();
            }
            if(contract.SalesManager!=null)
                viewModel.SalesManager = contract.SalesManager.ToViewModel();
            return viewModel;
        }

        public static DictDataVm ToViewModel(this DictDatum data)
        {
            var vm = new DictDataVm
            {
                DictCode = data.DictCode,
                DictLabel = data.DictLabel,
                DictTypeId = data.DictTypeId,
                DictValue = data.DictValue,
                ParentCode = data.ParentCode,
                Status = data.Status
            };
            if (vm.ParentCode != null)
            {
                vm.ParentDataVm = data.DictType.DictData.FirstOrDefault(m => m.DictCode == vm.ParentCode)?.ToViewModel();
            }
            
            return vm;
        }
        public static ProjectVm ToViewModel(this Project project,bool? includeProjectProcess = false)
        {
            var vm = new ProjectVm
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ContractId = project.ContractId,
                StartDate = project.StartDate,
                CreateTime = project.CreateTime,
                Status = project.Status,
                Address = project.Address,
                PlanEndDate = project.PlanEndDate,
                PlanPersonDays = project.PlanPersonDays,
                RegionId = project.RegionId,
                Contract = project.Contract.ToViewModel(),
                ProjectManager = project.ProjectManager.ToViewModel(),
                SalesManager = project.SalesManager.ToViewModel(),
                AcceptanceDate = project.AcceptanceDate
            };
            if (project.ProjectProcesses != null)
            {
                vm.ProjectProcesses = new ObservableCollection<ProjectProcessVm>();
                foreach (var process in project.ProjectProcesses)
                {
                    var processVm = new ProjectProcessVm
                    {
                        Id = process.Id,
                        ProjectId = process.ProjectId,
                        ProcessUnitId = process.ProcessUnitId,
                        Weight = process.Weight,
                        Sequence = process.Sequence,
                        Workload = process.Workload,
                        StartingWorkload = process.StartingWorkload,
                        Remarks = process.Remarks,                        
                    };
                    if (process.ProcessUnit != null) processVm.ProcessUnit = process.ProcessUnit.ToViewModel();
                    var addProcessList = new List<Guid>();
                    if (includeProjectProcess == true && process.ProjectDailyWorks != null)
                    {                        
                        processVm.ProjectDailyWorks = new ObservableCollection<ProjectDailyWorkVm>(process.ProjectDailyWorks.Select(m => m.ToViewModel()));
                    }
                    vm.ProjectProcesses.Add(processVm);
                    
                }
            }
            if (project.ProjectStaffs != null)
            {
                vm.InProjectStaffs = new ObservableCollection<StaffVm>();
                foreach (var staff in project.ProjectStaffs)
                {
                    if (staff.Staff != null&&staff.TransferOutDate==null)
                        vm.InProjectStaffs.Add(staff.Staff.ToViewModel());
                }
            }
            if (project.ProjectDevices != null)
            {
                vm.InProjectDevice = new ObservableCollection<DeviceVm>();
                foreach(var device in project.ProjectDevices)
                {
                    if(device.Device != null&&device.TransferOutDate==null)
                        vm.InProjectDevice.Add(device.Device.ToViewModel());
                }
            }
            if(project.ProjectPaymentTerms != null)
            {
                vm.ProjectPyamentTerms = new ObservableCollection<ProjectPaymentTermVm>();
                foreach (var term in project.ProjectPaymentTerms)
                {
                    vm.ProjectPyamentTerms.Add(term.ToViewModel());
                }
            }
            
            return vm;
        }
        public static ProjectVm ToSimpleViewModel(this Project project)
        {
            var vm = new ProjectVm
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                //ContractId = project.ContractId,
                StartDate = project.StartDate,
                //CreateTime = project.CreateTime,
                //Status = project.Status,
                //Address = project.Address,
                PlanEndDate = project.PlanEndDate,
                //PlanPersonDays = project.PlanPersonDays,
                //RegionId = project.RegionId,
                //Contract = project.Contract.ToViewModel(),
                ProjectManager = project.ProjectManager.ToViewModel(),
                SalesManager = project.SalesManager.ToViewModel(),
                LastUpdateScheduleReason = project.ProjectUpdateSchedules?.OrderByDescending(m => m.CreateTime).FirstOrDefault()?.Remarks
                //AcceptanceDate = project.AcceptanceDate
            };
            return vm;
        }
        public static ProjectProcessVm ToViewModel(this ProjectProcess projectProcess, bool includeModels = false)
        {
            var vm = new ProjectProcessVm
            {
                Id = projectProcess.Id,
                ProcessUnit = projectProcess.ProcessUnit.ToViewModel(),
                ProcessUnitId = projectProcess.ProcessUnit.Id,
                ProjectId = projectProcess.ProjectId,
                Remarks = projectProcess.Remarks,
                Sequence = projectProcess.Sequence,
                StartingWorkload = projectProcess.StartingWorkload,
                Weight = projectProcess.Weight,
                Workload = projectProcess.Workload
            };
            if (includeModels)
            {
                vm.Project = projectProcess.Project.ToSimpleViewModel();
            }
            return vm;
        }

        public static DictTypeVm ToViewModel(this DictType dictType)
        {
            return new DictTypeVm
            {
                DictId = dictType.DictId,
                DictName = dictType.DictName,
                TypeName = dictType.TypeName,
                Status = dictType.Status,
                Remarks = dictType.Remarks
            };
        }

        public static CustomerContactVm ToViewModel(this CustomerContact customerContact)
        {
            return new CustomerContactVm
            {
                ContactName = customerContact.ContactName,
                CustomerContactId = customerContact.CustomerContactId,
                Mobile = customerContact.Mobile
            };
        }

        public static CustomerVm ToViewModel(this Customer customer)
        {
            return new CustomerVm
            {
                CustomerId = customer.CustomerId,
                CustAddress = customer.CustAddress,
                CustNo = customer.CustNo,
                CustomerName = customer.CustomerName
            };
        }

        public static ProcessVm ToViewModel(this Process p)
        {
            var vm = new ProcessVm
            {
                ProcessId = p.ProcessId,
                ProcessName = p.ProcessName,                 
                Remarks = p.Remarks
            };
            return vm;
        }
        public static ProcessUnitVm ToViewModel(this ProcessUnit p)
        {
            var vm = new ProcessUnitVm
            {
                Id = p.Id,
                ProcessId = p.ProcessId,
                Remarks = p.Remarks,
                UnitId = p.UnitId
            };
            vm.Process = p.Process.ToViewModel();
            vm.ProUnit = p.Unit.ToViewModel();
            return vm;
        }
        public static ProUnitVm ToViewModel(this ProUnit p)
        {
            var vm = new ProUnitVm
            {
                UnitId = p.UnitId,
                UnitName = p.UnitName,
                Remarks = p.Remarks
            };
            return vm;
        }
        
        public static DeviceTypeVm ToViewModel(this  DeviceType d)
        {
            return new DeviceTypeVm
            {
                DeviceModel = d.DeviceModel,
                DeviceTypeId = d.DeviceTypeId,
                DeviceTypeName = d.DeviceTypeName,
                DeviceUnit = d.DeviceUnit,
                Remarks = d.Remarks
            };
        }

        public static DeviceVm ToViewModel(this Device d)
        {
            var vm = new DeviceVm
            {
                DeviceId = d.DeviceId,
                DeviceNumber = d.DeviceNumber,
                DeviceStatus = d.DeviceStatus,
                DeviceTypeId = d.DeviceTypeId,
                Remarks = d.Remarks,
                DeviceType = d.DeviceType.ToViewModel()                
            };
            vm.DeviceWithProjectName = $"{vm.DeviceNumber} {vm.DeviceType.DeviceTypeName} {vm.DeviceType.DeviceModel}";
            return vm;
        }

        public static DeviceStockVm ToStockViewModel(this Device d)
        {
            var vm = new DeviceStockVm
            {
                Device = d.ToViewModel()
            };
            var projectDevice = d.ProjectDevices.FirstOrDefault(m => m.TransferOutDate == null);
            if (projectDevice != null)
            {
                vm.Project = projectDevice.Project.ToSimpleViewModel();
            }
            return vm;
        }

        public static ProcessTemplateVm ToViewModel(this ProcessTemplate p)
        {
            var template = new ProcessTemplateVm
            {
                Id = p.Id,
                Name = p.Name,
                Remarks = p.Remarks,
                ProcessTemplateDetails = new ObservableCollection<ProcessTemplateDetailVm>()
            };
            foreach (var detail in p.ProcessTemplateDetails)
            {
                template.ProcessTemplateDetails.Add(new ProcessTemplateDetailVm
                {
                    Id = detail.Id,
                    ProcessUnitId = detail.ProcessUnitId,
                    Remarks = detail.Remarks,
                    Sequence = detail.Sequence,
                    TemplateId = detail.TemplateId,
                    Weight = detail.Weight,
                    ProcessUnit = detail.ProcessUnit.ToViewModel()
                });
            }
            return template;
        }
        public static ConsumableTypeVm ToViewModel(this ConsumableType c)
        {
            return new ConsumableTypeVm
            {
                ConsumableTypeName = c.ConsumableTypeName,
                ConsumableModel = c.ConsumableModel,
                ConsumableUnit = c.ConsumableUnit,
                ConsumableTypeId = c.ConsumableTypeId,
                Remarks = c.Remarks
            };
        }
        public static ProjectDailyProcessVm ToViewModel(this ProjectDailyProcess p)
        {
            var vm = new ProjectDailyProcessVm
            {
                Id = p.Id,
                DailyWorkload = p.DailyWorkload,
                ProjectProcessId = p.ProjectProcessId,
                ProjectProcess = p.ProjectProcess.ToViewModel(),
                Remarks = p.Remarks,
                StartDate = p.StartDate
            };
            if (vm.Id == 0) vm.Id = null;
            return vm;
        }

        public static ProjectBonusVm ToViewModel(this ProjectBonu p)
        {
            var vm = new ProjectBonusVm
            {
                Id = p.Id,
                Bonus = p.Bonus,
                ProjectProcess = p.ProjectProcess.ToViewModel(),
                ProjectProcessId = p.ProjectProcessId,
                Remarks = p.Remarks,
                Workload = p.Workload
            };
            return vm;
        }

        public static ProjectEarlyWarningVm ToViewModel(this ProjectEarlyWarning w)
        {
            return new ProjectEarlyWarningVm
            {
                Id = w.Id,
                ProjectId = w.ProjectId,
                WarningType = w.WarningType,
                WarningValue = w.WarningValue
            };
        }

        public static ProjectAttachmentVm ToViewModel(this ProjectAttachment a)
        {
            return new ProjectAttachmentVm
            {
                Id = a.Id,
                FileName = a.FileName,
                FileAddress = a.FileAddress,
                FileType = a.FileType,
                ProjectId = a.ProjectId,
                UploadDate = a.UploadDate,
                Project = a.Project?.ToViewModel()
            };
        }

        public static  ProjectDailyWorkVm ToViewModel(this ProjectDailyWork w, bool includeModels = false)
        {
            var vm = new ProjectDailyWorkVm
            {
                Id = w.Id,
                ProjectProcessId = w.ProjectProcessId,
                StaffId = w.StaffId,
                Workload = w.Workload,
                BillDate = w.BillDate,
                Remarks = w.Remarks
            };
            var dailyProcessStandard = w.ProjectProcess.ProjectDailyProcesses.Where(m => DateOnly.FromDateTime(m.StartDate) < vm.BillDate).OrderByDescending(o => o.StartDate).FirstOrDefault();
            vm.DailyWorkloadStandard = dailyProcessStandard?.DailyWorkload;
            if (includeModels)
            {
                vm.Staff = w.Staff.ToViewModel();
                vm.ProjectProcess= w.ProjectProcess?.ToViewModel(true);
            }
            return vm;
        }

        public static EarlyWarningHistoryVm ToViewModel(this EarlyWarningHistory w, List<DictDatum> warningsType, List<DictDatum> statusList)
        {
            return new EarlyWarningHistoryVm
            {
                Id = w.Id,
                Project = w.Project.ToViewModel(),
                ProjectId = w.ProjectId,
                WarningMessage = w.WarningMessages,
                WarningType = w.WarningType,
                WarningValue = w.WarningValue,
                WarningTypeData = warningsType.FirstOrDefault(m => m.DictCode == w.WarningType)?.ToViewModel(),
                Status = w.Status,
                StatusData = statusList.FirstOrDefault(m => m.DictCode == w.Status)?.ToViewModel(),
                ManagerReason = w.ManagerReason,
                StaffReason = w.StaffReason,
                Suggestions = w.Suggestions,
                CreateTime = w.CreateTime
            };
        }

        public static ProjectPaymentTermVm ToViewModel(this ProjectPaymentTerm t)
        {
            return new ProjectPaymentTermVm
            {
                PaymentTermsId = t.PaymentTermsId,
                ProjectId = t.ProjectId,
                Remarks = t.Remarks,
                WorkloadPercentage = t.WorkloadPercentage
            };
        }

        public static ProjectBonusExVm ToViewModel(this ProjectBonusEx b)
        {
            return new ProjectBonusExVm
            {
                Id = b.Id,
                Bonus = b.Bonus,
                ProjectId = b.ProjectId,
                Penalty = b.Penalty,
                PlanPersonDays = b.PlanPersonDays,
                Rewards = b.Rewards
            };
        }

        public static ProjectDeviceVm ToViewModel(this ProjectDevice device)
        {
            var vm = new ProjectDeviceVm
            {
                AssociationId = device.AssociationId,
                DeviceId = device.DeviceId,
                ProjectId = device.ProjectId,
                Remarks = device.Remarks,
                TransferInDate = device.TransferInDate,
                TransferOutDate = device.TransferOutDate,
                TransferInOperator = device.TransferInOperator,
                TransferOutOperator = device.TransferOutOperator,
                Device = device.Device.ToViewModel(),
                Project = device.Project.ToSimpleViewModel(),
                HandleByName = device.HandleByNavigation?.StaffName
            };
            return vm;
        }

        public static ConsumableVm ToViewModel(this Consumable consumable)
        {
            return new ConsumableVm
            {
                ConsumableId = consumable.ConsumableId,
                ConsumableNumber = consumable.ConsumableNumber,
                ConsumableTypeId = consumable.ConsumableTypeId,
                ConsumableStatus = consumable.ConsumableStatus,
                Quantity = consumable.Quantity,
                Price = consumable.Price ?? 0,
                Remarks = consumable.Remarks,
                ConsumableType = consumable.ConsumableType.ToViewModel()
            };
        }

        public static StockInBoundVm ToViewModel(this  StockInBound s)
        {
            return new StockInBoundVm
            {
                InBoundId = s.InBoundId,
                InBoundDate = s.InBoundDate,
                ConsumableId = s.ConsumableId,
                ProjectId = s.ProjectId,
                Quantity = s.Quantity,
                Remarks = s.Remarks,
                Consumable = s.Consumable.ToViewModel(),
                Project = s.Project?.ToSimpleViewModel()
            };
        }
        public static StockOutBoundVm ToViewModel(this StockOutBound s)
        {
            return new StockOutBoundVm
            {
                OutBoundId = s.OutBoundId,
                OutBoundDate = s.OutBoundDate,
                ProjectId = s.ProjectId,
                Quantity = s.Quantity,
                Remarks = s.Remarks,
                ConsumableId = s.ConsumableId,
                Consumable = s.Consumable.ToViewModel(),
                Project = s.Project.ToSimpleViewModel()
            };
        }
        public static ProjectUpdateScheduleVm ToViewModel(this ProjectUpdateSchedule s)
        {
            return new ProjectUpdateScheduleVm
            {
                Id = s.Id,
                ProjectId = s.ProjectId,
                ProjectName = s.Project.ProjectName,
                PlanEndDate = s.PlanEndDate,
                UpdateEndDate = s.UpdatedEndDate,
                ReasonType = s.ReasonType,
                Remarks = s.Remarks
            };
        }
        public static ApplicationVm ToViewModel(this Application a, bool includeProject = false)
        {
            var vm = new ApplicationVm
            {
                ApplicationId = a.ApplicationId,
                ProjectId = a.ProjectId,
                ApplicationType = a.ApplicationType,
                ApplicationUser = a.ApplicationUser,
                ApplicationTitle = a.ApplicationTitle,
                ApplicationContent = a.ApplicationContent,
                ApplicationItemCount = a.ApplicationItemCount,
                ApplicationDelivery = a.ApplicationDelivery,
                ApplicationTime = a.ApplicationTime,
                IsDeleted = a.IsDeleted,
                ApplicationStatus = a.ApplicationStatus,
                ApplicationResContent = a.ApplicationResContent,
                ApplicationResTime = a.ApplicationResTime,
                Remarks = a.Remarks,
                ApplicationUserName = a.ApplicationUserNavigation.StaffName,
                ConsumableList = a.ApplicationConsumables.Select(m => m.ToViewModel()).ToList(),
                DeviceList = a.ApplicationDevices.Select(m => m.ToViewModel()).ToList(),
                PersonList = a.ApplicationPeople.Select(m => m.ToViewModel()).ToList(),
                ProjectName = a.Project.ProjectName
            };
            if (includeProject)
            {
                vm.Project = a.Project.ToViewModel();   
            }
            return vm;
        }
        public static ApplicationConsumableVm ToViewModel(this ApplicationConsumable c)
        {
            return new ApplicationConsumableVm
            {
                ApplicationId = c.ApplicationId,
                ApplicationConsumableId = c.ApplicationConsumableId,
                ConsumableTypeId = c.ConsumableTypeId,
                ConsumableModel = c.ConsumableType?.ConsumableModel,
                ConsumableTypeName = c.ConsumableType?.ConsumableTypeName,
                Quantity = c.Quantity,
                Remarks = c.Remarks
            };
        }
        public static ApplicationDeviceVm ToViewModel(this ApplicationDevice d)
        {
            return new ApplicationDeviceVm
            {
                ApplicationId = d.ApplicationId,
                ApplicationDeviceId = d.ApplicationDeviceId,
                DeviceTypeId = d.DeviceTypeId,
                DeviceModel = d.DeviceType?.DeviceModel,
                DeviceTypeName = d.DeviceType?.DeviceTypeName,
                Quantity = d.Quantity,
                Remarks = d.Remarks
            };
        }
        public static ApplicationPersonVm ToViewModel(this ApplicationPerson p)
        {
            return new ApplicationPersonVm
            {
                ApplicationId = p.ApplicationId,
                ApplicationPersonId = p.ApplicationPersonId,
                ProcessId = p.ProcessId,
                ProcessName = p.Process?.ProcessName,
                Count = p.Count,
                Remarks = p.Remarks
            };
        }
        public static ProjectPatrolVm ToViewModel(this ProjectPatrol p)
        {
            return new ProjectPatrolVm
            {
                Id = p.Id,
                PatrolDate = p.PatrolDate,
                Project = p.Project.ToSimpleViewModel(),
                ProjectId = p.ProjectId,
                Remarks = p.Remarks,
                Staff = p.Staff.ToViewModel(),
                StaffId = p.StaffId,
                Status = p.Status
            };
        }

        public static ConsumableAskForItemVm ToViewModel(this ConsumableAskForItem c)
        {
            return new ConsumableAskForItemVm
            {
                ConsumableAskForId = c.ConsumableAskForId,
                ConsumableAskForItemId = c.ConsumableAskForItemId,
                //ConsumableModel = c.ConsumableType.ConsumableModel,
                //ConsumableTypeName = c.ConsumableType.ConsumableTypeName,
                ConsumableTypeId = c.ConsumableTypeId,
                Quantity = c.Quantity,
                ConsumableTypeShowName = $"{c.ConsumableType.ConsumableTypeName} {c.ConsumableType.ConsumableModel} ({c.ConsumableType.ConsumableUnit})"
            };
        }

        public static ConsumableAskForVm ToViewModel(this ConsumableAskFor c)
        {
            return new ConsumableAskForVm
            {
                ConsumableAskForId = c.ConsumableAskForId,
                ConsumableAskForItemList = c.ConsumableAskForItems.Select(m => m.ToViewModel()).ToList(),
                Content = c.Content,
                ProjectId = c.ProjectId,
                ProjectName = c.Project?.ProjectName,
                StaffId = c.StaffId,
                StaffName = c.Staff.StaffName,
                Status = c.Status,
                Title = c.Title,
                CreateTime = c.CreateTime
            };
        }

        public static WorkAttendanceVm ToViewModel(this WorkAttendance w)
        {
            return new WorkAttendanceVm
            {
                ProjectName = w.ProjectName,
                StaffName = w.StaffName,
                StaffId = w.StaffId,
                ClockIn01 = w.ClockIn01,
                ClockIn02 = w.ClockIn02,
                ClockIn03 = w.ClockIn03,
                ClockIn04 = w.ClockIn04,
                ClockIn05 = w.ClockIn05,
                ClockIn06 = w.ClockIn06,
                ClockIn07 = w.ClockIn07,
                ClockIn08 = w.ClockIn08,
                ClockIn09 = w.ClockIn09,
                ClockIn10 = w.ClockIn10,
                ClockIn11 = w.ClockIn11,
                ClockIn12 = w.ClockIn12,
                ClockIn13 = w.ClockIn13,
                ClockIn14 = w.ClockIn14,
                ClockIn15 = w.ClockIn15,
                ClockIn16 = w.ClockIn16,
                ClockIn17 = w.ClockIn17,
                ClockIn18 = w.ClockIn18,
                ClockIn19 = w.ClockIn19,
                ClockIn20 = w.ClockIn20,
                ClockIn21 = w.ClockIn21,
                ClockIn22 = w.ClockIn22,
                ClockIn23 = w.ClockIn23,
                ClockIn24 = w.ClockIn24,
                ClockIn25 = w.ClockIn25,
                ClockIn26 = w.ClockIn26,
                ClockIn27 = w.ClockIn27,
                ClockIn28 = w.ClockIn28,
                ClockIn29 = w.ClockIn29,
                ClockIn30 = w.ClockIn30,
                ClockIn31 = w.ClockIn31,
                ClockOut01 = w.ClockOut01,
                ClockOut02 = w.ClockOut02,
                ClockOut03 = w.ClockOut03,
                ClockOut04 = w.ClockOut04,
                ClockOut05 = w.ClockOut05,
                ClockOut06 = w.ClockOut06,
                ClockOut07 = w.ClockOut07,
                ClockOut08 = w.ClockOut08,
                ClockOut09 = w.ClockOut09,
                ClockOut10 = w.ClockOut10,
                ClockOut11 = w.ClockOut11,
                ClockOut12 = w.ClockOut12,
                ClockOut13 = w.ClockOut13,
                ClockOut14 = w.ClockOut14,
                ClockOut15 = w.ClockOut15,
                ClockOut16 = w.ClockOut16,
                ClockOut17 = w.ClockOut17,
                ClockOut18 = w.ClockOut18,
                ClockOut19 = w.ClockOut19,
                ClockOut20 = w.ClockOut20,
                ClockOut21 = w.ClockOut21,
                ClockOut22 = w.ClockOut22,
                ClockOut23 = w.ClockOut23,
                ClockOut24 = w.ClockOut24,
                ClockOut25 = w.ClockOut25,
                ClockOut26 = w.ClockOut26,
                ClockOut27 = w.ClockOut27,
                ClockOut28 = w.ClockOut28,
                ClockOut29 = w.ClockOut29,
                ClockOut30 = w.ClockOut30,
                ClockOut31 = w.ClockOut31,
                WorkYearMonth = w.WorkYearMonth
            };
        }

        public static ProcessStaffRelatedVm ToViewModel(this ProjectProcessStaffRelated r)
        {
            return new ProcessStaffRelatedVm
            {
                ProcessUnitId = r.ProcessUnitId,
                ProjectId = r.ProjectId,
                RelatedId = r.RelatedId,
                StaffId = r.StaffId
            };
        }

        public static AttachmentRequirementVm ToViewModel(this AttachmentRequirement r)
        {
            return new AttachmentRequirementVm
            {
                AttachmentRequirementId = r.AttachmentRequirementId,
                AttachmentName = r.AttachmentName
            };
        }

        public static PlaceOnFileVm ToViewModel(this PlaceOnFile p)
        {
            var vm = new PlaceOnFileVm
            {
                ProjectId = p.ProjectId,
                Project = p.Project.ToSimpleViewModel(),
                Reason = p.Reason,
                ApplicationUserId = p.ApplicationUserId,
                ReviewerId = p.ReviewerId,
                CreateTime = p.CreateTime
            };
            return vm;
        }
    }
}
