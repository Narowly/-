   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  5      4    .Project.Views.UserControls.EarlyWarningHistory   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Project.Views.UserControls     5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      .��5	      ��    RegionBrush#��[  6     �� CornerRadius$  10��67     EffectShadow4 J� Effect# [ 6I   $��16q�6q   ._�5
   
   7  1HandyControl.Controls.TransitioningContentControl  .��5      ,  &HandyControl.Controls.SimpleStackPanel  $��16q�6"   .+�5      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�6)   .+�5      c� $��Center=�6   $��Center=�69   .@�5      �� -   5        $��BtnApproval��5   !   $��处理��64     
ButtonInfo#��[ 6p   ��6A   �� *OpenHandlingWarningCommand+6p   5      5        5      $ 10Q�6)   $��0,16,0,0q�66   .+�5      �� $	��200��6   '  !HandyControl.Controls.InfoElement   Placeholder$ 合同名或合同编号��6R     TextBoxExtend#��[ 6x   ��6-   �� *ProjectReqs.Content+6x   &   HandyControl.Controls.DatePicker  5         ShowClearButton$
 True��6$   $	��140��6J   $ 预警开始时间��6�   (  "System.Windows.Controls.DatePicker   SelectedDate 6V   �� *ProjectReqs.StartDate+6�     5      $
 True��6$   $	��140��6J   $ 预警结束时间��6�    6V   �� *ProjectReqs.EndDate+6�   5        5      $ 10Q�6)   $��0,10,0,0q�66   $��Center=�6H   .+�5        $��Center=�6*   $��20��6E   .+�5      �� $��主管:��6$   5      /  )HandyControl.Controls.AutoCompleteTextBox  5      $	��140��6-    �� 
IsEditable$
 True��6�   $ 主管��6�      AutoCompleteTextBoxBaseStyle#��[ 6�    �� DisplayMemberPath$ 	StaffName��6�   ��69   �� *BindingManagerList+ �� Text 6d   �� *SearchManagerText+6�   �� 5      $ 预警状态��6   $ 	DictLabel��6i     ComboBoxExtend#��[ 6�   $	��140��6�   ��6A   �� *WarningTypeList+	 �� SelectedItem	 6�   �� *SelectedWarningType+�� 5      $��查询��6     ButtonPrimary#��[ 6Q   ��6*   �� *LoadHistoryCommand+6z   5        5      $ 10Q�6)   $��0,16,0,0q�66   .+�5      &   System.Windows.Controls.DataGrid  *	  $HandyControl.Controls.DataGridAttach"
 	  CanUnselectAllWithBlankArea$

 True��6      HeadersVisibility$	 All=�6T   $	��Top=�6l      AutoGenerateColumns$ False��6�      RowHeaderWidth$ 60Q�6�   ��5       �� *EarlyWarnings+	 6?   �� *SelectedHistory+   RowHeaderTemplate 5!      �� 5"      .R�5#   "   ��  d� 	IsChecked 6+   �� *
IsSelected+ �� RelativeSource ��  �� AncestorType DataGridRow5$         Columns 5&      0
  *System.Windows.Controls.DataGridTextColumn
  5'      ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��6e      
IsReadOnly$
 True��6{      Header$ 合同编号��6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *!Project.Contract.ContractNumber+6�   
  5(      $ False��6Y   $
 True��6o   $ 	合同名��6�    61   �� *Project.ProjectName+6�   
  5)      $ False��6f   $
 True��6|   $ 项目主管��6�    61   �� *" Project.ProjectManager.StaffName+6�   
  5*      $ False��6_   $
 True��6u   $ 预警类型��6�    61   �� *WarningTypeData.DictLabel+6�   
  5+      $ False��6q   $
 True��6�   $ 预警日期��6�    61   �� *
CreateTime+ �� StringFormat$ {0:yyyy-MM-dd}��6�   
  5,      $ False��6Z   $
 True��6p   $ 处理状态��6�    61   �� *StatusData.DictLabel+6�   5.      5/        50      $ 10Q�6)   $��0,16,0,0q�66   .+�51      &   HandyControl.Controls.Pagination  $��Bottom=�6_      IsJumpEnabled$
 True��6�      	PageIndex 6$   �� *PaginatedList.PageIndex+ �� Mode$ TwoWay=�   MaxPageCount 6z   �� *PaginatedList.TotalPages+   DataCountPerPage 6�   �� *PaginatedList.PageSize+,  &HandyControl.Interactivity.Interaction   Triggers 52      -  'HandyControl.Interactivity.EventTrigger  53         	EventName$ PageUpdated��6.   ,  &HandyControl.Interactivity.TriggerBase   Actions. 54   "   /  )HandyControl.Interactivity.EventToCommand      PassEventArgsToCommand$
  True��6[   !   Command! 64   �� *LoadHistoryCommand+6[   55      57      58      59      5:      5;      5<      