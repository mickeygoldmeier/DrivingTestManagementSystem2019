   M S B A M L   `   `   `  ���� @  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF      PLWPF.NewQuestionWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  localclr-namespace:PLWPF    � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=�$#��/PLWPF;component/Fonts/#Heebo)� � Title$" הוספת שאלה חדשה��$	��600��$	��400�� � Icon$ images/icon.ico������  j� Stretch$ UniformToFill=� �� ImageSource$! images/main_window_back.jpg��.���  � RowDefinitions �� $
��auto ��� $��* �.+��� $��0�� �� Orientation$ Vertical=����	#E6FFFFFF J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  	   	Direction$		 270Q�
   
BlurRadius$
 7Q�   ShadowDepth$ 3Q�   Opacity$	 0.7Q�.+��� $ ��הוסף שאלה חדשה��$��0, 5, 0, -5q�$��25(�$��Center=��� $ 
Horizontal=�$��Center=�.+��� -    � ToolTip$< 6שמור את השינויים שביצעת במידע��
���   �$��40��.���� $ 
Horizontal=�.+��� $��שמור��$��Center=�$��15(�'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]   Kind$ ContentSave=�$��Center=�$��5q�$��25��$��25������  �� 
TargetType" ��   MaterialDesignFlatButton �� BasedOn# [   �� Triggers ��  �� Binding ��  �� ElementName$ txb_question�� �� Path$
 Text� �� Value  .a���  �� Property � {� �� Value .  ��  �� $ 
txb_answer��$
 Text�  .a���  � {� .  ��  �� $ txb_wrong_answer1��$
 Text�  .a���  � {� .  ��  �� $ txb_wrong_answer2��$
 Text�  .a���  � {� .  ��  �� $ txb_wrong_answer3��$
 Text�  .a���  � {� .  �� $��Hidden=�$��1��.���� .+�#  MaterialDesignThemes.Wpf.Card  $��10, 10, 10, 0q�$��5q�+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=����	#E6FFFFFF.���� .+��� $��תוכן השאלה��$��RightToLeft=�$��15(�$��5q��� �� -    ��Name$ txb_question�� ( $MaterialDesignFilledTextFieldTextBox#��[  �� TextWrapping$
 Wrap=�$��RightToLeft=�$��Stretch=�$	��280�� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ טקסט חופשי��$��20��  $��10, 10, 10, 0q�$��5q�$ Depth3=����	#E6FFFFFF.���� .+��� $&�� תשובה נכונה לשאלה��$��RightToLeft=�$��15(�$��5q��� �� -   $ 
txb_answer��#��[ $
 Wrap=�$��RightToLeft=�$��Stretch=�$	��280�� . $
 Auto=�$ טקסט חופשי��$��20��  $��10, 10, 10, 0q�$��5q�$ Depth3=����	#E6FFFFFF.���� .+��� $*��$תשובות שגויות לשאלה��$��RightToLeft=�$��15(�$��5q��� �� -   $ txb_wrong_answer1��$��
0, 0, 0, 5q�#��[ $
 Wrap=�$��RightToLeft=�$��Stretch=�$	��280�� . $
 Auto=�$ טקסט חופשי��$��20���� -   $ txb_wrong_answer2��$
��0, 5q�#��[ $
 Wrap=�$��RightToLeft=�$��Stretch=�$	��280�� . $
 Auto=�$ טקסט חופשי��$��20���� -   $ txb_wrong_answer3��$
��0, 5q�#��[ $
 Wrap=�$��RightToLeft=�$��Stretch=�$	��280�� . $
 Auto=�$ טקסט חופשי��$��20��  $��10, 10, 10, 0q�$��5q�$ Depth3=����	#E6FFFFFF.���� .+��� $&�� הוספת תמונה לשאלה��$��RightToLeft=�$��15(�$��5q��� �� $ 
Horizontal=�$��Center=�.+��� $��הוסף תמונה��$��Center=�$��12(�d� -    " MaterialDesignFlatToggleButton#��[ $' !צרף תמונה לשאלה זו��.��  -     Name$ ico_clip��$ 	Paperclip=�$��21��$��21���� -	    ��Name$ image_display��$��10q�