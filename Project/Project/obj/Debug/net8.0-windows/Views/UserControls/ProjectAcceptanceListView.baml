   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  5      :    4Project.Views.UserControls.ProjectAcceptanceListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Project.Views.UserControls     5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      .��5	      ��    RegionBrush#��[  6     �� CornerRadius$  10��67     EffectShadow4 J� Effect# [ 6I   $��16q�6q   ._�5
   
   7  1HandyControl.Controls.TransitioningContentControl  .��5      ,  &HandyControl.Controls.SimpleStackPanel  $��16q�6"   .+�5      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�6)   .+�5      c� $��Center=�6   $��Center=�69   .@�5      �� -   5        $��BtnSettings��5   !   $��验收��64     
ButtonInfo#��[ 6m   ��6A   �� *OpenAcceptanceUcCommand+6m   �� 5      $��申请归档��6!     ButtonPrimary#��[ 6Z   $��10,0,0,0q�6�   ��60   �� *AddPlaceOnFileCommand+6�   5      5        5      $ 10Q�6)   $��0,16,0,0q�66   .+�5      �� $	��200��6   '  !HandyControl.Controls.InfoElement   Placeholder$ 合同名或合同编号��6R     TextBoxExtend#��[ 6x   ��6-   �� *ProjectReqs.Content+6x   &   HandyControl.Controls.DatePicker  5         ShowClearButton$
 True��6$   $	��140��6J   $ 开始时间��6�   (  "System.Windows.Controls.DatePicker   SelectedDate 6V   �� *ProjectReqs.StartDate+6�     5      $
 True��6$   $	��140��6J   $ 结束时间��6�    6V   �� *ProjectReqs.EndDate+6�   5        5      $ 10Q�6)   $��0,16,0,0q�66   $��Center=�6H   .+�5        $��Center=�6*   $��20��6E   .+�5      �� $��主管:��6$   5      /  )HandyControl.Controls.AutoCompleteTextBox  5      $	��140��6-    �� 
IsEditable$
 True��6�   $ 主管��6�      AutoCompleteTextBoxBaseStyle#��[ 6�    �� DisplayMemberPath$ 	StaffName��6�   ��69   �� *BindingManagerList+ �� Text 6d   �� *SearchManagerText+6�   �� 5      $��查询��6   #��[ 6Q   ��6*   �� *LoadProjectCommand+6z   5        5      $ 10Q�6)   $��16q�66   .+�5       &   System.Windows.Controls.DataGrid  *	  $HandyControl.Controls.DataGridAttach"	 	  CanUnselectAllWithBlankArea$
	 True��6   
   HeadersVisibility$	
 All=�6T   $	��Top=�6l      AutoGenerateColumns$ False��6�      RowHeaderWidth$ 60Q�6�      SelectionMode$ Single=�5!   Y   ��6   �� *
Projects+ �� SelectedItem 60   �� *SelectedProject+   RowHeaderTemplate 5"      �� 5#      .R�5$   "   ��  d� 	IsChecked 6+   �� *
IsSelected+ �� RelativeSource ��  �� AncestorType DataGridRow5%         Columns 5'      0
  *System.Windows.Controls.DataGridTextColumn
  5(      ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��6]      
IsReadOnly$
 True��6s      Header$ 合同编号��6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Contract.ContractNumber+6�   
  5)      $ False��6Q   $
 True��6g   $ 项目名称��6y    61   �� *ProjectName+6y   
  5*      $ False��6M   $
 True��6c   $ 实施地址��6u    61   �� *	Address+6u   
  5+      $ False��6^   $
 True��6t   $ 主管��6�    61   �� *ProjectManager.StaffName+6�   
  5,      $ False��6q   $
 True��6�   $ 立项日期��6�    61   �� *
CreateTime+ �� StringFormat$ {0:yyyy-MM-dd}��6�   
  5-      $ False��6   $
 True��6�   $ 合同结束日期��6�    61   �� *Contract.ContractEndDate+$ {0:yyyy-MM-dd}��6�   
  5.      $ False��6p   $
 True��6�   $ 开工日期��6�    61   �� *	StartDate+$ {0:yyyy-MM-dd}��6�   
  5/      $ False��6u   $
 True��6�   $ 验收日期��6�    61   �� *AcceptanceDate+$ {0:yyyy-MM-dd}��6�   51      52        53      $ 10Q�6)   .+�54      &   HandyControl.Controls.Pagination  $��Bottom=�6_      IsJumpEnabled$
 True��6�      	PageIndex 6$   �� *PaginatedList.PageIndex+ �� Mode$ TwoWay=�   MaxPageCount 6z   �� *PaginatedList.TotalPages+   DataCountPerPage 6�   �� *PaginatedList.PageSize+,  &HandyControl.Interactivity.Interaction   Triggers 55      -  'HandyControl.Interactivity.EventTrigger  56         	EventName$ PageUpdated��6.   ,  &HandyControl.Interactivity.TriggerBase    Actions.  57   "   /  )HandyControl.Interactivity.EventToCommand  !   PassEventArgsToCommand$
! True��6[   "   Command" 64   �� *LoadProjectCommand+6[   58      5:      5;      5<      5=      5>      5?      