   M S B A M L   `   `   `  ���� ?  ;Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Main.Views
Main.Views      Main.Views.LoadFromDB   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:Main.Views     -     � Icon$  ../Icons/LoadDB.ico�� � WindowStartupLocation$ CenterScreen=� � Title$ 	Load File��$	��700��$
��1200�� � 	Resources    Main.Views.LoadFromDB   
_File_self&   System.Windows.Controls.DataGrid �� 
TargetType  DG_TEXT �� Property �� Value5  /System.Windows.Controls.DataGridLengthConverter  DG_Cell*  $System.Windows.Controls.DataGridCell �� BasedOn � SpreadMethod	 �� 
StartPoint
 �� EndPoint � Color � Offset �� Triggers P� Property P� Value��%�  &	        &	      &	 }     7   �� "  .���  � �� �#8800FF��  � ��
 �   ���  � ��$ 25(���  �@ ColumnWidth$ * �� "  8   .���  � ��
 �������  � ��$ Center=���  � ��
 �   ���  � ��$ 20(���  � �� �� $ Reflect=�$		 1,1&�$	
 1,0&�.��� $ #008080��$ 0Q�� $ #ffffff��$	 .25Q�� $ #05c1ff��$ .5Q�� $ #ffffff��$	 .75Q�� $ #005757��$ 1Q���  �@�ToolTip _� $	��300��$	��100��.���� $��Double click to continue��$��25(�-    P�  �@ 
IsSelected . . ���  � ��
 �  ����  � ��
 �����.��� .+�  -   $��DG��   CanUserAddRows .     
IsReadOnly . #��[    	CellStyle# [    AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Phone��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
FileName+  $ E - mail�� �� *
FileData+