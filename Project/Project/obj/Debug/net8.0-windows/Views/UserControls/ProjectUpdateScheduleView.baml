   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  5      :    4Project.Views.UserControls.ProjectUpdateScheduleView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Project.Views.UserControls     5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      .��5
      ��    RegionBrush#��[  6     �� CornerRadius$  10��67     EffectShadow4 J� Effect# [ 6I   $��16q�6q   ._�5   
   7  1HandyControl.Controls.TransitioningContentControl  $��16q�6)   .��5      ,  &HandyControl.Controls.SimpleStackPanel  .+�5      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�6)   .+�5      c� $��Center=�6   $��Center=�69   .@�5      �� $��修改��6!     ButtonWarning#��[ 6W   $��10,0,0,0q�6~   ��6.   �� *OpenAddWindowCommand+6~   �� 5      $��查看历史��6!     
ButtonInfo#��[ 6]   $��10,0,0,0q�6�   ��60   �� *OpenHistoryWindowCommand+6�   5      5        5      $ 10Q�6)   $��0,16,0,0q�66   .+�5        $��Center=�6*   $��20��6E   .+�5      �� $��项目:��6$   5      /  )HandyControl.Controls.AutoCompleteTextBox  5      $	��200��6-    �� DisplayMemberPath$
 Name��6�   ��6<   �� *ProjectNamesSource+ �� SelectedItem 6g   �� *SelectedProject+ �� Text 6�   �� *SearchProjectNameText+6�   (  "System.Windows.Controls.DatePicker  5      $	��200��6!   (  "HandyControl.Controls.TitleElement   TitlePlacement$
 Left=�6X     DatePickerExtend#��[ 6}      Title$ 开始日期：��6�      SelectedDate 60   �� *Req.StartDate+6�     5      $	��200��6!   $
 Left=�6V   #��[ 6{   $ 结束日期：��6�    60   �� *Req.EndDate+6�   5        5      $ 10Q�6)   $��0,10,0,0q�66   .+�5        $��Center=�6*   $��20��6E   .+�5      �� $��主管:��6$   $��0,3,0,0q�6/   5        5      $	��140��6-   	 �� 
IsEditable$
	 True��6�   '  !HandyControl.Controls.InfoElement
   Placeholder$
 主管��6�      AutoCompleteTextBoxBaseStyle#��[ 6�   $ 	StaffName��6�   ��69   �� *BindingManagerList+ 6d   �� *SearchManagerText+ 6  �� *SelectedManager+�� 5       $
 Text��6F    �� SelectedValuePath$ Value��6_   $
 Left=�6�     ComboBoxExtend#��[ 6�   $ 快捷查询：��6�   $	��200��6  ��6   �� *SearchTypeList+ �� SelectedValue 6y   �� *
SearchType+6  �� 5!      $��查询��6     ButtonPrimary#��[ 6R   ��6*   �� *LoadProjectsCommand+6R   5"        5#      $ 10Q�6)   $��0,16,0,0q�66   .+�5$      &   System.Windows.Controls.DataGrid  *	  $HandyControl.Controls.DataGridAttach" 	  CanUnselectAllWithBlankArea$
 True��6      HeadersVisibility$	 All=�6T   $	��Top=�6l      AutoGenerateColumns$ False��6�      RowHeaderWidth$ 60Q�6�      SelectionMode$ Single=�6�      CanUserResizeColumns$
 True��6�   ��5%      �� *PaginatedList.Items+ 6E   �� *SelectedProjectData+   RowHeaderTemplate 5&      �� 5'      .R�5(   "   ��  d� 	IsChecked 6+   �� *
IsSelected+ �� RelativeSource ��  �� AncestorType DataGridRow5)         Columns 5+      0
  *System.Windows.Controls.DataGridTextColumn
  5,      ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��6Q      
IsReadOnly$
 True��6g      Header$ 项目名称��6y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ProjectName+6y   
  5-      $ False��6^   $
 True��6t   $ 主管��6�    61   �� *ProjectManager.StaffName+6�   
  5.      $ False��6O   $
 True��6e   $ 开工日期��6w    61   �� *	StartDate+6w   
  5/      $ False��6Q   $
 True��6g   $ 计划结束日期��6y    61   �� *PlanEndDate+6y   
  50      $ False��6^   $
 True��6t   $ 更改计划原因��6�    61   �� *LastUpdateScheduleReason+6�   52      53        54      $ 10Q�6)   $��0,16,0,0q�66   .+�55      &   HandyControl.Controls.Pagination  $��Bottom=�6[      IsJumpEnabled$
 True��6�      	PageIndex 6$   �� *Req.Pagination.Page+ �� Mode$ TwoWay=�   MaxPageCount 6v   �� *PaginatedList.TotalPages+    DataCountPerPage  6�   �� *PaginatedList.PageSize+,  &HandyControl.Interactivity.Interaction!   Triggers! 56      -  'HandyControl.Interactivity.EventTrigger  57      "   	EventName$" PageUpdated��6.   ,  &HandyControl.Interactivity.TriggerBase#   Actions.# 58   "   /  )HandyControl.Interactivity.EventToCommand  $   PassEventArgsToCommand$
$ True��6g   %   Command% 64   �� * LoadConsumableBoundListCommand+6g   59      5;      5<      5=      5>      5?      5@      