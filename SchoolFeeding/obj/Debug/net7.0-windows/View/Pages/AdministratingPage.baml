   M S B A M L   `   `   `  ����H  DSchoolFeeding, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:SchoolFeeding.Model.UtilitiesSchoolFeeding.Model.Utilities  5      1    +SchoolFeeding.View.Pages.AdministratingPage   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:SchoolFeeding.Model.Utilities     5        � 	Resources  5	      7   1SchoolFeeding.Model.Utilities.VisibilityConverter   VisibilityConverter��%   &	          5
   
   6$   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��600 �6   �� 5      .+�5   
   &   System.Windows.Controls.DataGrid     
IsReadOnly$
 True��6   ��5      �� *DataCollection+ �� SelectedItem 5      �� *SelectedItem+�� 5   
   
�������$��Stretch=�5       �� Orientation$ Vertical=�5      $��1��5      .+�5      �� $��5q�5      
�������$ Vertical=�5       $	��120��6(   .+�5"      �� $��Center=�5#      $7��1Информация об обущающемся:��5$      �� 5%      $��2q�6   $ 
Horizontal=�5&      .+�5'      �� $��Имя:��6   �� 5(      $	��124��6   $��25��5)       �� 	MaxLength$ 20��5+      ��5*      �� *	FirstName+5+      5,      �� 5-      $��2q�6   $ 
Horizontal=�5.      .+�5/      �� $��Фамилия:��6   �� 50      $	��100��6   $��25��51      $ 20��53      ��52      �� *
LastName+53      54      �� 55      $��2 2 2 0q�6   $ 
Horizontal=�56      .+�57      �� $��Класс:��6   �� 58      $	��120��6   $��25��59      $ 3��5;      ��5:      �� *Class+5;      5<      5=      �� 5?      
�������$��5q�5B      $)��#Добавить учащегося��5D      y�5A      �� *IsAdd+ �� 	Converter# [  ��5C      �� *StudentAddCommand+5D      �� 5G      
�������$��5q�5K      $6��0Изменить данные учащегося��5L      y�5I      �� *
IsChange+# [  ��5J      �� *StudentChangeCommand+5L      �� 5O      $ Vertical=�5P      $��	5 10 5 10q�5Q      
�������.+�5S      �� $��Center=�5T      $-��'Информация об оплате:��5U      �� 5V      $��5q�6   $ 
Horizontal=�5W      .+�5X      �� $��Уплачено:��6   �� 5Y      $	��100��6   $��25��6*   ��66   �� *Summ+5Z      5[      �� 5]      
�������$��5q�5`      $@��:Редактировать состояние оплаты��5a      ��5b      �� *SummCommand+�� 5d      
�������$��	5 10 5 10q�5g      $'��!Удалить учащегося��5h      ��5f      �� *StudentDeleteCommand+5h      �� 5j      $��	5 20 5 10q�5k      $ 
Horizontal=�5l      5p      5q      5r      5t      