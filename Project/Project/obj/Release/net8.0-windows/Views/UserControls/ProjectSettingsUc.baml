   M S B A M L   `   `   `  ���� B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 2    ,Project.Views.UserControls.ProjectSettingsUc   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:Project.Views.UserControls     S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  3system&clr-namespace:System;assembly=mscorlib     RegionBrush#��[    �� CornerRadius$  10��  EffectShadow4 J� Effect# [ $��16q� � 	Resources   TagCustomStyle  TagBaseStyle �� BasedOn �� 
TargetType  HandyControl.Controls.Tag �� Property �� Value��%K   &	       7  �� 8   "  .���  � �� �� ��  � ��$ 5q�._�7  1HandyControl.Controls.TransitioningContentControl  .��(  "HandyControl.Controls.ScrollViewer     IsInertiaEnabled$
 True��.��,  &HandyControl.Controls.SimpleStackPanel  $��16q�$��Stretch=�.+�/  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�	   ChildWrapping$
	 Wrap=�$��Stretch=�.+�&   HandyControl.Controls.TabControl  
   ShowOverflowButton$
 False��   ShowScrollButton$
 True��$
��Auto��$
��1100��   IsScrollable$
 True��  TabControlSliding#��[ .@�#  HandyControl.Controls.TabItem  $��开工日期设置�� �� 
IsSelected$
 True��.��  $ 10Q�   Orientation$ Vertical=�$��16q�.+�(  "System.Windows.Controls.DatePicker  $��Center=�$	��300��(	  "HandyControl.Controls.TitleElement 	  
TitleWidth$	 140 � 	  TitlePlacement$
 Left=�  DatePickerExtend#��[  	  Title$ 开工日期：��$��16q�   SelectedDate �� *Project.StartDate+�� $��Center=�.+���   ButtonPrimary#��[ $��保存������ *UpdateProjectStartDateCommand+  $��工序设置��.��  $ 10Q�$ Vertical=�#��[  $
	 Wrap=�.+��  � RowDefinitions �� $��9* ��� $��1* �.+��� ���� *Project.ProjectProcesses+���� .R�  $ 10Q�$��16q�.+��� $	��100��$��16,0,0,0q�${�False��$ 工序:��  TextBoxExtend#��[ $
 Left=����� *!ProcessUnit.Process.ProcessName+�� $	��100��$��16,0,0,0q�${�False��$ 单位:��#��[ $
 Left=����� *ProcessUnit.ProUnit.UnitName+�� -   $	��150��$��16,0,0,0q�$ 总量:��#��[ $
 Left=����� *
Workload+�� -   $	��150��$��16,0,0,0q�$ 
初始量:��#��[ $
 Left=����� *StartingWorkload+�� -   $	��150��$��16,0,0,0q�$ 权重:��#��[ $
 Left=����� *Weight+ �� StringFormat$ 0.####���� $	��200�� � Maximum$	 100Q� � Minimum$ 0Q� �� TickFrequency$ 1Q� �� IsSnapToTickEnabled$
 True��&
   HandyControl.Controls.TipElement 
  
Visibility$ Visible=� 
  	Placement$ Bottom=� 
  StringFormat$ #0.00�� � Value �� *Weight+�� $��1��$��Right=�$��16q�.+��� #��[ $��保存������ *UpdateProjectProcessCommand+  $��日工作量设置��.��  $��16q�   Orientation$ Vertical=�#��[  .+��  �� $��9* ��� $��1* �.+��� ���� *DailyProcessHistory+���� .R�9� $��0,0,0,16q����� *	StartDate+.��  .+�  $ 10Q�$��16q�$ Vertical=�.+�  #��[ $ 开始日期��$
 Left=�$��16,0,0,0q� �� *	StartDate+�� '  !HandyControl.Controls.InfoElement   Placeholder$ 变更理由��#��[ $	��140������ *	Remarks+  $ 10Q�$��16q�.+��� ���� *DailyProcessList+���� .R�  $ 10Q�$��16q�.+��� $	��100��$��16,0,0,0q�${�False��$ 工序:��#��[ $
 Left=����� *0.ProjectProcess.ProcessUnit.Process.ProcessName+�� $	��100��$��16,0,0,0q�${�False��$ 单位:��#��[ $
 Left=����� *-+ProjectProcess.ProcessUnit.ProUnit.UnitName+�� $	��140��$��16,0,0,0q����� *DailyWorkload+  $ 10Q�$��16q�.+��� $��保存��$	��100��#��[ ���� *.,DataContext.UpdateProjectDailyProcessCommand+ �� RelativeSource ��   �� AncestorType"  ������ �� $��删除��$	��100��  ButtonWarning#��[ ���� *.,DataContext.RemoveProjectDailyProcessCommand+ �� "  ������ �� $��1��$��Right=�$��16q�.+��� #��[ $��新增������ *AddProjectDailyProcessCommand+  $��绩效奖金设置设置��.��  $��16q�#��[  .+��  �� $��9* ��� $��1* �.+��� ���� *ProjectBonus+���� .R�  $��16q�$ Vertical=�.+�#  HandyControl.Controls.Divider  $
��Left=�!   Content! �� *0.ProjectProcess.ProcessUnit.Process.ProcessName+�� ���� *ProjectBonusList+���� .R�  $ 10Q�$��0,0,0,16q�.+��� $	��140��$
 Left=�$ 	工作量��#��[ ���� *
Workload+�� $	��140��$
 Left=�$ 奖金��#��[ ���� *Bonus+$ 0.####���� $��删除��#��[ ���� *.,DataContext.RemoveProjectProcessBonusCommand+ �� "  ����� �� $��0,16,0,0q�#��[ $��添加��$
��Left=����� *+)DataContext.AddProjectProcessBonusCommand+ �� "  ������ �� $��1��$��Right=�$��16q�.+��� #��[ $��保存������ *SaveBonusCommand+  $��预警设置��.��  $��16q�.+�  $ Vertical=�$ 10Q�.+��� $
 Left=�$! 开始预警天数（天）��#��[ $	��240������ *-+EarlyWarnings.StartWarningDays.WarningValue+�� $
 Left=�$ 项目总量预警（%）��#��[ $	��240������ *.,EarlyWarnings.TotalCountWarning.WarningValue+�� $
 Left=�$! 项目进度预警（天）��#��[ $	��240������ *,*EarlyWarnings.ScheduleWarning.WarningValue+�� $
 Left=�$ 项目效率预警（%）��#��[ $	��240������ *.,EarlyWarnings.EfficiencyWarning.WarningValue+�� #��[ $��16q�$��保存��$��Right=�$��50������ *SaveEarlyWarningCommand+  $��项目实施设置��.��  $��16q�.+�  $ 10Q�.+�  $
 Left=�$ 计划完成时间��#��[ $	��200�� �� *Project.PlanEndDate+�� $
 Left=�$ 计划人天数��#��[ $	��200������ *Project.PlanPersonDays+�� $��保存��#��[ ���� *UpdateProjectPlanDataCommand+%  HandyControl.Controls.FlexPanel  $��0,16,0,0q�.+��� $��Center=�$��人员配置�� 	 TextBlockDefault#��[	 $��Center=��� $��配置��#��[ $��16,0,0,0q����� *OpenProjectStaffCommand+�� $��Center=�$��设备配置��#��[	 $��Center=�$��16,0,0,0q��� $��配置��#��[ $��16,0,0,0q����� *OpenProjectDeviceCommand+  $��附件��.��  $��16q�.+�  $ 10Q�.+��� $��选择文件������ *SelectFilesCommand+(  "HandyControl.Controls.TagContainer  -   $��
Container1��#��[ $
��auto��$	��800��$��10q����� *SelectedFiles+�� $��上传��$
��Left=�#��[ $��0,16,0,0q����� *UploadMultipleFilesCommand+'  !HandyControl.Controls.SimplePanel  $��0,16,0,0q�.+��  �� $��9* ��� .+�&   System.Windows.Controls.DataGrid  *  $HandyControl.Controls.DataGridAttach""   CanUnselectAllWithBlankArea$
" True��#   HeadersVisibility$	# All=�$	��Top=�$   SelectionMode$$ Single=�%   RowHeaderWidth$% 60Q�&   AutoGenerateColumns$& False������ *ProjectAttachments+' �� SelectedItem' �� *SelectAttachment+(   RowHeaderTemplate( �� .R��� ) d� 	IsChecked) �� *
IsSelected+ ��   DataGridRow*   Columns* 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn+   CanUserResize$+ False��,   
IsReadOnly$
, True��-   Header$- 文件名称��1  +System.Windows.Controls.DataGridBoundColumn.   Binding. �� *
FileName+  $+ False��$
, True��$- 文件类型��. �� *
FileType+  $+ False��$
, True��$- 上传日期��. �� *
UploadDate+4  .System.Windows.Controls.DataGridTemplateColumn  $- 下载��/   CellTemplate/ �� .R��� $��下载������ *'%DataContext.DownloadAttachmentCommand+ �� "   ����   $��1��$��0,16,0,0q�$��Right=�.+��� $��删除��#��[ ���� *RemoveAttachmentCommand+