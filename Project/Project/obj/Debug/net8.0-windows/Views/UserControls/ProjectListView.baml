   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Project.Views.UserControlsProject.Views.UserControls  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      0    *Project.Views.UserControls.ProjectListView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Project.Views.UserControls     5      S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      0sys&clr-namespace:System;assembly=mscorlib  5         RegionBrush#��[  5
        �� CornerRadius$  10��68     EffectShadow4 J� Effect# [ 6J   $��16q�6r   ._�5      7  1HandyControl.Controls.TransitioningContentControl  .��5   
   ,  &HandyControl.Controls.SimpleStackPanel  $��16q�6   .+�5      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 10Q�6%   .+�5      c� $��Center=�6   $��Center=�65   .@�5      �� -   5        $��BtnSettings��5      $��设置��60     
ButtonInfo#��[ 6g   ��6=   �� *OpenSettingsUcCommand+6g   5      5        5      $ 10Q�6%   $��0,16,0,0q�62   .+�5        $��Center=�6&   $��20��6A   .+�5      �� $��合同:��6    5      /  )HandyControl.Controls.AutoCompleteTextBox  5      $	��200��6)    �� DisplayMemberPath$
 Name��6�   ��68   �� *ProjectNamesSource+ �� Text 6c   �� *SearchProjectNameText+6�   &   HandyControl.Controls.DatePicker  5      '  !HandyControl.Controls.InfoElement   ShowClearButton$
 True��6    $	��140��6F      Placeholder$ 开始时间��6�   (  "System.Windows.Controls.DatePicker   SelectedDate 6R   �� *ProjectReqs.StartDate+6�     5      $
 True��6    $	��140��6F   $ 结束时间��6    6R   �� *ProjectReqs.EndDate+6   5        5      $ 10Q�6%   $��0,10,0,0q�62   .+�5        $��Center=�6&   $��20��6A   .+�5      �� $��主管:��6    5        5      $	��140��6)    �� 
IsEditable$
 True��6�   $ 主管��6�      AutoCompleteTextBoxBaseStyle#��[ 6�   $ 	StaffName��6�   ��65   �� *BindingManagerList+ 6`   �� *SearchManagerText+	 �� SelectedItem	 6	  �� *SelectedManager+�� 5       $��查询��6     ButtonPrimary#��[ 6M   ��6&   �� *LoadProjectCommand+6v   5!        5"      $ 10Q�6%   $��0,16,0,0q�62   .+�5#      &   System.Windows.Controls.DataGrid  *	  $HandyControl.Controls.DataGridAttach"
 	  CanUnselectAllWithBlankArea$

 True��6      HeadersVisibility$	 All=�6P   $	��Top=�6h      AutoGenerateColumns$ False��6�      RowHeaderWidth$ 60Q�6�   ��5$      �� *
Projects+	 6<   �� *SelectedProject+   RowHeaderTemplate 5%      �� 5&      .R�5'      ��  d� 	IsChecked 6'   �� *
IsSelected+ �� RelativeSource ��  �� AncestorType DataGridRow5(         Columns 5*      0
  *System.Windows.Controls.DataGridTextColumn
  5+      ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��6Y      
IsReadOnly$
 True��6o      Header$ 合同编号��6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *Contract.ContractNumber+6�   
  5,      $ False��6M   $
 True��6c   $ 项目名称��6u    6-   �� *ProjectName+6u   
  5-      $ False��6I   $
 True��6_   $ 实施地址��6q    6-   �� *	Address+6q   
  5.      $ False��6Z   $
 True��6p   $ 主管��6�    6-   �� *ProjectManager.StaffName+6�   
  5/      $ False��6m   $
 True��6�   $ 立项日期��6�    6-   �� *
CreateTime+ �� StringFormat$ {0:yyyy-MM-dd}��6�   
  50      $ False��6{   $
 True��6�   $ 合同结束日期��6�    6-   �� *Contract.ContractEndDate+$ {0:yyyy-MM-dd}��6�   
  51      $ False��6l   $
 True��6�   $ 开工日期��6�    6-   �� *	StartDate+$ {0:yyyy-MM-dd}��6�   53      54        55      $ 10Q�6%   $��0,16,0,0q�62   .+�56      &   HandyControl.Controls.Pagination  $��Bottom=�6[      IsJumpEnabled$
 True��6�      	PageIndex 6    �� *PaginatedList.PageIndex+ �� Mode$ TwoWay=�   MaxPageCount 6v   �� *PaginatedList.TotalPages+   DataCountPerPage 6�   �� *PaginatedList.PageSize+,  &HandyControl.Interactivity.Interaction   Triggers 57      -  'HandyControl.Interactivity.EventTrigger  58         	EventName$ PageUpdated��6*   ,  &HandyControl.Interactivity.TriggerBase   Actions. 59      /  )HandyControl.Interactivity.EventToCommand      PassEventArgsToCommand$
  True��6W   !   Command! 60   �� *LoadProjectCommand+6W   5:      5<      5=      5>      5?      5@      