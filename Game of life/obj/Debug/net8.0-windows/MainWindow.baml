   M S B A M L   `   `   `  ����G  CGame of life, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Game_of_lifeGame_of_life  5   	       Game_of_life.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Game_of_life     5   	   -   5          �Name$  game_window��5   	    � Title$ Game of Life��5   	   $	��550��6   $	��600��6+   ���	#FF086758 � Topmost$
 True��6N    � Cursor$
 Hand��6]   .��5	      �� -   5         ��Name$ Main_Canvas��5	      $	��450��6"   $	��550��6/   ���	#FFA1C3BD.+�5
   
   �� -   5         ��Name$ button_pause��5
      $��20��6'   $��40��63   $��pause��6>    �� Left$	 492��6N   
 �� Top$	 362��6`   $��Center=�6�   $	��Top=�6�   �� 5   
   -   5        $ button_start��5      $��20��6'   $��40��63   $��Start��6>   $	 492��6N   $	 322��6`   $��Center=�6�   $	��Top=�6�   �� 5   
   -   5        $ button_clear��5      $��20��6'   $��40��63   $��clear��6>   $	 492��6N   $	 399��6`   $��Center=�6�   $	��Top=�6�   �� 5   
   -   5         ��Name$ start_Position_1��5      $0��*C:\Users\buerklele\Pictures\GoL\glider.png��6*   $��57��6^   $	 472��6j   $ 74��6|   $��79��6�   $
 Hand��6�   �� 5   
   -   5        $ start_Position_2��5      $1��+C:\Users\buerklele\Pictures\GoL\blinker.png��6*   $��57��6_   $	 472��6k   $	 159��6}   $��79��6�   $
 Hand��6�   �� 5   
   -   5        $ start_Position_3��5      $0��*C:\Users\buerklele\Pictures\GoL\pulsar.png��6*   $��57��6^   $	 472��6j   $	 250��6|   $��79��6�   $
 Hand��6�   �� 5   
   -	   5        	 ��Name$	 textBox_GenerationCounter��5      $	 471��65   
 �� TextWrapping$

 Wrap=�6G   $��Generation Counter��6[   $ 10��6u   $��79��6�   $��Center=�6�   $��44��6�   $	��Top=�6�    �� 
IsReadOnly$
 True��6�   �� 5   
   $	��100��6   $
 -393��6!   
���   �$	 138��6C   $	��100��6T   5      5      