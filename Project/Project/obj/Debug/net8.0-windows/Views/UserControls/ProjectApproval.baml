   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  5   	   0    *Project.Views.UserControls.ProjectApproval   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Project.Views.UserControls     5   	   S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	      CloudDrawingBrush#��[  5   	     � 	Resources  5
        boolToVisibility��%   &	       �� 5   
   6'   5      .��5      ��   RegionBrush#���  6    �� CornerRadius$ 10��68     EffectShadow4 J� Effect# [ 6J   $��16q�6r   ._�5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      7  1HandyControl.Controls.TransitioningContentControl  .��5      (  "HandyControl.Controls.ScrollViewer     IsInertiaEnabled$
 True��6"   .��5      ,  &HandyControl.Controls.SimpleStackPanel  $��16q�6*   .+�5      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�61   $��16q�6>   .+�5      �� ${�False��6W   (  "HandyControl.Controls.TitleElement   TitlePlacement$
 Left=�6i      Title$ 合同编号:��5        TextBoxExtend#��[ 6:   $	��200��6a   ��5   &   �� *!Project.Contract.ContractNumber+5   a   �� 5      $
 Left=�6U   $ 合同名称:��5      #��[ 6:   $	��200��6a   ��5   &   �� *Project.Contract.ContractName+5   a   (  "System.Windows.Controls.DatePicker  5      $	��200��6e   $ 合同开始时间:��5      $
 Left=�6<     DatePickerExtend#��[ 6a      SelectedDate 5   )   �� *$"Project.Contract.ContractStartDate+5   a     5       $	��200��6c   $ 合同结束时间:��5!      $
 Left=�6<   #��[ 6a    5    )   �� *" Project.Contract.ContractEndDate+5!   a   5"        5#      $ 10Q�61   $��16q�6>   .+�5$      �� $
 Left=�6K   $ 项目名称:��5%      #��[ 62   $	��200��6Y   ��5$   &   �� *Project.ProjectName+5%   Y   �� 5&      $
 Left=�6G   $ 实施地址:��5'      #��[ 62   $	��200��6Y   ��5&   &   �� *Project.Address+5'   Y   5)        5*      $ 10Q�61   $��16q�6>   .+�5+      �� 	 �� DisplayMemberPath$	 ContactName��6P   $	��200��6p   $
 Left=�6   $ 
联系人:��5,   !     ComboBoxExtend#��[ 6=   
 �� Text$
  ��6e   ��5+   '   �� *CustomerContacts+ �� SelectedItem 5,   m   �� *" Project.Contract.CustomerContact+5-      �� 5.      $
 Left=�6_   $ 联系方式：��6�   #��[ 5/   &   $	��200��6M   ��5.   &   �� *)'Project.Contract.CustomerContact.Mobile+5/   M   �� 51      $	 	DictLabel��6y   $	��200��6�   $
 Left=�6�   $ 区域:��52   !   #��[ 6<   $
  ��6d    �� 
IsEditable$
 True��6l   ��51   '   �� *BindingRegionList+ 6Q   �� *SelectedRegion+,  &HandyControl.Interactivity.Interaction   Triggers 53   "   -  'HandyControl.Interactivity.EventTrigger  54   &      	EventName$ SelectionChanged��66   ,	  &HandyControl.Interactivity.TriggerBase 	  Actions. 55   *   /
  )HandyControl.Interactivity.EventToCommand
   
  PassEventArgsToCommand$
 True��6b    
  Command 6<   �� *RcomboxChangedCmd+6b   56   '   58      �� 59      $	 	DictLabel��6   $	��200��6�   $
 Left=�6�   $  ��5:   !   #��[ 69   $
  ��6a   $
 True��6i   ��59   '   �� *SubBindingRegionList+ 6T   �� *SelectedSubRegion+y�5;   !   �� *SubRegionVisiable+ �� 	Converter# [  �� ConverterParameter$
 true�� 5<   "     5=   &   $ SelectionChanged��66   . 5>   *   
  $
 True��6c    6<   �� *SrComboxChangedCmd+6c   5?   '   5A      5C        5D      $ 10Q�61   $��16q�6>   .+�5E      �� $��销售主管:��6(   $��Center=�65   $��20��6P   $��Center=�6\   6{   /  )HandyControl.Controls.AutoCompleteTextBox  5F      $	��200��65   $	 	StaffName��6f   ��6D   �� *	SalesList+ 6�   �� *Project.SalesManager+
 5G   !   �� *SearchSalesText+�� 5H      $
 Left=�6W   $ 联系方式：��5I   !   #��[ 6>   $	��200��6e   ��5H   &   �� *!Project.SalesManager.StaffPhone+5I   e   �� 5J      $��项目主管:��6(   $��Center=�65   $��20��6P   $��Center=�6\   6{     5K      $	��200��65   $	 	StaffName��6q   ��6D   �� *BindingManagerStaffs+ 6�   �� *Project.ProjectManager+
 5L   !   �� *SearchManagerText+�� 5M      $
 Left=�6Y   $ 联系方式：��5N   !   #��[ 6>   $	��200��6e   ��5M   &   �� *#!Project.ProjectManager.StaffPhone+5N   e   5O        5P      $ 32Q�61      Orientation$ Vertical=�6>   $��16q�6U   .+�5Q        .+�5R   "   �� $��预估项目总量：��6,   $��5q�6;   $	��Top=�6F     TextBlockDefault#��[ 6^   �� 5S   "   $��16,0,0,0q�6)   $��配置��6;     ButtonPrimary#��[ 6o   ��6H   �� *OpenProcessUnitCmd+6o   �� 5X   "   $��付款条件：��6,   $��16,5,0,0q�69   $��Center=�6K   #��[ 6f   �� 5Y   "   $��16,0,0,0q�6)   $��配置��6;   #��[ 6\   ��6H   �� 6\   5Z      5[      #  HandyControl.Controls.Divider  5\         LineStrokeDashArray$	 2,2R�6%   5]      5^      5_      �� 5`      $��1��6    �� Orientation$ 
Horizontal=�6&   $��Right=�6?   .+�5b      �� $��确认��6   $��
16,16,0,16q�6&   #��[ 6:   ��6a   �� *SaveProjectCommand+�� 5c      $��确认并设置��6   $��
16,16,0,16q�6)    	 
ButtonInfo#��[	 6=   ��6a   �� *#!SaveProjectAndOpenSettingsCommand+�� 5e      -   5        $��	BtnCancel��5e   &   $��取消��6   $��16q�67   6C   5f      5g      5i      5j      