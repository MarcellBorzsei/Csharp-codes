   M S B A M L   `   `   `  ����E  AKitolasWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:KitolasWPF
KitolasWPF  5   	       KitolasWPF.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:KitolasWPF     5   	     � Title$  
MainWindow��5   	   $	��681��6   $	��681��6)    J� InputBindings 5	      �� 5
   
   
 �� Key$ Up��6    �� CommandParameter$ up��6?    �� Command 6   �� *SlideCommand+6?   �� 5   
   $
 Down��6   $
 down��6A    6    �� *SlideCommand+6A   �� 5   
   $ Right��6   $ right��6B    6!   �� *SlideCommand+6B   �� 5   
   $
 Left��6   $
 left��6A    6    �� *SlideCommand+6A   �� 5   
   $ Space��6    6!   �� *ChangeSelectedStoneCommand+.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   o� $��0��6   .@�5      m� $
��File��6   .@�5      m� $��	Save Game��6    m� Command 6.   �� *SaveGameCommand+6T   �� 5      m� 5      $��	Load Game��6    6.   �� *LoadGameCommand+6T   5      m� 5      $��Mapsize��6   .@�5      m� $��3x3 Game��6    m� CommandParameter$ 3��6R    6-   �� *MapChangeCommand+6i   m� 5       $��4x4 Game��6   $ 4��6R    6-   �� *MapChangeCommand+6i   m� 5!      $��6x6 Game��6   $ 6��6R    6-   �� *MapChangeCommand+6i   5"      5$      �� 5&   
   $��1��6   ��6$   �� *Fields+��5'      �� 5(      .R�5)      A�  A� Rows 6"   �� *	MapSize+	 A� Columns	 6;   �� *	MapSize+5*      ��5,      �� 5-      .R�5.      �� ��5/      �� 50      
 �� 
TargetType"
 ��6$    �� Triggers 51   "   �� 52   &    �� Binding 62   �� *Stone+ �� Value 06L   .a�53   *   ��  �� Property � �� �� Value
 �����6G   54   '   �� 55   &    62   �� *Stone+ 16L   .a�56   *   ��  � ��
 �   �6G   57   '   �� 58   &    62   �� *Stone+ 26L   .a�59   *   ��  � ��
 �����6G   5:   '   5<      5?      5@      5B      �� 5C   
   $��2��6   .@�5E      �� .��5F      �� $��Remaining steps: ��6   5G      �� 5H      .��5I      �� ��6   �� *	CircleNum+5J      �� 5K      �� 5L      .��5M      �� $��Current player: ��6   5N      �� 5O      .��5P      �� ��6   �� *CurrentPlayer+5Q      �� 5R      �� 5S      .��5T      �� $��Selected stone: ��6   5U      �� 5V      .��5W      �� ��6   �� *SelectedStone+5X      �� 5Y      �� 5Z      .��5[      �� $��Number of black stones: ��6   5\      �� 5]      .��5^      �� ��6   �� *BlackCounter+5_      �� 5`      �� 5a      .��5b      �� $��Number of white stones: ��6   5c      �� 5d      .��5e      �� ��6   �� *WhiteCounter+5f      �� 5g      �� 5h      5i      5k      5l      