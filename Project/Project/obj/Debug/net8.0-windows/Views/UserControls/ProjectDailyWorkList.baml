   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  5      5    /Project.Views.UserControls.ProjectDailyWorkList   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Project.Views.UserControls     5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      .��5
      ��    RegionBrush#��[  6     �� CornerRadius$  10��67     EffectShadow4 J� Effect# [ 6I   $��16q�6q   ._�5   
   7  1HandyControl.Controls.TransitioningContentControl  .��5      ,  &HandyControl.Controls.SimpleStackPanel  $��16q�6"   .+�5      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�6)   .+�5      c� $��Center=�6   $��Center=�69   .@�5      �� $��补录��6!   $��0��6W     ButtonPrimary#��[ 6m   ��6.   �� *OpenAddWindowCommand+6m   �� 5      $��修改��6!   $��1��6W     ButtonWarning#��[ 6l   $��10,0,0,0q�6�   ��6.   �� *OpenAddWindowCommand+6�   �� 5      $��批量导入��6!   #��[ 6[   $��10,0,0,0q�6�   ��60   �� *OpenBatchWindowCommand+6�   �� 5      $��excel导入��6!   #��[ 6\   $��10,0,0,0q�6�   ��63   �� *OpenExcelFileCommand+6�   5      5        5      $ 10Q�6)   $��0,16,0,0q�66   .+�5        $��Center=�6*   $��20��6E   .+�5      �� $��项目:��6$   5      /  )HandyControl.Controls.AutoCompleteTextBox  5      $	��200��6-    �� DisplayMemberPath$
 Name��6�   ��6<   �� *ProjectNamesSource+ �� SelectedItem 6g   �� *SelectedProject+ �� Text 6�   �� *SearchProjectNameText+6�   &   HandyControl.Controls.DatePicker  5      '  !HandyControl.Controls.InfoElement   ShowClearButton$
 True��6$   $	��140��6J      Placeholder$ 开始时间��6}   (  "System.Windows.Controls.DatePicker   SelectedDate 6V   �� *Req.StartDate+6}     5      $
 True��6$   $	��140��6J   $ 结束时间��6{    6V   �� *Req.EndDate+6{   5        5      $ 10Q�6)   $��0,10,0,0q�66   .+�5        $��Center=�6*   $��20��6E   .+�5      �� $��主管:��6$   $��0,3,0,0q�6/   5         5!      $	��140��6-   	 �� 
IsEditable$
	 True��6�   $ 主管��6�      AutoCompleteTextBoxBaseStyle#��[ 6�   $ 	StaffName��6�   ��69   �� *BindingManagerList+ 6d   �� *SearchManagerText+ 6  �� *SelectedManager+  5"      $��Center=�6*   $��20��6E   .+�5#      �� $��人员:��6$   $��0,3,0,0q�6/   5$        5%      $	��140��6-   $
	 True��6�   $ 主管��6�   #��[ 6�   $ 	StaffName��6�   ��69   �� *BindingStaffList+ 6b   �� *SearchStaffText+ 6	  �� *SelectedStaff+�� 5&      $��查询��6   #��[ 6Z   ��6*   �� *LoadProjectDailyWorkCommand+6Z   5'        5(      $ 10Q�6)   $��0,16,0,0q�66   .+�5)      &   System.Windows.Controls.DataGrid  *	  $HandyControl.Controls.DataGridAttach"
 	  CanUnselectAllWithBlankArea$

 True��6      HeadersVisibility$	 All=�6T   $	��Top=�6l      AutoGenerateColumns$ False��6�      RowHeaderWidth$ 60Q�6�      SelectionMode$ Single=�6�      CanUserResizeColumns$
 True��6�   ��5*      �� *PaginatedList.Items+ 6G   �� *SelectedDailyWork+   RowHeaderTemplate 5+      �� 5,      .R�5-   "   ��  d� 	IsChecked 6+   �� *
IsSelected+ �� RelativeSource ��  �� AncestorType DataGridRow5.         Columns 50      0
  *System.Windows.Controls.DataGridTextColumn
  51      ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��6h      
IsReadOnly$
 True��6~      Header$ 项目名称��6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *$"ProjectProcess.Project.ProjectName+6�   
  52      $ False��6u   $
 True��6�   $ 主管��6�    61   �� *1/ProjectProcess.Project.ProjectManager.StaffName+6�   
  53      $ False��6U   $
 True��6k   $ 员工��6}    61   �� *Staff.StaffName+6}   
  54      $ False��6t   $
 True��6�   $ 工序��6�    61   �� *0.ProjectProcess.ProcessUnit.Process.ProcessName+6�   
  55      $ False��6q   $
 True��6�   $ 单位��6�    61   �� *-+ProjectProcess.ProcessUnit.ProUnit.UnitName+6�   
  56      $ False��6N   $
 True��6d   $ 报量��6v    61   �� *
Workload+6v   
  57      $ False��6[   $
 True��6q   $ 报量标准��6�    61   �� *DailyWorkloadStandard+6�   
  58      $ False��6U   $
 True��6k   $ 是否达标��6}    61   �� *WorkloadQualify+6}   
  59      $ False��6N   $
 True��6d   $ 报量日期��6v    61   �� *
BillDate+6v   
  5:      $ False��6U   $
 True��6k   $ 身份证号��6}    61   �� *Staff.StaffCard+6}   4  .System.Windows.Controls.DataGridTemplateColumn  5;      $ 备注��65      MaxWidth$	 500Q�6A      CellTemplate 5<   "   �� 5=   &   .R�5>   *   �� ��64   �� *	Remarks+ � ToolTip 5?   .   _� 5@   2   .��5A   6   �� $
��Wrap=�6Y   $	��500��6m   ��6@   �� *	Remarks+6m   5B   3   5D   +   5E   '   5G      5I      5J        5K      $ 10Q�6)   $��0,16,0,0q�66   .+�5L      &   HandyControl.Controls.Pagination  $��Bottom=�6[      IsJumpEnabled$
 True��6�      	PageIndex 6$   �� *Req.Pagination.Page+ �� Mode$ TwoWay=�   MaxPageCount 6v   �� *PaginatedList.TotalPages+    DataCountPerPage  6�   �� *PaginatedList.PageSize+,  &HandyControl.Interactivity.Interaction!   Triggers! 5M      -  'HandyControl.Interactivity.EventTrigger  5N      "   	EventName$" PageUpdated��6.   ,  &HandyControl.Interactivity.TriggerBase#   Actions.# 5O   "   /  )HandyControl.Interactivity.EventToCommand  $   PassEventArgsToCommand$
$ True��6d   %   Command% 64   �� *LoadProjectDailyWorkCommand+6d   5P      5R      5S      5T      5U      5V      5W      