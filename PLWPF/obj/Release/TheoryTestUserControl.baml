   M S B A M L   `   `   `  ���� @  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  !    PLWPF.TheoryTestUserControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  localclr-namespace:PLWPF  .��� 
������ .+��� -   $��	text_hold��$��Center=�$��RightToLeft=�  �� TextAlignment$  Center=�$	��Top=�$��0,30,0,0q�$��30(�$y�	Collapsed=��� -   $��before_test��$��Stretch=�$��Stretch=�$y�	Collapsed=�.+��� $��Right=�$	��Top=�$��	0,30,70,0q�$��הוראות��$��25(�$
��Bold"��� -   $��txtblk_rules��$  Right=�$��Right=�$	��Top=�$��
0,10,100,0q�$��20(��� -   $��התחל מבחן��$��20(�$��Center=�$��	0,30,0,10q�$��Bottom=�$��50��$	��150��� -   $��	test_view��$��Stretch=�$��Stretch=�$y�	Collapsed=� � RowDefinitions �� $��* ��� $
��auto � � ColumnDefinitions �� $
��auto ��� $��* �.+��� $��Stretch=�$��Stretch=�.+��� $	��Top=�$��Center=�$��20, 20, 20, 10q�$��עבור לשאלה��$��25(�� -   $��check_box_grid��$��20, 5q� �� $
��auto � �� $��* ��� $��* ��� $��* ��� $��0��$��1��.+��� $��זמן שנותר:��$��Center=�$��RightToLeft=�$��15(�$��-5q��� -   $��timerTextBlock��$��20, 10q�$��1��$	��Top=�$��Center=�$�� ��$��25(�����  �� 
TargetType" �� �� Triggers P�  P� Property � �� P� Value$ 00:10��. ���  �� Property � �� �� Value
 �  ��P�  � ��$ 00:09��. ���  � ��
 �  ��P�  � ��$ 00:08��. ���  � ��
 �  ��P�  � ��$ 00:07��. ���  � ��
 �  ��P�  � ��$ 00:06��. ���  � ��
 �  ��P�  � ��$ 00:05��. ���  � ��
 �  ��P�  � ��$ 00:04��. ���  � ��
 �  ��P�  � ��$ 00:03��. ���  � ��
 �  ��P�  � ��$ 00:02��. ���  � ��
 �  ��P�  � ��$ 00:01��. ���  � ��
 �  ��P�  � ��$ 00:00��. ���  � ��
 �  ���� -   $��Bottom=�$��Center=�$��20, 10q�$��סיים מבחן��$��20(�$
��auto��$
��auto���� 	 �� Orientation$	 
Horizontal=�$��1��$��1��$��Center=�$��Center=�.+��� -	   $��next_question_button��$��1��$��1��$��Center=�$��Center=�$��7q�$��25(�$
��auto��$
��auto��.���� $	 
Horizontal=�$��Center=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]
   Kind$
 	ArrowLeft=�$��Center=�$��30��$��30��$��0, 0, 10, 0q��� $��לשאלה הבאה��$��Center=��� -
   $��previous_question_button��$��1��$��1��$��center=�$��Center=�$��7q�$��25(�$
��auto��$
��auto��.���� $	 
Horizontal=�$��Center=�.+��� $��לשאלה הקודמת��$��Center=�  $
 
ArrowRight=�$��Center=�$��30��$��30��$��10, 0, 0, 0q�� -   $��end_test_view��$��Stretch=�$��Stretch=�$y�	Collapsed=� �� $��6* ��� $��4* �.+��� $��Stretch=�$��Stretch=�.+��� -   $��text_result��$��RightToLeft=�$	��Top=�$��Center=�$��0,20,0,0q�$��50(��� -   $��text_points��$��RightToLeft=�$��Center=�$��Center=�$��35(��� -   $��text_postponement��$��RightToLeft=�$��Center=�$��Center=�$��30(�$C��=תוכל לקחת את המבחן שוב בעוד 24 שעות��$y�	Collapsed=��� -   $��refresh_button��$��עדכן משתמש��$y�	Collapsed=�$	��250��&   System.Windows.Controls.DataGrid  -     Name$ Wrong_list_view��$��RightToLeft=�$��Center=�$��1��   
IsReadOnly . $��10q�   CanUserAddRows .     AutoGenerateColumns .  ���	#00000000   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ שאלה��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
Question+  $ תשובתך�� �� *Student_answer+  $ התשובה הנכונה�� �� *Answer+.@�>