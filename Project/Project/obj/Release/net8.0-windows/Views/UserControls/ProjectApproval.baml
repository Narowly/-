   M S B A M L   `   `   `  ���� B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  0    *Project.Views.UserControls.ProjectApproval   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:Project.Views.UserControls     S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol     CloudDrawingBrush#��[    � 	Resources    boolToVisibility��%   &	       �� .����   RegionBrush#���   �� CornerRadius$ 10��  EffectShadow4 J� Effect# [ $��16q�._��  � RowDefinitions �� $��* ��� $
��Auto �.+�7  1HandyControl.Controls.TransitioningContentControl  .��(  "HandyControl.Controls.ScrollViewer     IsInertiaEnabled$
 True��.��,  &HandyControl.Controls.SimpleStackPanel  $��16q�.+�/  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�$��16q�.+��� ${�False��(  "HandyControl.Controls.TitleElement   TitlePlacement$
 Left=�   Title$ 合同编号:��  TextBoxExtend#��[ $	��200������ *!Project.Contract.ContractNumber+�� $
 Left=�$ 合同名称:��#��[ $	��200������ *Project.Contract.ContractName+(  "System.Windows.Controls.DatePicker  $	��200��$ 合同开始时间:��$
 Left=�  DatePickerExtend#��[    SelectedDate �� *$"Project.Contract.ContractStartDate+  $	��200��$ 合同结束时间:��$
 Left=�#��[  �� *" Project.Contract.ContractEndDate+  $ 10Q�$��16q�.+��� $
 Left=�$ 项目名称:��#��[ $	��200������ *Project.ProjectName+�� $
 Left=�$ 实施地址:��#��[ $	��200������ *Project.Address+  $ 10Q�$��16q�.+��� 	 �� DisplayMemberPath$	 ContactName��$	��200��$
 Left=�$ 
联系人:��  ComboBoxExtend#��[ 
 �� Text$
  ������ *CustomerContacts+ �� SelectedItem �� *" Project.Contract.CustomerContact+�� $
 Left=�$ 联系方式：��#��[ $	��200������ *)'Project.Contract.CustomerContact.Mobile+�� $	 	DictLabel��$	��200��$
 Left=�$ 区域:��#��[ $
  �� �� 
IsEditable$
 True������ *BindingRegionList+ �� *SelectedRegion+,  &HandyControl.Interactivity.Interaction   Triggers -  'HandyControl.Interactivity.EventTrigger     	EventName$ SelectionChanged��,	  &HandyControl.Interactivity.TriggerBase 	  Actions. /
  )HandyControl.Interactivity.EventToCommand
   
  PassEventArgsToCommand$
 True�� 
  Command �� *RcomboxChangedCmd+�� $	 	DictLabel��$	��200��$
 Left=�$  ��#��[ $
  ��$
 True������ *SubBindingRegionList+ �� *SelectedSubRegion+y��� *SubRegionVisiable+ �� 	Converter# [  �� ConverterParameter$
 true��   $ SelectionChanged��. 
  $
 True�� �� *SrComboxChangedCmd+  $ 10Q�$��16q�.+��� $��销售主管:��$��Center=�$��20��$��Center=�/  )HandyControl.Controls.AutoCompleteTextBox  $	��200��$	 	StaffName������ *	SalesList+ �� *Project.SalesManager+
 �� *SearchSalesText+�� $
 Left=�$ 联系方式：��#��[ $	��200������ *!Project.SalesManager.StaffPhone+�� $��项目主管:��$��Center=�$��20��$��Center=�  $	��200��$	 	StaffName������ *BindingManagerStaffs+ �� *Project.ProjectManager+
 �� *SearchManagerText+�� $
 Left=�$ 联系方式：��#��[ $	��200������ *#!Project.ProjectManager.StaffPhone+  $ 32Q�   Orientation$ Vertical=�$��16q�.+�  .+��� $��预估项目总量：��$��5q�$	��Top=�  TextBlockDefault#��[ �� $��16,0,0,0q�$��配置��  ButtonPrimary#��[ ���� *OpenProcessUnitCmd+�� $��付款条件：��$��16,5,0,0q�$��Center=�#��[ �� $��16,0,0,0q�$��配置��#��[ ���� #  HandyControl.Controls.Divider     LineStrokeDashArray$	 2,2R��� $��1�� �� Orientation$ 
Horizontal=�$��Right=�.+��� $��确认��$��
16,16,0,16q�#��[ ���� *SaveProjectCommand+�� $��确认并设置��$��
16,16,0,16q� 	 
ButtonInfo#��[	 ���� *#!SaveProjectAndOpenSettingsCommand+�� -   $��	BtnCancel��$��取消��$��16q�