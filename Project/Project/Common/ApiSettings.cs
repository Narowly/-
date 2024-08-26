using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace Project.Common
{
    internal static class ApiSettings
    {
        #region 接口地址
        //控制器
        private static string userController = "api/user/";
        private static string projectController = "api/project/";
        private static string staffController = "api/staff/";
        private static string dictController = "api/dict/";
        private static string processController = "api/process/";
        private static string deviceController = "api/device/";
        private static string projectDailyProcessController = "api/projectDailyProcess/";
        private static string projectBonusController = "api/projectBonus/";
        private static string earlyWarningController = "api/earlyWarning/";
        private static string fileUploadController = "api/fileUpload/";
        private static string projectAttachmentController = "api/ProjectAttachment/";
        private static string projectPaymenetTermController = "api/ProjectPaymentTerm/";
        private static string projectDailyWorkController = "api/projectDailyWork/";
        private static string consumableController = "api/consumable/";
        private static string projectUpdateScheduleController = "api/projectUpdateSchedule/";
        private static string applicationController = "api/application/";
        private static string patrolController = "api/patrol/";
        private static string consumableAskForController = "api/ConsumableAskFor/";
        private static string workAttendanceController = "api/workAttendance/";
        private static string placeOnFileController = "api/placeOnFile/";

        //登录
        internal static string Login = $"{userController}{nameof(Login)}";
        //获取合同
        internal static string GetContracts = $"{projectController}{nameof(GetContracts)}";
        //获取分页项目
        internal static string PaginatedSearchProject = $"{projectController}{nameof(PaginatedSearchProject)}";
        //获取合同名称
        internal static string GetContractName = $"{projectController}{nameof(GetContractName)}";
        //获取开工项目分页数据
        internal static string PaginatedOpenningProject = $"{projectController}{nameof(PaginatedOpenningProject)}";
        //查询临近计划结束时间（7天）的项目
        internal static string ApproachingPlanDateSearch = $"{projectController}{nameof(ApproachingPlanDateSearch)}";
        //查询已延期的项目
        internal static string DelayedPlanDateSearch = $"{projectController}{nameof(DelayedPlanDateSearch)}";
        //查询进度预警的项目
        internal static string ProcessWarningDateSearch = $"{projectController}{nameof(ProcessWarningDateSearch)}";
        //根据合同ID获取合同
        internal static string GetContractById = $"{projectController}{nameof(GetContractById)}";
        //根据客户ID获取客户联系人
        internal static string CustomerContactList = $"{projectController}{nameof(CustomerContactList)}";
        //获取员工
        internal static string StaffList = $"{staffController}{nameof(StaffList)}";
        //获取员工（附带项目名称）
        internal static string GetStaffListWithProjectName = $"{staffController}{nameof(GetStaffListWithProjectName)}";
        //根据职位获取员工
        internal static string StaffByDuty = $"{staffController}{nameof(StaffByDuty)}";
        //获取空闲的项目人员
        internal static string IdleProjectStaff = $"{staffController}{nameof(IdleProjectStaff)}";
        //获取项目中的人员
        internal static string ProjectStaffByProjectId = $"{staffController}{nameof(ProjectStaffByProjectId)}";
        //根据字典类型获取字典数据
        internal static string GetDictDataByTypeName = $"{dictController}{nameof(GetDictDataByTypeName)}";
        //根据字典类型名称，字典显示字段获取数据
        internal static string GetDictDataId = $"{dictController}{nameof(GetDictDataId)}";
        //根据DictCode获取字典数据
        internal static string GetDictData = $"{dictController}{nameof(GetDictData)}";
        //获取工序列表
        internal static string GetProcessList = $"{processController}{nameof(GetProcessList)}";
        //获取工序及单位
        internal static string GetProcessUnitList = $"{processController}{nameof(GetProcessUnitList)}";
        //获取工序模板
        internal static string GetProcessTemplateList = $"{processController}{nameof(GetProcessTemplateList)}";
        //根据模板Id获取工序模板详情
        internal static string GetProcessTemplate = $"{processController}{nameof(GetProcessTemplate)}";
        //保存工序模板
        internal static string SaveProcessTemplate = $"{processController}{nameof(SaveProcessTemplate)}";
        //获取项目工序员工配置关系
        internal static string GetProjectProcessStaffRelatedSettings = $"{processController}{nameof(GetProjectProcessStaffRelatedSettings)}";
        //保存项目工序员工配置关系
        internal static string SaveProjectProcessStaffRelated = $"{processController}{nameof(SaveProjectProcessStaffRelated)}";
        //获取项目工序员工配置关系
        internal static string GetProjectProcessStaffRelatedList = $"{processController}{nameof(GetProjectProcessStaffRelatedList)}";
        //获取设备类型
        internal static string DeviceTypeList = $"{deviceController}{nameof(DeviceTypeList)}";
        //根据项目ID获取项目设备
        internal static string ProjectDeviceByProjectId =$"{deviceController}{nameof(GetDictDataByTypeName)}";
        //根据查询条件获取设备
        internal static string DeviceList = $"{deviceController}{nameof(DeviceList)}";
        //根据查询条件获取分页设备
        internal static string DevicePaginatedList = $"{deviceController}{nameof(DevicePaginatedList)}";
        //保存项目，立项
        internal static string SaveProject = $"{projectController}{nameof(SaveProject)}";
        //根据ID获取项目
        internal static string GetProjectById = $"{projectController}{nameof(GetProjectById)}";
        //更新项目的开工日期
        internal static string UpdateProjectStartDate = $"{projectController}{nameof(UpdateProjectStartDate)}";
        //更新项目工序
        internal static string UpdateProjectProcess = $"{projectController}{nameof(UpdateProjectProcess)}";
        //根据输入获取项目名称和项目ID
        internal static string LoadProjectNames = $"{projectController}{nameof(LoadProjectNames)}";
        //根据项目ID获取项目工序
        internal static string GetProjectProcesses = $"{processController}{nameof(GetProjectProcesses)}";
        //获取日工作量历史
        internal static string GetProjectDailyProcessHistory = $"{projectDailyProcessController}{nameof(GetProjectDailyProcessHistory)}";
        //保存日工作量
        internal static string SaveProjectDailyProcess = $"{projectDailyProcessController}{nameof(SaveProjectDailyProcess)}";
        //修改日工作量
        internal static string UpdateProjectDailyProcess = $"{projectDailyProcessController}{nameof(UpdateProjectDailyProcess)}";
        //删除日工作量
        internal static string RemoveProjectDailyProcess = $"{projectDailyProcessController}{nameof(RemoveProjectDailyProcess)}";
        //获取工作量工序奖金设置
        internal static string GetBonusList = $"{projectBonusController}{nameof(GetBonusList)}";
        //保存工作量工序奖金设置
        internal static string SaveBonus = $"{projectBonusController}{nameof(SaveBonus)}";
        //获取项目奖金
        internal static string GetBonusEx = $"{projectBonusController}{nameof(GetBonusEx)}";
        //保存项目奖金
        internal static string SaveBonusEx = $"{projectBonusController}{nameof(SaveBonusEx)}";
        //根据项目，时间，员工计算绩效
        internal static string AchievementBonusCalculate = $"{projectBonusController}{nameof(AchievementBonusCalculate)}";
        //根据项目查询项目绩效
        internal static string ProjectBonusCalculate = $"{projectBonusController}{nameof(ProjectBonusCalculate)}";
        //保存项目预警设置
        internal static string SaveProjectEarlyWarnings = $"{earlyWarningController}{nameof(SaveProjectEarlyWarnings)}";
        //获取项目预警设置
        internal static string GetProjectEarlyWarnings = $"{earlyWarningController}{nameof(GetProjectEarlyWarnings)}";
        //保存项目人员
        internal static string SaveProjectStaffs = $"{staffController}{nameof(SaveProjectStaffs)}";
        //获取进度快的项目工作人员
        internal static string SpeedupProjectStaff = $"{staffController}{nameof(SpeedupProjectStaff)}";
        //保存项目设备
        internal static string SaveProjectDevice = $"{deviceController}{nameof(SaveProjectDevice)}";
        //保存项目计划人天数，计划完成时间
        internal static string UpdateProjectPlanData = $"{projectController}{nameof(UpdateProjectPlanData)}";
        //上传文件
        internal static string UploadMultiple = $"{fileUploadController}{nameof(UploadMultiple)}";
        //获取项目文件
        internal static string GetProjectAttachments = $"{projectAttachmentController}{nameof(GetProjectAttachments)}";
        //删除项目文件
        internal static string RemoveProjectAttachment = $"{projectAttachmentController}{nameof(RemoveProjectAttachment)}";
        //获取项目动态分页数据
        internal static string GetProjectDynamics = $"{projectController}{nameof(GetProjectDynamics)}";
        //获取预警分页历史消息
        internal static string PaginatedWarningHistory = $"{earlyWarningController}{nameof(PaginatedWarningHistory)}";
        //获取预警信息
        internal static string GetEarlyWarningHistoryById = $"{earlyWarningController}{nameof(GetEarlyWarningHistoryById)}";
        //验收项目
        internal static string AcceptanceProject = $"{projectController}{nameof(AcceptanceProject)}";
        //获取必要附件列表数据
        internal static string GetAttachmentRequirementList = $"{projectAttachmentController}{nameof(GetAttachmentRequirementList)}";
        //申请归档
        internal static string SavePlaceOnFile = $"{placeOnFileController}{nameof(SavePlaceOnFile)}";
        //获取申请归档分页数据
        internal static string PaginatedApplyPlaceOnFileProject = $"{placeOnFileController}{nameof(PaginatedApplyPlaceOnFileProject)}";
        //读取附件分页数据
        internal static string PaginatedProjectAttachments = $"{projectAttachmentController}{nameof(PaginatedProjectAttachments)}";
        //获取付款条件
        internal static string ProjectPaymentTermList = $"{projectPaymenetTermController}{nameof(ProjectPaymentTermList)}";
        //保存付款条件
        internal static string SaveProjectPaymentTerm = $"{projectPaymenetTermController}{nameof(SaveProjectPaymentTerm)}";
        //保存预警处理信息
        internal static string SaveEarlyWarningHistory = $"{earlyWarningController}{nameof(SaveEarlyWarningHistory)}";
        //保存报量
        internal static string SaveProjectDailyWork = $"{projectDailyWorkController}{nameof(SaveProjectDailyWork)}";
        //获取报量分页数据
        internal static string PaginatedSearchProjectDailyWork = $"{projectDailyWorkController}{nameof(PaginatedSearchProjectDailyWork)}";
        //根据ID获取报量数据
        internal static string GetDailyWorkById = $"{projectDailyWorkController}{nameof(GetDailyWorkById)}";
        //获取工作报量汇总
        internal static string GetDailyWorkSummary = $"{projectDailyWorkController}{nameof(GetDailyWorkSummary)}";
        //批量录入工作报量
        internal static string SaveBatchDailyWork = $"{projectDailyWorkController}{nameof(SaveBatchDailyWork)}";
        //excel录入工作报量
        internal static string SaveExcelDailyWork = $"{projectDailyWorkController}{nameof(SaveExcelDailyWork)}";
        //添加设备
        internal static string SaveDevice = $"{deviceController}{nameof(SaveDevice)}";
        //根据ID获取设备
        internal static string GetDeviceById = $"{deviceController}{nameof(GetDeviceById)}";
        //删除设备
        internal static string RemoveDevice = $"{deviceController}{nameof(RemoveDevice)}";
        //获取设备调动记录
        internal static string PaginatedProjectDeviceHistory = $"{deviceController}{nameof(PaginatedProjectDeviceHistory)}";
        //获取设备类型分页数据
        internal static string PaginatedDeviceType = $"{deviceController}{nameof(PaginatedDeviceType)}";
        //根据ID获取设备类型
        internal static string GetDeviceTypeById = $"{deviceController}{nameof(GetDeviceTypeById)}";
        //保存设备类型
        internal static string SaveDeviceType = $"{deviceController}{nameof(SaveDeviceType)}";
        //批量保存设备
        internal static string BatchSaveDevice = $"{deviceController}{nameof(BatchSaveDevice)}";
        //设置设备处理人
        internal static string SetHandleBy = $"{deviceController}{nameof(SetHandleBy)}";
        //根据ID获取消耗品类型
        internal static string GetConsumableTypeById = $"{consumableController}{nameof(GetConsumableTypeById)}";
        //保存消耗品类型
        internal static string SaveConsumableType = $"{consumableController}{nameof(SaveConsumableType)}";
        //获取消耗品类型分页数据
        internal static string PaginatedConsumableType = $"{consumableController}{nameof(PaginatedConsumableType)}";
        //获取消耗品类型列表数据
        internal static string GetConsumableTypeList = $"{consumableController}{nameof(GetConsumableTypeList)}";
        //获取消耗品分页数据
        internal static string PaginatedConsumable = $"{consumableController}{nameof(PaginatedConsumable)}";
        //获取消耗品列表数据
        internal static string GetConsumableList = $"{consumableController}{nameof(GetConsumableList)}";
        //根据ID获取消耗品数据
        internal static string GetConsumableById = $"{consumableController}{nameof(GetConsumableById)}";
        //保存消耗品
        internal static string SaveConsumable = $"{consumableController}{nameof(SaveConsumable)}";
        //消耗品入库
        internal static string SaveStockInBound = $"{consumableController}{nameof(SaveStockInBound)}";
        //消耗品出库
        internal static string SaveStockOutBound = $"{consumableController}{nameof(SaveStockOutBound)}";
        //消耗品调动记录分页数据
        internal static string PaginatedConsumableBound = $"{consumableController}{nameof(PaginatedConsumableBound)}";
        //根据ID获取消耗品调入记录
        internal static string GetStockInBoundById = $"{consumableController}{nameof(GetStockInBoundById)}";
        //根据ID获取消耗品调出记录
        internal static string GetStockOutBoundById = $"{consumableController}{nameof(GetStockOutBoundById)}";
        //保存进度调整记录
        internal static string AddProjectUpdateSchedule = $"{projectUpdateScheduleController}{nameof(AddProjectUpdateSchedule)}";
        //获取进度调整分页数据
        internal static string PaginatedProjectUpdateSchedule = $"{projectUpdateScheduleController}{nameof(PaginatedProjectUpdateSchedule)}";
        //根据ID获取项目简单信息
        internal static string GetSimpleProjectById = $"{projectController}{nameof(GetSimpleProjectById)}";
        //保存申请
        internal static string SaveApplication = $"{applicationController}{nameof(SaveApplication)}";
        //获取申请分页数据
        internal static string PaginatedApplication = $"{applicationController}{nameof(PaginatedApplication)}";
        //获取申请详情
        internal static string GetApplicationById = $"{applicationController}{nameof(GetApplicationById)}";
        //审批申请
        internal static string ApproveApplication = $"{applicationController}{nameof(ApproveApplication)}";
        //处理设备申请
        internal static string ProcessDeviceApplication = $"{applicationController}{nameof(ProcessDeviceApplication)}";
        //处理人员申请
        internal static string ProcessStaffApplication = $"{applicationController}{nameof(ProcessStaffApplication)}";
        //处理消耗品申请
        internal static string ProcessConsumableApplication = $"{applicationController}{nameof(ProcessConsumableApplication)}";
        //获取巡查历史分页数据
        internal static string PaginatedPatrol = $"{patrolController}{nameof(PaginatedPatrol)}";
        //获取巡查列表数据
        internal static string GetPatrolList = $"{patrolController}{nameof(GetPatrolList)}";
        //保存巡查
        internal static string SavePatrol = $"{patrolController}{nameof(SavePatrol)}";
        //根据ID获取巡查
        internal static string GetPatrolById = $"{patrolController}{nameof(GetPatrolById)}";
        //excel导入巡查记录
        internal static string SavePatrolByExcel = $"{patrolController}{nameof(SavePatrolByExcel)}";
        //获取消耗品申购分页记录
        internal static string PaginatedConsumableAskFor = $"{consumableAskForController}{nameof(PaginatedConsumableAskFor)}";
        //保存消耗品申购记录
        internal static string SaveConsumableAskFor = $"{consumableAskForController}{nameof(SaveConsumableAskFor)}";
        //根据ID获取消耗品申购记录
        internal static string GetConsumableAskForById = $"{consumableAskForController}{nameof(GetConsumableAskForById)}";
        //保存考勤打卡月报
        internal static string InsertYearMonthWorkAttendanceExcel = $"{workAttendanceController}{nameof(InsertYearMonthWorkAttendanceExcel)}";
        //保存考勤补打卡记录
        internal static string InsertDelayClockExcel = $"{workAttendanceController}{nameof(InsertDelayClockExcel)}";
        //保存外出打卡记录
        internal static string InsertOutClockExcel = $"{workAttendanceController}{nameof(InsertOutClockExcel)}";
        //保存请假打卡记录
        internal static string InsertApplyLeaveExcel = $"{workAttendanceController}{nameof(InsertApplyLeaveExcel)}";
        //获取打卡月报
        internal static string GetYearMonthWorkAttendance = $"{workAttendanceController}{nameof(GetYearMonthWorkAttendance)}";
        #endregion

        #region 字典信息
        internal static string ProjectStatus = nameof(ProjectStatus);
        #endregion
        #region 职位信息
        internal static List<int> ProjectManagerDuty = new List<int> { 203, 204, 205, 206, 207, 208 };
        internal static List<int> ProjectStaffDuty = ProjectManagerDuty.Union(new List<int> { 201, 202, 209 }).ToList();
        internal static List<int> SalesManagerDuty = new List<int> { 26, 28, 60, 61, 62 };
        #endregion
    }
}
