   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  5      ;    5Project.Views.UserControls.ConsumableBoundHistoryView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Project.Views.UserControls     5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      .��5
      ��    RegionBrush#��[  6     �� CornerRadius$  10��67     EffectShadow4 J� Effect# [ 6I   $��16q�6q   ._�5   
   7  1HandyControl.Controls.TransitioningContentControl  $��16q�6)   .��5      ,  &HandyControl.Controls.SimpleStackPanel  .+�5      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�6)   .+�5      c� $��Center=�6   $��Center=�69   .@�5      �� $��修改��6!   $��1��6c     ButtonWarning#��[ 6x   $��10,0,0,0q�6�   ��6.   �� *" OpenUpdateConsumableBoundCommand+6�   5      5        5      $ 10Q�6)   $��0,16,0,0q�66   .+�5        $��Center=�6*   $��20��6E   .+�5      �� $��项目:��6$   5      /  )HandyControl.Controls.AutoCompleteTextBox  5      $	��200��6-    �� DisplayMemberPath$
 Name��6�   ��6<   �� *ProjectNamesSource+ �� SelectedItem 6g   �� *SelectedProject+ �� Text 6�   �� *SearchProjectNameText+6�   (  "System.Windows.Controls.DatePicker  5      $	��200��6!   (  "HandyControl.Controls.TitleElement   TitlePlacement$
 Left=�6X     DatePickerExtend#��[ 6}      Title$ 开始日期：��6�      SelectedDate 60   �� *Req.StartDate+6�     5      $	��200��6!   $
 Left=�6V   #��[ 6{   $ 结束日期：��6�    60   �� *Req.EndDate+6�   5        5      $ 10Q�6)   $��0,10,0,0q�66   .+�5        $��Center=�6*   $��20��6E   .+�5      �� $��主管:��6$   $��0,3,0,0q�6/   5        5      $	��140��6-   	 �� 
IsEditable$
	 True��6�   '  !HandyControl.Controls.InfoElement
   Placeholder$
 主管��6�      AutoCompleteTextBoxBaseStyle#��[ 6�   $ 	StaffName��6�   ��69   �� *BindingManagerList+ 6d   �� *SearchManagerText+ 6  �� *SelectedManager+�� 5      $	��200��6     TextBoxExtend#��[ 6-   $-
 '可输入设备类型、编号或型号��6T   ��6   �� *Req.Content+�� 5       $��查询��6     ButtonPrimary#��[ 6]   ��6*   �� * LoadConsumableBoundListCommand+6]   5!        5"      $ 10Q�6)   $��0,16,0,0q�66   .+�5#      &   System.Windows.Controls.DataGrid  *	  $HandyControl.Controls.DataGridAttach" 	  CanUnselectAllWithBlankArea$
 True��6      HeadersVisibility$	 All=�6T   $	��Top=�6l      AutoGenerateColumns$ False��6�      RowHeaderWidth$ 60Q�6�      SelectionMode$ Single=�6�      CanUserResizeColumns$
 True��6�   ��5$      �� *PaginatedList.Items+ 6E   �� *SelectedDto+   RowHeaderTemplate 5%      �� 5&      .R�5'   "   ��  d� 	IsChecked 6+   �� *
IsSelected+ �� RelativeSource ��  �� AncestorType DataGridRow5(         Columns 5*      0
  *System.Windows.Controls.DataGridTextColumn
  5+      ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��6V      
IsReadOnly$
 True��6l      Header$ 编号��6~   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ConsumableNumber+6~   
  5,      $ False��6X   $
 True��6n   $ 类型��6�    61   �� *ConsumableTypeName+6�   
  5-      $ False��6Y   $
 True��6o   $ 型号��6�    61   �� *ConsumableTypeModel+6�   
  5.      $ False��6J   $
 True��6`   $ 	出入库��6r    61   �� *Type+6r   
  5/      $ False��6N   $
 True��6d   $ 数量��6v    61   �� *
Quantity+6v   
  50      $ False��6O   $
 True��6e   $ 日期��6w    61   �� *	BoundDate+6w   
  51      $ False��6Q   $
 True��6g   $ 项目名称��6y    61   �� *ProjectName+6y   4  .System.Windows.Controls.DataGridTemplateColumn  52      $ 备注��65      MaxWidth$	 500Q�6A      CellTemplate 53   "   �� 54   &   .R�55   *   �� ��64   �� *	Remarks+ � ToolTip 56   .   _� 57   2   .��58   6   �� $
��Wrap=�6Y   $	��500��6m   ��6@   �� *	Remarks+6m   59   3   5;   +   5<   '   5>      5@      5A        5B      $ 10Q�6)   $��0,16,0,0q�66   .+�5C      &   HandyControl.Controls.Pagination  $��Bottom=�6[      IsJumpEnabled$
 True��6�      	PageIndex 6$   �� *Req.Pagination.Page+ �� Mode$ TwoWay=�    MaxPageCount  6v   �� *PaginatedList.TotalPages+!   DataCountPerPage! 6�   �� *PaginatedList.PageSize+,  &HandyControl.Interactivity.Interaction"   Triggers" 5D      -  'HandyControl.Interactivity.EventTrigger  5E      #   	EventName$# PageUpdated��6.   ,  &HandyControl.Interactivity.TriggerBase$   Actions.$ 5F   "   /  )HandyControl.Interactivity.EventToCommand  %   PassEventArgsToCommand$
% True��6g   &   Command& 64   �� * LoadConsumableBoundListCommand+6g   5G      5I      5J      5K      5L      5M      5N      