   M S B A M L   `   `   `  ����F  BWpfAppNotiz, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfAppNotizWpfAppNotiz  5   	       WpfAppNotiz.SecondWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WpfAppNotiz     5   	     � Title$  SecondWindow��5   	   $	��450��6   $	��800��6+   .��5	      � ���#F1F1F1 � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��1��6   $��1��6!   .+�5      �� .+�5      �� $	��100��6   $��50��6$   $
��Bold"�60   $��15(�6B   $��Italic$�6P   ���#333333$	��Top=�6x   $��Titel:��6�   �� 5      -   5         ��Name$ txtTitel��5      $	��600��6,   $��50��68   $	��Top=�6D   $��15(�6\   �� 5      $	��100��6   $��50��6$   $
��Bold"�60   $��15(�6B   $��Italic$�6P   ���#333333$	��Top=�6x   $��Notiz:��6�   �� 5      -   5        $ txtNotiz��5      $	��600��6,   $	��150��68   $	��Top=�6E   $��15(�6]   �� 5       �� Orientation$ Vertical=�6   .+�5      �� -   5         ��Name$ btnAdd��5      $��0,20,0,0q�6-   $��Stretch=�6T   ���#0078D7$��12(�6�   $
��Bold"�6�   ���#FFFFFF$	��200��6�   $��40��6�   .��6�   Add6�   �� 5       -   5        $ btnBack��5       $��0,5,0,0q�6.   $	��200��6U   $��40��6a   ���#0078D7$��12(�6�   $
��Bold"�6�   ���#FFFFFF$
��Back��6�   5!      5"      5#      5$      5%      