   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Project.CommonProject.Common  5   	   '    !Project.Views.Windows.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /commonclr-namespace:Project.Common     5   	   S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	   -   5        $��loginWindow��5	   	     � Title$  登录��5   	   $	��600��6   $	��360��6!   $��
ExtraLight"�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�6    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��6   ��66   Z�  � 	Resources 5       �� 
TargetType   CloseButtonTemplate ��Name �� Triggers	 P� Property
 P� Value �� 
TargetName �� Property �� Value��%�  &	        �� 5   
   " ��6   .R�5      �� $
 back��6.   
������ ._�5      F� +����        � �  � �     � � �     	
�������$��1��6B   $��Center=�6V   $��Center=�6s   6�   5       5      P� 5      	 �@J�IsMouseOver$

 True��61   . �5      �� $
 back��6    � �� �	#22FFFFFF6Y   5      P� 5      	 � ��$

 True��6/   . �5      �� $
 back��6    � �� �	#44FFFFFF6Y   5      5      5      .��5      7  1HandyControl.Controls.TransitioningContentControl  .��5   
   (  "HandyControl.Controls.ScrollViewer     IsInertiaEnabled$
 True��6   .��5      �� $��5q�6   
������� �� CornerRadius$ 10��63    J� Effect 5       3  -System.Windows.Media.Effects.DropShadowEffect  5!         Color$
 Gray��6'      ShadowDepth$ 0Q�64      
BlurRadius$ 5Q�6D      Opacity$	 0.3Q�6S      	Direction$ 0Q�6a   ._�5#      �  � RowDefinitions 5$      �� 5%      $
��1.8* �6(   �� 5&      $��3* �6(   .+�5(      � -   5        .+�5)      �� ���#007DFA$ 	10,10,0,0��66   �� 5*      -   5        $	��Top=�5*   !   $��Right=�69   $��40��6U   $��30��6`   #��[  6l   5+   %   �� 5,      $��Bottom=�6%   $��0,0,0,30q�6@   .+�5-      �� 
�������$��Center=�68   $��Center=�6S   $ 50��6p   $��0,0,0,20q�6�   ._�5.   "   �� $	��100��6)   $	��100��65   ��5/   &   �� 50   *    �� ImageSource$ /Assets/Images/logo.png��65   6]   52   #   53      �� 54      $��项目管理平台��6(   $��Center=�66   
�������$��18(�6f   6v   55      56      � 58      $��1��6    59      �� 5:      6.   .+�5<      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 32Q�61   $��32q�6>   $��Center=�6J      ChildWrapping$
 Wrap=�6h   .+�5=      �� $	��250��6&   $��Center=�62   '  !HandyControl.Controls.InfoElement   Placeholder$ 请输入用户名��6O     TextBoxExtend#��[ 6s   ��6�   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�G� 5>      $	��250��6*   $ 请输入密码��66     PasswordBoxExtend#��[ 6Y   )  #Project.Common.PasswordBoxAssistant   BoundPassword 6�   �� *
Password+ �� Mode$ TwoWay=�$ PropertyChanged=�6�   �� 5@      $	��250��6%   $��Center=�61   $��登录��6N   $��0,16,0,0q�6[     ButtonPrimary#��[ 6m   ��6�   �� *LoginCommand+5A      5B      5C      5D      5E      5F      5G      