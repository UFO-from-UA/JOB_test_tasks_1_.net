   M S B A M L   `   `   `  ����?  ;Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Main.Views
Main.Views  5   	       Main.Views.LoadFromDB   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Main.Views     5   	   -   5          � Icon$  ../Icons/LoadDB.ico��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ 	Load File��5
   	   $	��700��6   $
��1200��6(    � 	Resources 5         Main.Views.LoadFromDB   
_File_self&   System.Windows.Controls.DataGrid �� 
TargetType  DG_TEXT �� Property �� Value5  /System.Windows.Controls.DataGridLengthConverter  DG_Cell*  $System.Windows.Controls.DataGridCell �� BasedOn � SpreadMethod	 �� 
StartPoint
 �� EndPoint � Color � Offset �� Triggers P� Property P� Value��%�  &	        &	      &	 �     7   5   
   60   �� 5   
   "  6   .�5      ��  � �� �#8800FF6=   �� 5       � ��
 �   �6;   �� 5       � ��$ 25(�6)   66   �� 5       �@ ColumnWidth$ * 6,   5      �� 5   
   "  6    8   .�5      ��  � ��
 �����6+   �� 5       � ��$ Center=�69   �� 5       � ��
 �   �6;   �� 5       � ��$ 20(�6)   66   �� 5       � �� 5      �� 5      $ Reflect=�6*   $		 1,1&�6B   $	
 1,0&�6S   .��5      � $ #008080��6'   $ 0Q�67   6D   � 5      $ #ffffff��6'   $	 .25Q�67   6F   � 5      $ #05c1ff��6'   $ .5Q�67   6E   � 5      $ #ffffff��6'   $	 .75Q�67   6F   � 5      $ #005757��6'   $ 1Q�67   6D   5       5"      �� 5#       �@�ToolTip 5$      _� 5%      $	��300��6   $	��100��6*   .��5&      �� $��Double click to continue��6$   $��25(�6D   5'      5)      -   5         5+      P� 5,       �@ 
IsSelected . . �5-      ��  � ��
 �  ��63   �� 5.       � ��
 �����63   5/      51      52      .��53      � .+�54   
     -   5        $��DG��54   *      CanUserAddRows .     
IsReadOnly . #��[ 6F      	CellStyle# [ 6h      AutoGenerateColumns .     Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn   Header$ Phone��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *
FileName+6W     57      $ E - mail��6%    67   �� *
FileData+6V   59      5:      5;      