   M S B A M L   `   `   `  ���� B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  0    *Project.Views.UserControls.ProjectDynamics   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:Project.Views.UserControls     S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  .����    RegionBrush#��[    �� CornerRadius$  10��  EffectShadow4 J� Effect# [ $��16q�._�7  1HandyControl.Controls.TransitioningContentControl  .��,  &HandyControl.Controls.SimpleStackPanel  $��16q�.+�/  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�.+�  $��Center=�$��20��.+��� $��合同:��/  )HandyControl.Controls.AutoCompleteTextBox  $	��200�� �� DisplayMemberPath$
 Name������ *ProjectNamesSource+ �� Text �� *SearchProjectNameText+&   HandyControl.Controls.DatePicker  '  !HandyControl.Controls.InfoElement   ShowClearButton$
 True��$	��140��   Placeholder$ 开始时间��(  "System.Windows.Controls.DatePicker   SelectedDate �� *ProjectReqs.StartDate+  $
 True��$	��140��$ 结束时间�� �� *ProjectReqs.EndDate+  $ 10Q�$��0,10,0,0q�$��Center=�.+�  $��Center=�$��20��.+��� $��主管:��  $	��140�� �� 
IsEditable$
 True��$ 主管��   AutoCompleteTextBoxBaseStyle#��[ $ 	StaffName������ *BindingManagerList+ �� *SearchManagerText+	 �� SelectedItem	 �� *SelectedManager+�� $��查询��  ButtonPrimary#��[ ���� *LoadProjectCommand+  $ 10Q�$��0,16,0,0q�.+�&   System.Windows.Controls.DataGrid  *	  $HandyControl.Controls.DataGridAttach"
 	  CanUnselectAllWithBlankArea$

 True��   HeadersVisibility$	 All=�$	��Top=�   AutoGenerateColumns$ False��   RowHeaderWidth$ 60Q����� *
Projects+	 �� *SelectedProject+   RowHeaderTemplate �� .R���  d� 	IsChecked �� *
IsSelected+ �� RelativeSource ��  �� AncestorType DataGridRow   Columns 0
  *System.Windows.Controls.DataGridTextColumn
  ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��   
IsReadOnly$
 True��   Header$ 合同编号��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Contract.ContractNumber+
  $ False��$
 True��$ 	合同名�� �� *ProjectName+
  $ False��$
 True��$ 项目主管�� �� *ProjectManager.StaffName+
  $ False��$
 True��$ 项目开始日期�� �� *	StartDate+ �� StringFormat$ {0:yyyy-MM-dd}��
  $ False��$
 True��$ 计划结束日期�� �� *PlanEndDate+$ {0:yyyy-MM-dd}��
  $ False��$
 True��$ 	人员数�� �� *ProjectStaffCount+
  $ False��$
 True��$ 	设备数�� �� *ProjectDeviceCount+
  $ False��$
 True��$ 计划人天数�� �� *PlanPersonDays+
  $ False��$
 True��$ 已用人天数�� �� *
UsedDays+4  .System.Windows.Controls.DataGridTemplateColumn  $ 工作量进度��   CellTemplate �� .R��  � Minimum$ 0Q� � Maximum$	 100Q�$��20��$��80�� � Value �� *WorkloadPercentage+  $ 时间进度�� �� .R�� $ 0Q�$	 100Q�$��20��$��80�� �� *TimePercentage+  $ 10Q�$��0,16,0,0q�.+�&   HandyControl.Controls.Pagination  $��Bottom=�   IsJumpEnabled$
 True��   	PageIndex �� *PaginatedList.PageIndex+ �� Mode$ TwoWay=�   MaxPageCount �� *PaginatedList.TotalPages+    DataCountPerPage  �� *PaginatedList.PageSize+,  &HandyControl.Interactivity.Interaction!   Triggers! -  'HandyControl.Interactivity.EventTrigger  "   	EventName$" PageUpdated��,  &HandyControl.Interactivity.TriggerBase#   Actions.# /  )HandyControl.Interactivity.EventToCommand  $   PassEventArgsToCommand$
$ True��%   Command% �� *LoadProjectCommand+