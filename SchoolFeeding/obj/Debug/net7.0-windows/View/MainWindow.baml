   M S B A M L   `   `   `  ����H  DSchoolFeeding, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SchoolFeeding.ViewModelSchoolFeeding.ViewModel  5   	   C&clr-namespace:SchoolFeeding.View.PagesSchoolFeeding.View.Pages  5   	   #    SchoolFeeding.View.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:SchoolFeeding.ViewModel     5   	   8pages&clr-namespace:SchoolFeeding.View.Pages     5   	     � Title$  
MainWindow��5
   	   $	��450��6   $	��800��6)    � 
ResizeMode$ NoResize=�5   	    � DataContext 5      +   %SchoolFeeding.ViewModel.MainViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      .+�5   
   o� $
��Left=�5      $��5,5,0,5q�5      $��45��5      $��20��5      $��0��5      .@�5      m� $��20��6   $��45��5      $��
Назад��5      $��Center=�5       m� Command 5       �� *BackPageCommand+5"      5#      �� 5$   
   $��1��5&      ��5%      �� *Page+5&      �� 5'   
   $��Right=�6   $	��Top=�6,   $��30(�6D   ��6R   �� *	Balance+5(      5)      