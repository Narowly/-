   M S B A M L   `   `   `  ���� B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Project.CommonProject.Common  '    !Project.Views.Windows.LoginWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /commonclr-namespace:Project.Common     S OHandyControl, Version=3.5.1.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  -   $��loginWindow��  � Title$  登录��$	��600��$	��360��$��
ExtraLight"� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency$
 True����Z�  � 	Resources  �� 
TargetType   CloseButtonTemplate ��Name �� Triggers	 P� Property
 P� Value �� 
TargetName �� Property �� Value��%+  &	        �� " ��.R��� $
 back��
������ ._�F� +����        � �  � �     � � �     	
�������$��1��$��Center=�$��Center=� P� 	 �@J�IsMouseOver$

 True��. ��� $
 back�� � �� �	#22FFFFFFP� 	 � ��$

 True��. ��� $
 back�� � �� �	#44FFFFFF.��7  1HandyControl.Controls.TransitioningContentControl  .��(  "HandyControl.Controls.ScrollViewer     IsInertiaEnabled$
 True��.���� $��5q�
������� �� CornerRadius$ 10�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$
 Gray��   ShadowDepth$ 0Q�   
BlurRadius$ 5Q�   Opacity$	 0.3Q�   	Direction$ 0Q�._��  � RowDefinitions �� $
��1.8* ��� $��3* �.+�� -   .+��� ���#007DFA$ 	10,10,0,0���� -   $	��Top=�$��Right=�$��40��$��30��#��[  �� $��Bottom=�$��0,0,0,30q�.+��� 
�������$��Center=�$��Center=�$ 50��$��0,0,0,20q�._��� $	��100��$	��100������  �� ImageSource$ /Assets/Images/logo.png���� $��项目管理平台��$��Center=�
�������$��18(�� $��1�� �� .+�/  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 32Q�$��32q�$��Center=�   ChildWrapping$
 Wrap=�.+��� $	��250��$��Center=�'  !HandyControl.Controls.InfoElement   Placeholder$ 请输入用户名��  TextBoxExtend#��[ ���� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�G� $	��250��$ 请输入密码��  PasswordBoxExtend#��[ )  #Project.Common.PasswordBoxAssistant   BoundPassword �� *
Password+ �� Mode$ TwoWay=�$ PropertyChanged=��� $	��250��$��Center=�$��登录��$��0,16,0,0q�  ButtonPrimary#��[ ���� *LoginCommand+