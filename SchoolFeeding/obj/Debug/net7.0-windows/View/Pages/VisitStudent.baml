   M S B A M L   `   `   `  ����H  DSchoolFeeding, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SchoolFeeding.View.PagesSchoolFeeding.View.Pages  5      +    %SchoolFeeding.View.Pages.VisitStudent   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:SchoolFeeding.View.Pages     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��0.25* �6   �� 5       � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      $	��13* �6   .+�5   
   �� $��0��6Z   $��0��6j   $��10,10,10,10q�6w   $��20(�6�   ��6   �� *	Classes+ �� SelectedItem 63   �� *SelectedClass+6�   �� 5   
   $��0��6   $��1��6   $��10,10,335,10q�6.   $��Отметить��6e   $��30(�6x   ��6D   �� *TodayCommand+6x   �� 5   
   $��1��6   $��335,10,10,10q�6    $��30(�6V   
������ 
������ ��66   �� *CurrentDate+6}   �� 5   
   $��2��6\   $��1��6p   $��10,10,10,10q�6}   ��6   �� *
Students+ 63   �� *SelectedStudent+��5      �� 5      .R�5      �  5      �� 5      $��50 �6/   �� 5      $	��100 �6/   �� 5      $��20 �6/   .+�5       �� $��0��6$   ��64   �� *Имя+�� 5!      $��1��6$   ��64   �� *Фамилия+�� 5"      $��2��6#    d� 	IsChecked 63   �� *
IsSelected+5$      5%      5'      5(      5)      