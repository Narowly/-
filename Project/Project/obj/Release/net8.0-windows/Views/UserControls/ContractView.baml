   M S B A M L   `   `   `  ���� B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  -    'Project.Views.UserControls.ContractView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:Project.Views.UserControls     S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol     RegionBrush#��[    �� CornerRadius$  10��  EffectShadow4 J� Effect# [ $��16q� � 	Resources   boolToVisibility��%   &	       �� ._�7  1HandyControl.Controls.TransitioningContentControl  $��16q�.��,  &HandyControl.Controls.SimpleStackPanel  .+�/  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�.+�c� $��Center=�$��Center=�.@��� -   $��BtnApproval��$��立项��  
ButtonInfo#��[ ���� *OpenProjectApprovalCommand+  $ 10Q�$��0,16,0,0q�.+�  $��Center=�$��20��.+��� $��合同:��/  )HandyControl.Controls.AutoCompleteTextBox  $	��200�� �� DisplayMemberPath$
 Name������ *ProjectNamesSource+ �� Text �� *SearchProjectNameText+&   HandyControl.Controls.DatePicker  '  !HandyControl.Controls.InfoElement   ShowClearButton$
 True��$	��140��   Placeholder$ 开始时间��(  "System.Windows.Controls.DatePicker   SelectedDate �� *Req.StartDate+  $
 True��$	��140��$ 结束时间�� �� *Req.EndDate+�� $��查询��  ButtonPrimary#��[ ���� *SearchCommand+  $ 10Q�$��0,16,0,0q�.+��� $ 状态��  ComboBoxExtend#��[ $	��100��	 �� SelectedIndex$	 0��$
 Name������ *BindingStatus+
 �� SelectedItem
 �� *SelectedStatus+  $��Center=�$��20��.+��� $��主管:��  $	��140�� �� 
IsEditable$
 True��$ 主管��   AutoCompleteTextBoxBaseStyle#��[ $ 	StaffName������ *BindingManagerList+ �� *SearchManagerText+
 �� *SelectedManager+&   System.Windows.Controls.DataGrid  *	  $HandyControl.Controls.DataGridAttach" 	  CanUnselectAllWithBlankArea$
 True��   HeadersVisibility$	 All=�$	��Top=�   SelectionMode$ Single=�$��0,16,0,0q�   RowHeaderWidth$ 60Q�   AutoGenerateColumns$ False������ *	Contracts+
 �� *SelectedItem+   RowHeaderTemplate �� .R���  d� 	IsChecked �� *
IsSelected+ �� RelativeSource ��  �� AncestorType DataGridRow   Columns 0
  *System.Windows.Controls.DataGridTextColumn
  ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��   
IsReadOnly$
 True��   Header$ 合同编号��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ContractNumber+
  $ False��$
 True��$ 合同名称�� �� *ContractName+
  $ False��$
 True��$ 立项日期�� �� *
CreateTime+&   HandyControl.Controls.Pagination  $��Bottom=�$��0,16,0,0q�   IsJumpEnabled$
 True��   	PageIndex �� *CurrentIndex+ �� Mode$ TwoWay=�   MaxPageCount �� *Paginated.TotalPages+   DataCountPerPage �� *Paginated.PageSize+,  &HandyControl.Interactivity.Interaction   Triggers -  'HandyControl.Interactivity.EventTrigger      	EventName$  PageUpdated��,  &HandyControl.Interactivity.TriggerBase!   Actions.! /  )HandyControl.Interactivity.EventToCommand  "   PassEventArgsToCommand$
" True��#   Command# �� *PageSearchCommand+