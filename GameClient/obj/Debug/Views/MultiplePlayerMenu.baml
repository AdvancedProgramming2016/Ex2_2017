   M S B A M L   `   `   `  ����E  AGameClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GameClient.ViewsGameClient.Views  5   	   )    #GameClient.Views.MultiplePlayerMenu   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:GameClient.Views     5   	     � Title$  MultiplePlayerMenu��5   	   $	��350��6$   $	��350��61    � WindowStartupLocation$ CenterScreen=�6=   .��5	      � .+�5
   
   �� -   5         ��Name$ listOfGameBlock��5
      $
��Left=�6-   $��	34,30,0,0q�6H   $
��Wrap=�6[   $��List of games:��6o   $	��Top=�6�   �� 5   
   -   5         ��Name$ listOfGameComboBox��5   �   $
��Left=�6�   $��
121,30,0,0q�6�   $	��Top=�6�   $	��120��6�   ��6   ��  �� Path$ VM_ListOfGames� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   -   5         ��Name$ joinGameButton��5      $��	Join Game��6)   $
��Left=�6=   $��
121,68,0,0q�6X   $	��Top=�6l   $��75��6�   6�   �� 5   
   -   5        $ mazeNameBlock��5      $
��Left=�6+   $��
34,109,0,0q�6F   $
��Wrap=�6Z   $��
Maze name:��6n   $	��Top=�6�   �� 5   
   -   5         ��Name$ mazeNameBox��5      $�� ��6'   $
��Left=�6/   $��23��6J    �� TextWrapping$
 Wrap=�6V   $	��Top=�6j   $	��120��6�   $��121,108,0,0q�6�   �� 5   
   -   5        $ mazeRowsBlock��5      $
��Left=�6+   $
��Wrap=�6F   $��
Maze rows:��6Z   $	��Top=�6l   $��
34,153,0,0q�6�   �� 5   
   -   5        $ mazeRowsBox��5      $
��Left=�6\   $��23��6w   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   $��121,152,0,0q�6�   ��6'   �� $ VM_DefaultNumRows�	 �� Mode$	 OneWay=�6�   �� 5   
   -   5        $ mazeColBlock��5      $
��Left=�6*   $��
34,193,0,0q�6E   $
��Wrap=�6Y   $	��Top=�6m   �� 6�   $�Maze coloms:��6�   6�   �� 5   
   -	   5        $ mazeColsBox��5      $
��Left=�6\   $��23��6w   $��121,192,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��120��6�   ��6'   �� $ VM_DefaultNumCols�$	 OneWay=�6�   �� 5   
   -
   5        $ 	startGame��5      $��
Start Game��6$   $
��Left=�69   $��121,239,0,0q�6T   $	��Top=�6i   $��75��6�   6�   �� 5   
   -   5        $ 	CancelBtn��5      $��Cancel��6$   $
��Left=�65   $��121,273,0,0q�6P   $	��Top=�6e   $��75��6}   6�   5      5      