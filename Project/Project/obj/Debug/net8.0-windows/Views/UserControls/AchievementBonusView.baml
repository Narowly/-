   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  5      5    /Project.Views.UserControls.AchievementBonusView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Project.Views.UserControls     5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      .��5
      ��    RegionBrush#��[  6     �� CornerRadius$  10��67     EffectShadow4 J� Effect# [ 6I   $��16q�6q   ._�5   
   7  1HandyControl.Controls.TransitioningContentControl  $��16q�6)   .��5      (  "HandyControl.Controls.ScrollViewer     IsInertiaEnabled$
 True��6   .  (HandyControl.Controls.ScrollViewerAttach   Orientation$ Vertical=�66   $��Hidden=�6c   $
��Auto=�6�   .��5      ,  &HandyControl.Controls.SimpleStackPanel  .+�5      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�6-   .+�5        $��Center=�6.   $��20��6I   .+�5      �� $��项目:��6(   5      /  )HandyControl.Controls.AutoCompleteTextBox  5      $	��200��61    �� DisplayMemberPath$
 Name��6�   ��6@   �� *ProjectNamesSource+ �� SelectedItem 6k   �� *SelectedProject+ �� Text 6�   �� *SearchProjectNameText+6�   �� 5      $	��150��6"     TextBoxExtend#��[ 6L   (  "HandyControl.Controls.TitleElement   TitlePlacement$
 Left=�6s   	   Title$	 	年月：��6�   '  !HandyControl.Controls.InfoElement
   Placeholder$
 格式：2024-08��6�   ��61   �� *	YearMonth+6�     5      $��Center=�6.   $��20��6I   .+�5      �� $��员工:��6(   $��0,3,0,0q�63   5        5      $	��140��61      AutoCompleteTextBoxBaseStyle#��[ 6�   $ 	StaffName��6�   ��6=   �� *BindingStaffList+ 6f   �� *SearchStaffText+ 6�   �� *SelectedStaff+�� 5      $��查询��6!     ButtonPrimary#��[ 6c   ��6.   �� *" AchievementBonusCalculateCommand+6c   5        5      $ 10Q�6-   $��0,16,0,0q�6:   .+�5      (	  "System.Windows.Controls.DatePicker	  $	��200��6%   $
 Left=�6\     DatePickerExtend#��[ 6�   $	 开始日期：��6�    	  SelectedDate 64   �� *Req.StartDate+6�   	  5      $	��200��6%   $
 Left=�6Z   #��[ 6   $	 结束日期：��6�    64   �� *Req.EndDate+6�   5        5      $ 10Q�6-   $��0,16,0,0q�6:   .+�5      &
   System.Windows.Controls.DataGrid
  *  $HandyControl.Controls.DataGridAttach"   CanUnselectAllWithBlankArea$
 True��6#    
  HeadersVisibility$	 All=�6X   $	��Top=�6p    
  AutoGenerateColumns$ False��6�    
  RowHeaderWidth$ 60Q�6�    
  SelectionMode$ Single=�6�    
  CanUserResizeColumns$
 True��6�   ��5       �� *	BonusList+ 6;   �� *SelectedDto+ 
  RowHeaderTemplate 5!      �� 5"   "   .R�5#   &   ��  d� 	IsChecked 6/   �� *
IsSelected+ �� RelativeSource ��  �� AncestorType DataGridRow5$   #    
  Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'   "   ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��6U      
IsReadOnly$
 True��6k      Header$ 项目名称��6}   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *ProjectName+6}     5(   "   $ False��6S   $
 True��6i   $ 员工��6{    65   �� *	StaffName+6{     5)   "   $ False��6Y   $
 True��6o   $ 工序��6�    65   �� *ProcessShowName+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5*   "   $ 平均日工作量��69      MaxWidth$	 500Q�6I      CellTemplate 5+   &   �� 5,   *   .R�5-   .   �� ��68   �� *AverageWorkloadPerDay+ � ToolTip 5.   2   _� 5/   6   .��50   :   �� ��6G   �� *	BonusList+��51   >   �� 52   B   .R�53   F     .+�54   J     $ 10Q�6a   $��0,8,0,0q�6n   .+�55   N   ��   TextBlockDefault#��[ 6X   $��工作量：��6�   �� 56   N   #��[ 6X   ��6�   �� *
Workload+�� 57   N   #��[ 6X   $��	奖金：��6�   �� 58   N   #��[ 6X   ��6�   �� *Bonus+ �� StringFormat$ F2��59   K   5:   G   5;   C   5=   ;   5>   7   5@   /   5A   +   5C   #     5D   "   $ False��6U   $
 True��6k   $ 总工作量��6}    65   �� *SumWorkload+6}     5E   "   $ False��6R   $
 True��6h   $ 天数��6z    65   �� *
UsedDays+6z     5F   "   $ False��6c   $
 True��6y   $ 绩效奖金��6�    65   �� *
SumBonus+$ F2��6�   5H      5I      5J      5K      5L      5M      5N      