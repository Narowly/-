   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  5      1    +Project.Views.UserControls.DailyWorkSummary   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Project.Views.UserControls     5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      .��5
      ��    RegionBrush#��[  6     �� CornerRadius$  10��67     EffectShadow4 J� Effect# [ 6I   $��16q�6q   ._�5   
   7  1HandyControl.Controls.TransitioningContentControl  $��16q�6)   .��5      ,  &HandyControl.Controls.SimpleStackPanel  .+�5      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�6)   $��0,16,0,0q�66   .+�5        $��Center=�6*   $��20��6E   .+�5      �� $��项目:��6$   5      /  )HandyControl.Controls.AutoCompleteTextBox  5      $	��200��6-    �� DisplayMemberPath$
 Name��6�   ��6<   �� *ProjectNamesSource+ �� SelectedItem 6g   �� *SelectedProject+ �� Text 6�   �� *SearchProjectNameText+6�   &   HandyControl.Controls.DatePicker  5      '  !HandyControl.Controls.InfoElement   ShowClearButton$
 True��6$   $	��140��6J      Placeholder$ 开始时间��6}   (  "System.Windows.Controls.DatePicker   SelectedDate 6V   �� *Req.StartDate+6}     5      $
 True��6$   $	��140��6J   $ 结束时间��6{    6V   �� *Req.EndDate+6{   5        5      $ 10Q�6)   $��0,10,0,0q�66   .+�5        $��Center=�6*   $��20��6E   .+�5      �� $��主管:��6$   $��0,3,0,0q�6/   5        5      $	��140��6-   	 �� 
IsEditable$
	 True��6�   $ 主管��6�      AutoCompleteTextBoxBaseStyle#��[ 6�   $ 	StaffName��6�   ��69   �� *BindingManagerList+ 6d   �� *SearchManagerText+ 6  �� *SelectedManager+  5      $��Center=�6*   $��20��6E   .+�5      �� $��人员:��6$   $��0,3,0,0q�6/   5        5      $	��140��6-   $
	 True��6�   $ 主管��6�   #��[ 6�   $ 	StaffName��6�   ��69   �� *BindingStaffList+ 6b   �� *SearchStaffText+ 6	  �� *SelectedStaff+�� 5      $��查询��6     ButtonPrimary#��[ 6Z   ��6*   �� *LoadDailyWorkSummaryCommand+6Z   5        5       $ 10Q�6)   $��0,16,0,0q�66   .+�5!      &   System.Windows.Controls.DataGrid  *	  $HandyControl.Controls.DataGridAttach"
 	  CanUnselectAllWithBlankArea$

 True��6      HeadersVisibility$	 All=�6T   $	��Top=�6l      AutoGenerateColumns$ False��6�      RowHeaderWidth$ 60Q�6�      SelectionMode$ Single=�6�      CanUserResizeColumns$
 True��6�   $  ��5"   G   ��6   �� *PaginatedList.Items+   Columns 5(      0
  *System.Windows.Controls.DataGridTextColumn
  5)      ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��6O      
IsReadOnly$
 True��6e      Header$ 人员��6w   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *	StaffName+6w   
  5*      $ False��6O   $
 True��6e   $ 	身份证��6w    61   �� *	StaffCard+6w   
  5+      $ False��6Q   $
 True��6g   $ 项目名称��6y    61   �� *ProjectName+6y   
  5,      $ False��6Q   $
 True��6g   $ 工序��6y    61   �� *ProcessName+6y   
  5-      $ False��6N   $
 True��6d   $ 单位��6v    61   �� *
UnitName+6v   
  5.      $ False��6Q   $
 True��6g   $ 报量总量��6y    61   �� *SumWorkload+6y   50      51        52      $ 10Q�6)   $��0,16,0,0q�66   .+�53      &   HandyControl.Controls.Pagination  $��Bottom=�6[      IsJumpEnabled$
 True��6�      	PageIndex 6$   �� *Req.Pagination.Page+ �� Mode$ TwoWay=�   MaxPageCount 6v   �� *PaginatedList.TotalPages+   DataCountPerPage 6�   �� *PaginatedList.PageSize+,  &HandyControl.Interactivity.Interaction   Triggers 54      -  'HandyControl.Interactivity.EventTrigger  55         	EventName$ PageUpdated��6.   ,  &HandyControl.Interactivity.TriggerBase   Actions. 56   "   /  )HandyControl.Interactivity.EventToCommand     PassEventArgsToCommand$
 True��6d      Command 64   �� *LoadDailyWorkSummaryCommand+6d   57      59      5:      5;      5<      5=      5>      