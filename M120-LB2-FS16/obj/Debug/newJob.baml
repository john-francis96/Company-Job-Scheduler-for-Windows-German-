   M S B A M L   `   `   `  ����H  DM120-LB2-FS16, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:M120_LB2_FS16M120_LB2_FS16  5          M120_LB2_FS16.newJob   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:M120_LB2_FS16     5        N� Title$  newJob��5	      .��5      � 
�������$��15q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� 
�������$��14(�6#   $
��Bold"�61   .��6C   Neue Einsatz6Q   �� 5   
   $��1��6   .��6   Name:6$   �� 5   
   -   5         ��Name$ jobName��5      $��1��6#   $��1��60   $��5q�6@   �� 5   
   $��2��6   .��6   Mitarbeiter:6+   �� 5   
   -   5         ��Name$ jobMit��5      $��2��6#   $��1��60   $��5q�6@   �� 5   
   $��3��6   .��6   
Projekt:6'   �� 5    
   -   5        $ jobProj��5       $��3��6$   $��1��61   $��5q�6A   �� 5!   
   $��4��6   .��6   Start Datum:6+   (  "System.Windows.Controls.DatePicker  5"   
   -   5          Name$ jobStart��5"      $��4��6'   $��1��64   $��5q�6D   �� 5#   
   $��5��6   .��6   
End Datum:6)     5$   
   -   5        $ jobEnd��5$      $��5��6%   $��1��62   $��5q�6B   �� 5%   
   $��1��6   $��6��6%    �� Orientation$ 
Horizontal=�62   $��Right=�6K   $��5q�6g   .+�5&      �� -   5         ��Name$ btnID��5&      $y�	Collapsed=�6$   6=   �� 5'      -   5        $ 	jobSubmit��5'      
 � Tag$	 new��6?   .��6I   	Speichern6T   5(      �� 5)   
   -   5        	 ��Name$	 success��5)      $y�	Collapsed=�6%   $��2��6<   $��7��6P   ���	#FF2EFF00$��Center=�6t   $��0,10,0,0q�6�   $��16(�6�   $��Segoe UI Emoji)�6�   6�   5*      5+      