using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectService.Db;

public partial class ProjectDbContext : DbContext
{
    public ProjectDbContext()
    {
    }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationConsumable> ApplicationConsumables { get; set; }

    public virtual DbSet<ApplicationDevice> ApplicationDevices { get; set; }

    public virtual DbSet<ApplicationPerson> ApplicationPeople { get; set; }

    public virtual DbSet<AttachmentRequirement> AttachmentRequirements { get; set; }

    public virtual DbSet<Consumable> Consumables { get; set; }

    public virtual DbSet<ConsumableAskFor> ConsumableAskFors { get; set; }

    public virtual DbSet<ConsumableAskForItem> ConsumableAskForItems { get; set; }

    public virtual DbSet<ConsumableType> ConsumableTypes { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerContact> CustomerContacts { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceType> DeviceTypes { get; set; }

    public virtual DbSet<DictDatum> DictData { get; set; }

    public virtual DbSet<DictType> DictTypes { get; set; }

    public virtual DbSet<EarlyWarningHistory> EarlyWarningHistories { get; set; }

    public virtual DbSet<PlaceOnFile> PlaceOnFiles { get; set; }

    public virtual DbSet<ProUnit> ProUnits { get; set; }

    public virtual DbSet<Process> Processes { get; set; }

    public virtual DbSet<ProcessTemplate> ProcessTemplates { get; set; }

    public virtual DbSet<ProcessTemplateDetail> ProcessTemplateDetails { get; set; }

    public virtual DbSet<ProcessUnit> ProcessUnits { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectAttachment> ProjectAttachments { get; set; }

    public virtual DbSet<ProjectBonu> ProjectBonus { get; set; }

    public virtual DbSet<ProjectBonusEx> ProjectBonusexes { get; set; }

    public virtual DbSet<ProjectDailyProcess> ProjectDailyProcesses { get; set; }

    public virtual DbSet<ProjectDailyWork> ProjectDailyWorks { get; set; }

    public virtual DbSet<ProjectDevice> ProjectDevices { get; set; }

    public virtual DbSet<ProjectEarlyWarning> ProjectEarlyWarnings { get; set; }

    public virtual DbSet<ProjectPatrol> ProjectPatrols { get; set; }

    public virtual DbSet<ProjectPaymentTerm> ProjectPaymentTerms { get; set; }

    public virtual DbSet<ProjectProcess> ProjectProcesses { get; set; }

    public virtual DbSet<ProjectProcessStaffRelated> ProjectProcessStaffRelateds { get; set; }

    public virtual DbSet<ProjectStaff> ProjectStaffs { get; set; }

    public virtual DbSet<ProjectSuspendedHistory> ProjectSuspendedHistories { get; set; }

    public virtual DbSet<ProjectUpdateSchedule> ProjectUpdateSchedules { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StockInBound> StockInBounds { get; set; }

    public virtual DbSet<StockOutBound> StockOutBounds { get; set; }

    public virtual DbSet<WorkApplyLeave> WorkApplyLeaves { get; set; }

    public virtual DbSet<WorkAttendance> WorkAttendances { get; set; }

    public virtual DbSet<WorkDelayClock> WorkDelayClocks { get; set; }

    public virtual DbSet<WorkOutClock> WorkOutClocks { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=User-2024GTUDDM\\SQLEXPRESS;Database=ProjectDbNew;User ID=sa;Password=admin123@;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK_APPLICATION");

            entity.ToTable("Application", tb => tb.HasComment("申请表"));

            entity.Property(e => e.ApplicationId)
                .ValueGeneratedNever()
                .HasComment("申请ID");
            entity.Property(e => e.ApplicationContent)
                .HasMaxLength(1000)
                .HasComment("申请内容");
            entity.Property(e => e.ApplicationDelivery)
                .HasMaxLength(300)
                .HasComment("申请收货地址");
            entity.Property(e => e.ApplicationItemCount).HasComment("申请数量");
            entity.Property(e => e.ApplicationResContent)
                .HasMaxLength(1000)
                .HasComment("审批回复内容");
            entity.Property(e => e.ApplicationResTime)
                .HasComment("审批时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ApplicationStatus).HasComment("申请状态（0申请，1通过，2未通过）");
            entity.Property(e => e.ApplicationTime)
                .HasComment("申请时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ApplicationTitle)
                .HasMaxLength(100)
                .HasComment("申请标题");
            entity.Property(e => e.ApplicationType).HasComment("申请类型（1，设备，2消耗品，3人员调动）");
            entity.Property(e => e.ApplicationUser).HasComment("申请人，项目主管");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasComment("是否删除");
            entity.Property(e => e.ProjectId).HasComment("项目ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ApplicationUserNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ApplicationUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPLICAT_REFERENCE_STAFF");

            entity.HasOne(d => d.Project).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPLICAT_REFERENCE_PROJECT");
        });

        modelBuilder.Entity<ApplicationConsumable>(entity =>
        {
            entity.ToTable("ApplicationConsumable");

            entity.Property(e => e.ApplicationConsumableId).ValueGeneratedNever();
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationConsumables)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationConsumable_Application");

            entity.HasOne(d => d.ConsumableType).WithMany(p => p.ApplicationConsumables)
                .HasForeignKey(d => d.ConsumableTypeId)
                .HasConstraintName("FK_ApplicationConsumable_ConsumableType");
        });

        modelBuilder.Entity<ApplicationDevice>(entity =>
        {
            entity.ToTable("ApplicationDevice");

            entity.Property(e => e.ApplicationDeviceId).ValueGeneratedNever();
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationDevices)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationDevice_Application");

            entity.HasOne(d => d.DeviceType).WithMany(p => p.ApplicationDevices)
                .HasForeignKey(d => d.DeviceTypeId)
                .HasConstraintName("FK_ApplicationDevice_DeviceType");
        });

        modelBuilder.Entity<ApplicationPerson>(entity =>
        {
            entity.ToTable("ApplicationPerson");

            entity.Property(e => e.ApplicationPersonId).ValueGeneratedNever();
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationPeople)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationPerson_Application");

            entity.HasOne(d => d.Process).WithMany(p => p.ApplicationPeople)
                .HasForeignKey(d => d.ProcessId)
                .HasConstraintName("FK_ApplicationPerson_Process");
        });

        modelBuilder.Entity<AttachmentRequirement>(entity =>
        {
            entity.ToTable("AttachmentRequirement");

            entity.Property(e => e.AttachmentName).HasMaxLength(100);
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Consumable>(entity =>
        {
            entity.HasKey(e => e.ConsumableId).HasName("PK_CONSUMABLE");

            entity.ToTable("Consumable", tb => tb.HasComment("消耗品表"));

            entity.Property(e => e.ConsumableId)
                .ValueGeneratedNever()
                .HasComment("消耗品ID");
            entity.Property(e => e.ConsumableNumber)
                .HasMaxLength(50)
                .HasComment("消耗品编号");
            entity.Property(e => e.ConsumableStatus).HasComment("消耗品状态");
            entity.Property(e => e.ConsumableTypeId).HasComment("消耗品类型ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 10)");
            entity.Property(e => e.Quantity).HasComment("数量");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ConsumableType).WithMany(p => p.Consumables)
                .HasForeignKey(d => d.ConsumableTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONSUMAB_REFERENCE_CONSUMAB");
        });

        modelBuilder.Entity<ConsumableAskFor>(entity =>
        {
            entity.ToTable("ConsumableAskFor");

            entity.Property(e => e.ConsumableAskForId).ValueGeneratedNever();
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ConsumableAskFors)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ConsumableAskFor_Project");

            entity.HasOne(d => d.Staff).WithMany(p => p.ConsumableAskFors)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumableAskFor_Staff");
        });

        modelBuilder.Entity<ConsumableAskForItem>(entity =>
        {
            entity.ToTable("ConsumableAskForItem");

            entity.Property(e => e.ConsumableAskForItemId).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ConsumableAskFor).WithMany(p => p.ConsumableAskForItems)
                .HasForeignKey(d => d.ConsumableAskForId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumableAskForItem_ConsumableAskFor");

            entity.HasOne(d => d.ConsumableType).WithMany(p => p.ConsumableAskForItems)
                .HasForeignKey(d => d.ConsumableTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsumableAskForItem_ConsumableType");
        });

        modelBuilder.Entity<ConsumableType>(entity =>
        {
            entity.HasKey(e => e.ConsumableTypeId).HasName("PK_CONSUMABLETYPE");

            entity.ToTable("ConsumableType", tb => tb.HasComment("消耗品类型表"));

            entity.Property(e => e.ConsumableTypeId)
                .ValueGeneratedNever()
                .HasComment("消耗品类型ID");
            entity.Property(e => e.ConsumableModel)
                .HasMaxLength(50)
                .HasComment("消耗品型号");
            entity.Property(e => e.ConsumableTypeName)
                .HasMaxLength(50)
                .HasComment("消耗品类型名称");
            entity.Property(e => e.ConsumableUnit)
                .HasMaxLength(10)
                .HasComment("消耗品单位");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK_CONTRACT");

            entity.ToTable("Contract", tb => tb.HasComment("合同表"));

            entity.Property(e => e.ContractId)
                .ValueGeneratedNever()
                .HasComment("合同ID");
            entity.Property(e => e.ContractAmount)
                .HasComment("合同金额")
                .HasColumnType("money");
            entity.Property(e => e.ContractEndDate)
                .HasComment("合同结束时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ContractName)
                .HasMaxLength(200)
                .HasComment("合同名称");
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasComment("合同编号");
            entity.Property(e => e.ContractPayAmount)
                .HasComment("合同付款")
                .HasColumnType("money");
            entity.Property(e => e.ContractStartDate)
                .HasComment("合同开始时间")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerContactId).HasComment("客户联系人ID");
            entity.Property(e => e.CustomerId).HasComment("客户ID");
            entity.Property(e => e.IsDeleted).HasComment("是否删除");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CustomerContact).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CustomerContactId)
                .HasConstraintName("FK_CONTRACT_REFERENCE_CUSTOMER2");

            entity.HasOne(d => d.Customer).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTRACT_REFERENCE_CUSTOMER");

            entity.HasOne(d => d.SalesManager).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.SalesManagerId)
                .HasConstraintName("FK_Contract_Staff");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_CUSTOMER");

            entity.ToTable("Customer", tb => tb.HasComment("客户表"));

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasComment("客户ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.CustAddress)
                .HasMaxLength(200)
                .HasComment("客户地址");
            entity.Property(e => e.CustNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("客户编号");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .HasComment("客户名称");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CustomerContact>(entity =>
        {
            entity.HasKey(e => e.CustomerContactId).HasName("PK_CUSTOMERCONTACT");

            entity.ToTable("CustomerContact", tb => tb.HasComment("客户联系人表"));

            entity.Property(e => e.CustomerContactId)
                .ValueGeneratedNever()
                .HasComment("客户联系人表");
            entity.Property(e => e.ContactName)
                .HasMaxLength(20)
                .HasComment("联系人名称");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasComment("客户ID");
            entity.Property(e => e.Mobile)
                .HasMaxLength(200)
                .HasComment("联系方式");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerContacts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUSTOMER_REFERENCE_CUSTOMER");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("PK_DEVICE");

            entity.ToTable("Device", tb => tb.HasComment("设备表"));

            entity.Property(e => e.DeviceId)
                .ValueGeneratedNever()
                .HasComment("设备ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceNumber)
                .HasMaxLength(50)
                .HasComment("设备编号");
            entity.Property(e => e.DeviceStatus).HasComment("设备状态");
            entity.Property(e => e.DeviceTypeId).HasComment("设备类型ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.DeviceType).WithMany(p => p.Devices)
                .HasForeignKey(d => d.DeviceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEVICE_REFERENCE_DEVICETY");
        });

        modelBuilder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(e => e.DeviceTypeId).HasName("PK_DEVICETYPE");

            entity.ToTable("DeviceType", tb => tb.HasComment("设备类型表"));

            entity.Property(e => e.DeviceTypeId)
                .ValueGeneratedNever()
                .HasComment("设备类型ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceModel)
                .HasMaxLength(50)
                .HasComment("设备型号名称");
            entity.Property(e => e.DeviceTypeName)
                .HasMaxLength(50)
                .HasComment("设备类型名称");
            entity.Property(e => e.DeviceUnit)
                .HasMaxLength(10)
                .HasComment("设备单位");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<DictDatum>(entity =>
        {
            entity.HasKey(e => e.DictCode).HasName("PK_DICTDATA");

            entity.ToTable(tb => tb.HasComment("字典数据表"));

            entity.Property(e => e.DictCode).HasComment("字典编码");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.DictLabel)
                .HasMaxLength(100)
                .HasComment("字典标签");
            entity.Property(e => e.DictTypeId).HasComment("字典类型ID");
            entity.Property(e => e.DictValue)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("字典键值");
            entity.Property(e => e.ParentCode).HasComment("父编码");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.Status).HasComment("状态（1正常0停用）");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.DictType).WithMany(p => p.DictData)
                .HasForeignKey(d => d.DictTypeId)
                .HasConstraintName("FK_DICTDATA_REFERENCE_DICTTYPE");
        });

        modelBuilder.Entity<DictType>(entity =>
        {
            entity.HasKey(e => e.DictId).HasName("PK_DICTTYPE");

            entity.ToTable("DictType", tb => tb.HasComment("字典类型表"));

            entity.Property(e => e.DictId).HasComment("字典主键");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.DictName)
                .HasMaxLength(50)
                .HasComment("字典名称");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.Status).HasComment("状态（1正常0停用）");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("字典类型");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<EarlyWarningHistory>(entity =>
        {
            entity.ToTable("EarlyWarningHistory");

            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.ManagerReason).HasMaxLength(500);
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.StaffReason).HasMaxLength(500);
            entity.Property(e => e.Suggestions).HasMaxLength(500);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            entity.Property(e => e.WarningMessages).HasMaxLength(200);

            entity.HasOne(d => d.Project).WithMany(p => p.EarlyWarningHistories)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EarlyWarningHistory_Project");
        });

        modelBuilder.Entity<PlaceOnFile>(entity =>
        {
            entity.ToTable("PlaceOnFile");

            entity.Property(e => e.PlaceOnFileId).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.PlaceOnFiles)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlaceOnFile_Project");
        });

        modelBuilder.Entity<ProUnit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK_PROUNIT");

            entity.ToTable("ProUnit");

            entity.Property(e => e.UnitId).HasComment("单位ID号");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UnitName)
                .HasMaxLength(10)
                .HasComment("单位名称");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK_PROCESS");

            entity.ToTable("Process");

            entity.Property(e => e.ProcessId).HasComment("工序ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ProcessName)
                .HasMaxLength(50)
                .HasComment("工序名称");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ProcessTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROCESSTEMPLATE");

            entity.ToTable("ProcessTemplate", tb => tb.HasComment("工序模板表"));

            entity.Property(e => e.Id).HasComment("模板ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("模板名称");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ProcessTemplateDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROCESSTEMPLATEDETAIL");

            entity.ToTable("ProcessTemplateDetail", tb => tb.HasComment("工序模板详情表"));

            entity.Property(e => e.Id).HasComment("详情ID号");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ProcessUnitId).HasComment("工序单位关联ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.Sequence).HasComment("顺序号");
            entity.Property(e => e.TemplateId).HasComment("模板ID号");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Weight)
                .HasComment("权重")
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.ProcessUnit).WithMany(p => p.ProcessTemplateDetails)
                .HasForeignKey(d => d.ProcessUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROCESST_REFERENCE_PROCESSU");

            entity.HasOne(d => d.Template).WithMany(p => p.ProcessTemplateDetails)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROCESST_REFERENCE_PROCESST");
        });

        modelBuilder.Entity<ProcessUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROCESSUNIT");

            entity.ToTable("ProcessUnit", tb => tb.HasComment("工序单位关联表"));

            entity.Property(e => e.Id).HasComment("关联ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ProcessId).HasComment("工序ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UnitId).HasComment("单位ID");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Process).WithMany(p => p.ProcessUnits)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROCESSU_REFERENCE_PROCESS");

            entity.HasOne(d => d.Unit).WithMany(p => p.ProcessUnits)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROCESSU_REFERENCE_PROUNIT");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK_PROJECT");

            entity.ToTable("Project", tb => tb.HasComment("项目表"));

            entity.Property(e => e.ProjectId)
                .ValueGeneratedNever()
                .HasComment("项目ID");
            entity.Property(e => e.AcceptanceDate)
                .HasComment("验收日期")
                .HasColumnType("datetime");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.ContractId).HasComment("合同ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.PlanEndDate)
                .HasComment("计划结束日期")
                .HasColumnType("datetime");
            entity.Property(e => e.PlanPersonDays).HasComment("计划人天数");
            entity.Property(e => e.ProjectManagerId).HasComment("主管人员ID");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(200)
                .HasComment("项目名称");
            entity.Property(e => e.RegionId).HasComment("区域ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.SalesManagerId).HasComment("销售人员ID");
            entity.Property(e => e.StartDate)
                .HasComment("开工日期")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasComment("项目状态");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Contract).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECT_REFERENCE_CONTRACT");

            entity.HasOne(d => d.ProjectManager).WithMany(p => p.ProjectProjectManagers)
                .HasForeignKey(d => d.ProjectManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECT_REFERENCE_STAFF");

            entity.HasOne(d => d.SalesManager).WithMany(p => p.ProjectSalesManagers)
                .HasForeignKey(d => d.SalesManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECT_REFERENCE_STAFF2");
        });

        modelBuilder.Entity<ProjectAttachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROJECTATTACHMENT");

            entity.ToTable("ProjectAttachment", tb => tb.HasComment("项目附件表"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("附件表主键");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.FileAddress)
                .HasMaxLength(500)
                .HasComment("文件路径");
            entity.Property(e => e.FileName)
                .HasMaxLength(200)
                .HasComment("文件名称");
            entity.Property(e => e.FileType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("文件类型");
            entity.Property(e => e.ProjectId).HasComment("项目ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
            entity.Property(e => e.UploadDate)
                .HasComment("上传日期")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectAttachments)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTA_REFERENCE_PROJECT");
        });

        modelBuilder.Entity<ProjectBonu>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_PROJECTBONUS")
                .IsClustered(false);

            entity.ToTable(tb => tb.HasComment("项目奖金表"));

            entity.Property(e => e.Id).HasComment("奖金表ID");
            entity.Property(e => e.Bonus)
                .HasComment("奖金金额")
                .HasColumnType("money");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectProcessId).HasComment("项目工序ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Workload).HasComment("起算工作量");

            entity.HasOne(d => d.ProjectProcess).WithMany(p => p.ProjectBonus)
                .HasForeignKey(d => d.ProjectProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTB_REFERENCE_PROJECTP");
        });

        modelBuilder.Entity<ProjectBonusEx>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROJECTBONUSEX");

            entity.ToTable("ProjectBonusEx");

            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectBonusexes)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ProjectBonusEx_Project");
        });

        modelBuilder.Entity<ProjectDailyProcess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROJECTDAILYPROCESS");

            entity.ToTable("ProjectDailyProcess", tb => tb.HasComment("项目日工作量设定表"));

            entity.Property(e => e.Id).HasComment("日工作量表ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.DailyWorkload).HasComment("日工作量");
            entity.Property(e => e.ProjectProcessId).HasComment("项目工序ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.StartDate)
                .HasComment("开始时间")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ProjectProcess).WithMany(p => p.ProjectDailyProcesses)
                .HasForeignKey(d => d.ProjectProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTD_REFERENCE_PROJECTP2");
        });

        modelBuilder.Entity<ProjectDailyWork>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROJECTDAILYWORK");

            entity.ToTable("ProjectDailyWork", tb => tb.HasComment("员工项目日工作量报量表"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("主键");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectProcessId).HasComment("项目工序ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.StaffId).HasComment("员工ID");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Workload).HasComment("工作量");

            entity.HasOne(d => d.ProjectProcess).WithMany(p => p.ProjectDailyWorks)
                .HasForeignKey(d => d.ProjectProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTD_REFERENCE_PROJECTP");

            entity.HasOne(d => d.Staff).WithMany(p => p.ProjectDailyWorks)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTD_REFERENCE_STAFF");
        });

        modelBuilder.Entity<ProjectDevice>(entity =>
        {
            entity.HasKey(e => e.AssociationId).HasName("PK_PROJECTDEVICE");

            entity.ToTable("ProjectDevice", tb => tb.HasComment("项目设备表"));

            entity.Property(e => e.AssociationId)
                .ValueGeneratedNever()
                .HasComment("关联ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceId).HasComment("设备ID");
            entity.Property(e => e.ProjectId).HasComment("项目ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.TransferInDate)
                .HasComment("调入日期")
                .HasColumnType("datetime");
            entity.Property(e => e.TransferInOperator).HasComment("调入操作员ID");
            entity.Property(e => e.TransferOutDate)
                .HasComment("调出日期")
                .HasColumnType("datetime");
            entity.Property(e => e.TransferOutOperator).HasComment("调出操作员ID");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Device).WithMany(p => p.ProjectDevices)
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTD_REFERENCE_DEVICE");

            entity.HasOne(d => d.HandleByNavigation).WithMany(p => p.ProjectDevices)
                .HasForeignKey(d => d.HandleBy)
                .HasConstraintName("FK_ProjectDevice_Staff");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectDevices)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTD_REFERENCE_PROJECT");
        });

        modelBuilder.Entity<ProjectEarlyWarning>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROJECTEARLYWARNING");

            entity.ToTable("ProjectEarlyWarning", tb => tb.HasComment("项目预警表"));

            entity.Property(e => e.Id).HasComment("预警ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId).HasComment("项目ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
            entity.Property(e => e.WarningType).HasComment("预警类型");
            entity.Property(e => e.WarningValue).HasComment("预警值");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectEarlyWarnings)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTE_REFERENCE_PROJECT");
        });

        modelBuilder.Entity<ProjectPatrol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROJECTPATROL");

            entity.ToTable("ProjectPatrol", tb => tb.HasComment("项目巡查表"));

            entity.Property(e => e.Id).HasComment("巡查表主键");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.PatrolDate)
                .HasComment("巡查日期")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId).HasComment("项目号");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.StaffId).HasComment("员工号");
            entity.Property(e => e.Status).HasComment("巡查状态（0无需整改，1已整改，2未整改）");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectPatrols)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTP_REFERENCE_PROJECT2");

            entity.HasOne(d => d.Staff).WithMany(p => p.ProjectPatrols)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTP_REFERENCE_STAFF");
        });

        modelBuilder.Entity<ProjectPaymentTerm>(entity =>
        {
            entity.HasKey(e => e.PaymentTermsId).HasName("PK_PROJECTPAYMENTTERMS");

            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectPaymentTerms)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectPaymentTerms_Project");
        });

        modelBuilder.Entity<ProjectProcess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROJECTPROCESS");

            entity.ToTable("ProjectProcess", tb => tb.HasComment("项目工序表"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("工序ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ProcessUnitId).HasComment("工序单位关联表ID");
            entity.Property(e => e.ProjectId).HasComment("项目ID号");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.Sequence).HasComment("顺序号");
            entity.Property(e => e.StartingWorkload).HasComment("阶段开始工作量起始量");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Weight)
                .HasComment("权重")
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Workload).HasComment("工作总量");

            entity.HasOne(d => d.ProcessUnit).WithMany(p => p.ProjectProcesses)
                .HasForeignKey(d => d.ProcessUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTP_REFERENCE_PROCESSU");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectProcesses)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTP_REFERENCE_PROJECT");
        });

        modelBuilder.Entity<ProjectProcessStaffRelated>(entity =>
        {
            entity.HasKey(e => e.RelatedId);

            entity.ToTable("ProjectProcessStaffRelated");

            entity.Property(e => e.RelatedId).ValueGeneratedNever();

            entity.HasOne(d => d.ProcessUnit).WithMany(p => p.ProjectProcessStaffRelateds)
                .HasForeignKey(d => d.ProcessUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectProcessStaffRelated_ProcessUnit");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectProcessStaffRelateds)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectProcessStaffRelated_Project");

            entity.HasOne(d => d.Staff).WithMany(p => p.ProjectProcessStaffRelateds)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectProcessStaffRelated_Staff");
        });

        modelBuilder.Entity<ProjectStaff>(entity =>
        {
            entity.HasKey(e => e.AssociationId).HasName("PK_PROJECTSTAFF");

            entity.ToTable("ProjectStaff", tb => tb.HasComment("项目员工表"));

            entity.Property(e => e.AssociationId)
                .ValueGeneratedNever()
                .HasComment("项目员工表ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId).HasComment("项目ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.StaffId).HasComment("员工ID");
            entity.Property(e => e.TransferInDate)
                .HasComment("调入日期")
                .HasColumnType("datetime");
            entity.Property(e => e.TransferInOperator).HasComment("调入操作人员");
            entity.Property(e => e.TransferOutDate)
                .HasComment("调出日期")
                .HasColumnType("datetime");
            entity.Property(e => e.TransferOutOperator).HasComment("调出操作人员");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectStaffs)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTS_REFERENCE_PROJECT");

            entity.HasOne(d => d.Staff).WithMany(p => p.ProjectStaffs)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTS_REFERENCE_STAFF");
        });

        modelBuilder.Entity<ProjectSuspendedHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROJECTSUSPENDEDHISTORY");

            entity.ToTable("ProjectSuspendedHistory", tb => tb.HasComment("项目暂停历史表"));

            entity.Property(e => e.Id).HasComment("历史表主键");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId).HasComment("项目ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.StartDate)
                .HasComment("开工日期")
                .HasColumnType("datetime");
            entity.Property(e => e.SuspendedDate)
                .HasComment("暂停日期")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectSuspendedHistories)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTS_REFERENCE_PROJECT2");
        });

        modelBuilder.Entity<ProjectUpdateSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROJECTUPDATESCHEDULE");

            entity.ToTable("ProjectUpdateSchedule", tb => tb.HasComment("项目进度调整表"));

            entity.Property(e => e.Id).HasComment("进度表主键");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.PlanEndDate)
                .HasComment("原计划结束日期")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId).HasComment("项目ID");
            entity.Property(e => e.ReasonType).HasComment("原因类型");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedEndDate)
                .HasComment("更新后计划结束日期")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectUpdateSchedules)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECTU_REFERENCE_PROJECT");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK_STAFF");

            entity.ToTable(tb => tb.HasComment("员工表"));

            entity.Property(e => e.StaffId)
                .ValueGeneratedNever()
                .HasComment("员工ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.StaffCard)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("员工身份证号");
            entity.Property(e => e.StaffCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasComment("员工编号");
            entity.Property(e => e.StaffDepartment).HasComment("员工部门");
            entity.Property(e => e.StaffDuty).HasComment("员工职位");
            entity.Property(e => e.StaffFees)
                .HasComment("员工费率")
                .HasColumnType("decimal(20, 2)");
            entity.Property(e => e.StaffGiveMoneyType).HasComment("员工付款类型");
            entity.Property(e => e.StaffInsuranceAmount)
                .HasComment("员工保险金额")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StaffName)
                .HasMaxLength(20)
                .HasComment("员工名称");
            entity.Property(e => e.StaffPhone)
                .HasMaxLength(50)
                .HasComment("员工手机号");
            entity.Property(e => e.StaffRecode).HasComment("员工重编码");
            entity.Property(e => e.StaffSex)
                .HasMaxLength(5)
                .HasComment("员工性别");
            entity.Property(e => e.StaffStatus).HasComment("员工状态");
            entity.Property(e => e.StaffSubsidy)
                .HasComment("员工补贴")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StaffWages)
                .HasComment("员工工资")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StaffzzWages)
                .HasComment("员工转正工资")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<StockInBound>(entity =>
        {
            entity.HasKey(e => e.InBoundId).HasName("PK_STOCKINBOUND");

            entity.ToTable("StockInBound", tb => tb.HasComment("消耗品调入表"));

            entity.Property(e => e.InBoundId)
                .ValueGeneratedNever()
                .HasComment("调入ID");
            entity.Property(e => e.ConsumableId).HasComment("消耗品ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.InBoundDate)
                .HasComment("调入日期")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId).HasComment("项目ID");
            entity.Property(e => e.Quantity).HasComment("数量");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Consumable).WithMany(p => p.StockInBounds)
                .HasForeignKey(d => d.ConsumableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_STOCKINB_REFERENCE_CONSUMAB");

            entity.HasOne(d => d.Project).WithMany(p => p.StockInBounds)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_STOCKINB_REFERENCE_PROJECT");
        });

        modelBuilder.Entity<StockOutBound>(entity =>
        {
            entity.HasKey(e => e.OutBoundId).HasName("PK_STOCKOUTBOUND");

            entity.ToTable("StockOutBound", tb => tb.HasComment("消耗品调出表"));

            entity.Property(e => e.OutBoundId)
                .ValueGeneratedNever()
                .HasComment("调出ID");
            entity.Property(e => e.ConsumableId).HasComment("消耗品ID");
            entity.Property(e => e.CreateBy).HasComment("创建者");
            entity.Property(e => e.CreateTime)
                .HasComment("创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.OutBoundDate)
                .HasComment("调出日期")
                .HasColumnType("datetime");
            entity.Property(e => e.ProjectId).HasComment("项目ID");
            entity.Property(e => e.Quantity).HasComment("数量");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("备注");
            entity.Property(e => e.UpdateBy).HasComment("更新者");
            entity.Property(e => e.UpdateTime)
                .HasComment("更新时间")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Consumable).WithMany(p => p.StockOutBounds)
                .HasForeignKey(d => d.ConsumableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_STOCKOUT_REFERENCE_CONSUMAB");

            entity.HasOne(d => d.Project).WithMany(p => p.StockOutBounds)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_STOCKOUT_REFERENCE_PROJECT");
        });

        modelBuilder.Entity<WorkApplyLeave>(entity =>
        {
            entity.ToTable("WorkApplyLeave");

            entity.Property(e => e.WorkApplyLeaveId).HasMaxLength(50);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.LeaveType).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.WorkYearMonth).HasMaxLength(50);

            entity.HasOne(d => d.Staff).WithMany(p => p.WorkApplyLeaves)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkApplyLeave_Staff");
        });

        modelBuilder.Entity<WorkAttendance>(entity =>
        {
            entity.ToTable("WorkAttendance");

            entity.Property(e => e.WorkAttendanceId).ValueGeneratedNever();
            entity.Property(e => e.ClockIn01)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_01");
            entity.Property(e => e.ClockIn02)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_02");
            entity.Property(e => e.ClockIn03)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_03");
            entity.Property(e => e.ClockIn04)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_04");
            entity.Property(e => e.ClockIn05)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_05");
            entity.Property(e => e.ClockIn06)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_06");
            entity.Property(e => e.ClockIn07)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_07");
            entity.Property(e => e.ClockIn08)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_08");
            entity.Property(e => e.ClockIn09)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_09");
            entity.Property(e => e.ClockIn10)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_10");
            entity.Property(e => e.ClockIn11)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_11");
            entity.Property(e => e.ClockIn12)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_12");
            entity.Property(e => e.ClockIn13)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_13");
            entity.Property(e => e.ClockIn14)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_14");
            entity.Property(e => e.ClockIn15)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_15");
            entity.Property(e => e.ClockIn16)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_16");
            entity.Property(e => e.ClockIn17)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_17");
            entity.Property(e => e.ClockIn18)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_18");
            entity.Property(e => e.ClockIn19)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_19");
            entity.Property(e => e.ClockIn20)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_20");
            entity.Property(e => e.ClockIn21)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_21");
            entity.Property(e => e.ClockIn22)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_22");
            entity.Property(e => e.ClockIn23)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_23");
            entity.Property(e => e.ClockIn24)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_24");
            entity.Property(e => e.ClockIn25)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_25");
            entity.Property(e => e.ClockIn26)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_26");
            entity.Property(e => e.ClockIn27)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_27");
            entity.Property(e => e.ClockIn28)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_28");
            entity.Property(e => e.ClockIn29)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_29");
            entity.Property(e => e.ClockIn30)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_30");
            entity.Property(e => e.ClockIn31)
                .HasMaxLength(250)
                .HasColumnName("ClockIn_31");
            entity.Property(e => e.ClockOut01)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_01");
            entity.Property(e => e.ClockOut02)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_02");
            entity.Property(e => e.ClockOut03)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_03");
            entity.Property(e => e.ClockOut04)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_04");
            entity.Property(e => e.ClockOut05)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_05");
            entity.Property(e => e.ClockOut06)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_06");
            entity.Property(e => e.ClockOut07)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_07");
            entity.Property(e => e.ClockOut08)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_08");
            entity.Property(e => e.ClockOut09)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_09");
            entity.Property(e => e.ClockOut10)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_10");
            entity.Property(e => e.ClockOut11)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_11");
            entity.Property(e => e.ClockOut12)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_12");
            entity.Property(e => e.ClockOut13)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_13");
            entity.Property(e => e.ClockOut14)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_14");
            entity.Property(e => e.ClockOut15)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_15");
            entity.Property(e => e.ClockOut16)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_16");
            entity.Property(e => e.ClockOut17)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_17");
            entity.Property(e => e.ClockOut18)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_18");
            entity.Property(e => e.ClockOut19)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_19");
            entity.Property(e => e.ClockOut20)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_20");
            entity.Property(e => e.ClockOut21)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_21");
            entity.Property(e => e.ClockOut22)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_22");
            entity.Property(e => e.ClockOut23)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_23");
            entity.Property(e => e.ClockOut24)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_24");
            entity.Property(e => e.ClockOut25)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_25");
            entity.Property(e => e.ClockOut26)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_26");
            entity.Property(e => e.ClockOut27)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_27");
            entity.Property(e => e.ClockOut28)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_28");
            entity.Property(e => e.ClockOut29)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_29");
            entity.Property(e => e.ClockOut30)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_30");
            entity.Property(e => e.ClockOut31)
                .HasMaxLength(250)
                .HasColumnName("ClockOut_31");
            entity.Property(e => e.ProjectName).HasMaxLength(100);
            entity.Property(e => e.StaffAccount).HasMaxLength(100);
            entity.Property(e => e.StaffName).HasMaxLength(50);
            entity.Property(e => e.WorkYearMonth).HasMaxLength(50);

            entity.HasOne(d => d.Staff).WithMany(p => p.WorkAttendances)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkAttendance_Staff");
        });

        modelBuilder.Entity<WorkDelayClock>(entity =>
        {
            entity.HasKey(e => e.DelayClockId).HasName("PK_DelayClock");

            entity.ToTable("WorkDelayClock");

            entity.Property(e => e.DelayClockId).HasMaxLength(50);
            entity.Property(e => e.ApplyWorkTime).HasMaxLength(250);
            entity.Property(e => e.DelayClockTime).HasColumnType("datetime");
            entity.Property(e => e.WorkYearMonth).HasMaxLength(50);

            entity.HasOne(d => d.Staff).WithMany(p => p.WorkDelayClocks)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DelayClock_Staff");
        });

        modelBuilder.Entity<WorkOutClock>(entity =>
        {
            entity.ToTable("WorkOutClock");

            entity.Property(e => e.WorkOutClockId).ValueGeneratedNever();
            entity.Property(e => e.OutClockDateTime).HasColumnType("datetime");
            entity.Property(e => e.WorkYearMonth).HasMaxLength(50);

            entity.HasOne(d => d.Staff).WithMany(p => p.WorkOutClocks)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkOutClock_Staff");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
