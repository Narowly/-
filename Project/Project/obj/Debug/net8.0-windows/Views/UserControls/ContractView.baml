   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  5      -    'Project.Views.UserControls.ContractView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Project.Views.UserControls     5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5         RegionBrush#��[  5	        �� CornerRadius$  10��68     EffectShadow4 J� Effect# [ 6J   $��16q�6r    � 	Resources 5
        boolToVisibility��%   &	       �� 5   
   6'   5      ._�5      7  1HandyControl.Controls.TransitioningContentControl  $��16q�6%   .��5   
   ,  &HandyControl.Controls.SimpleStackPanel  .+�5      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�6%   .+�5      c� $��Center=�6   $��Center=�65   .@�5      �� -   5        $��BtnApproval��5      $��立项��60     
ButtonInfo#��[ 6l   ��6=   �� *OpenProjectApprovalCommand+6l   5      5        5      $ 10Q�6%   $��0,16,0,0q�62   .+�5        $��Center=�6&   $��20��6A   .+�5      �� $��合同:��6    5      /  )HandyControl.Controls.AutoCompleteTextBox  5      $	��200��6)    �� DisplayMemberPath$
 Name��6�   ��68   �� *ProjectNamesSource+ �� Text 6c   �� *SearchProjectNameText+6�   &   HandyControl.Controls.DatePicker  5      '  !HandyControl.Controls.InfoElement   ShowClearButton$
 True��6    $	��140��6F      Placeholder$ 开始时间��6y   (  "System.Windows.Controls.DatePicker   SelectedDate 6R   �� *Req.StartDate+6y     5      $
 True��6    $	��140��6F   $ 结束时间��6w    6R   �� *Req.EndDate+6w   �� 5      $��查询��6     ButtonPrimary#��[ 6H   ��6&   �� *SearchCommand+6q   5        5      $ 10Q�6%   $��0,16,0,0q�62   .+�5      �� $ 状态��6     ComboBoxExtend#��[ 6;   $	��100��6c   	 �� SelectedIndex$	 0��6o   $
 Name��6�   ��6�   �� *BindingStatus+
 �� SelectedItem
 6�   �� *SelectedStatus+6�     5      $��Center=�6&   $��20��6A   .+�5       �� $��主管:��6    5!        5"      $	��140��6)    �� 
IsEditable$
 True��6�   $ 主管��6�      AutoCompleteTextBoxBaseStyle#��[ 6�   $ 	StaffName��6�   ��65   �� *BindingManagerList+ 6`   �� *SearchManagerText+
 6	  �� *SelectedManager+5#      &   System.Windows.Controls.DataGrid  5$      *	  $HandyControl.Controls.DataGridAttach" 	  CanUnselectAllWithBlankArea$
 True��6      HeadersVisibility$	 All=�6L   $	��Top=�6d      SelectionMode$ Single=�6|   $��0,16,0,0q�6�      RowHeaderWidth$ 60Q�5%         AutoGenerateColumns$ False��6+   ��6G   �� *	Contracts+
 6i   �� *SelectedItem+   RowHeaderTemplate 5&      �� 5'      .R�5(      ��  d� 	IsChecked 6#   �� *
IsSelected+ �� RelativeSource ��  �� AncestorType DataGridRow5)         Columns 5+      0
  *System.Windows.Controls.DataGridTextColumn
  5,      ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��6L      
IsReadOnly$
 True��6b      Header$ 合同编号��6t   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *ContractNumber+6t   
  5-      $ False��6J   $
 True��6`   $ 合同名称��6r    6)   �� *ContractName+6r   
  5.      $ False��6H   $
 True��6^   $ 立项日期��6p    6)   �� *
CreateTime+6p   50      &   HandyControl.Controls.Pagination  51      $��Bottom=�6K   $��0,16,0,0q�6�      IsJumpEnabled$
 True��6�      	PageIndex 6   �� *CurrentIndex+ �� Mode$ TwoWay=�   MaxPageCount 6f   �� *Paginated.TotalPages+   DataCountPerPage 6�   �� *Paginated.PageSize+,  &HandyControl.Interactivity.Interaction   Triggers 52      -  'HandyControl.Interactivity.EventTrigger  53          	EventName$  PageUpdated��6&   ,  &HandyControl.Interactivity.TriggerBase!   Actions.! 54      /  )HandyControl.Interactivity.EventToCommand  "   PassEventArgsToCommand$
" True��6R   #   Command# 6,   �� *PageSearchCommand+6R   55      57      58      59      5:      