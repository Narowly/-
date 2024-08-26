namespace ProjectViewModels
{
    public static class DictSettings
    {
        #region 项目状态
        public const string ProjectStatusTypeName = "ProjectStatus";
        public const string ProjectStatus_Approval = "已立项";
        public const string ProjectStatus_Oppenning = "已开工";
        public const string ProjectStatus_Pause = "暂停";
        public const string ProjectStatus_Accepted = "已验收";
        public const string ProjectStatus_PlaceOnFile = "已归档";
        #endregion
        #region 设备状态
        public const string ComputerDevice = "主机";
        public const string DeviceStatusTypeName = "DeviceStatus";
        public const string DeviceStatus_Normal = "正常";
        public const string DeviceStatus_Damage = "损坏";
        public const string DeviceStatus_Scrapped = "报废";
        #endregion
        #region 申请状态
        public const string ApplicationStatusTypeName = "ApplicationStatus";
        public const string ApplicationStatus_ApplyFor = "申请";
        public const string ApplicationStatus_Approved = "通过";
        public const string ApplicationStatus_Unapproved = "未通过";
        public const string ApplicationStatus_Processed = "已处理";
        #endregion
        #region 申请类型
        public const string ApplicationTypeName = "ApplicationType";
        public const string ApplicationType_Device = "设备申请";
        public const string ApplicationType_Consumable = "消耗品申请";
        public const string ApplicationType_People = "人员申请";
        #endregion
        #region 巡查整改状态
        public const string PatrolStatusTypeName = "PatrolStatus";
        public const string PatrolStatus_None = "无整改";
        public const string PatrolStatus_Rectified = "已整改";
        public const string PatrolStatus_NotRectified = "未整改";
        #endregion
        #region 消费品申购状态
        public const string ConsumableAskForStatusTypeName = "ConsumableAskForStatus";
        public const string ConsumableAskForStatus_Unprocess = "未处理";
        public const string ConsumableAskForStatus_Processing = "处理中";
        public const string ConsumableAskForStatus_Processed = "已处理";
        #endregion
    }
}
