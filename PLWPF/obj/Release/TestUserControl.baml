   M S B A M L   `   `   `  ����   Test%clr-namespace:BE;assembly=TestBE  @ <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF    PLWPF.TestUserControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  *localclr-namespace:BE;assembly=Test   local1clr-namespace:PLWPF  $#��/PLWPF;component/Fonts/#Heebo)�$��Right=�.��#  MaterialDesignThemes.Wpf.Card  -      Name$
  card��$��6q����	#E6FFFFFF.��� .+��� $��Hidden=�.���� .+�� -    �Name$ state_color��
���z���$��Center=�$��0, -2, 0, 0q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     	Direction$	 270Q�   
BlurRadius$ 7Q�   ShadowDepth$ 1Q�   Opacity$	 0.5Q�.+��� -    ��Name$ 
test_state��
�������$��מבחן עתידי��$��Black"�$��1q�$��15(�$��Center=�$��Center=��� .+���  �� Orientation$ 
Horizontal=�$��Right=�$��0q�$��Center=�.+��� -   $
 code��$��Center=�$��5q����� *Test_number+�� $��:קוד מבחן��$��Center=��� $��images/code.png��$��25��$��25��$��5q��� $ 
Horizontal=�$��Right=�$��0q�$��Center=�.+��� -   $ 	tester_id��$��Center=�$��5q����� *	Tester_id+�� $��בוחן��$��Center=��� $��images/user.png��$��25��$��25��$��5q��� $ 
Horizontal=�$��Right=�$��0q�$��Center=�.+��� -   $ 
student_id��$��Center=�$��5q����� *
Traniee_id+�� $��
תלמיד��$��Center=��� $��images/user.png��$��25��$��25��$��5q��� $ 
Horizontal=�$��Right=�$��0q�$��Center=�.+��� -   $
 hour��$��14:00��$��Center=�$��5q��� $��images/clock.png��$��25��$��25��$��5q��� $ 
Horizontal=�$��Right=�$��0q�$��Center=�.+��� -   $
 date��$��	15/2/2018��$��Center=�$��5q��� $��images/calender.png��$��25��$��25��$��5q��� $ 
Horizontal=�$��Right=�$��0q�$��Center=�.+��� -	   $ address��$.��(הועד הלאומי 21, ירושלים��$��Center=��� $��images/location.png��$��25��$��25��$��5q��� $ 
Horizontal=�$��Right=�$��0q�$��Center=�.+��� -
   $ car_type��$��רכב פרטי��$��Center=�$��5q��� $��images/car.png��$��25��$��25��$��5q��� -   	 ��Name$	 details��$y�	Collapsed=�.+��� $ 
Horizontal=�$��Right=�$��0q�$��Center=�.+��� -   
 ��Name$
 	criterion��$��
0, 4, 0, 0q�$	��150��{�.  ��Z� ��Z�  �� SelectionBrush Z� . �/� .T�J�  �� TextAlignment$ Right=��� $��:מדדים��$	��Top=�$��5q��� $��images/criterion.png��$��25��$��25��$��5q�$	��Top=��� $ 
Horizontal=�$��Right=�$��0q�$	��Top=�.+��� -   $
 remarks��$��
0, 4, 0, 0q�$	��150��{�.  ��Z� ��Z�  Z� . �/� .T�J� $ Right=��� $��:הערות��$	��Top=�$��5q��� $��images/edit.png��$��25��$��25��$��5q�$	��Top=�� -   $ OptionPanel��$y�Hidden=�.+��� -    ��Name$ delete_button��
���z���
���z��� *  &MaterialDesignFloatingActionMiniButton#��[   � ToolTip$ מחק מבחן זה��$
��Left=�$��Bottom=�$��10q�.��'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]   Kind$ Delete=�$��24��$��24��
��������� -   $ 	fee_buton��
����� �
����� �$y�	Collapsed=�#��[  $" הדפס אגרת רישוי��$
��Left=�$��Bottom=�$��10q�.��  $ Printer=�$��24��$��24��
��������� -   $ edit_button��
���z���
���z���$y�Hidden=�#��[  $ ערוך מבחן זה��$
��Left=�$��Bottom=�$��10q�.��  $
 Edit=�$��24��$��24��
�������