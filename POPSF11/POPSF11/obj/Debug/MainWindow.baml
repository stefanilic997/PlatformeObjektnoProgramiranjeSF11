   M S B A M L   `   `   `  ����H  DPOP-SF-11-GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:POP_SF_11_GUIPOP_SF_11_GUI  5   	       POP_SF_11_GUI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:POP_SF_11_GUI     5   	     � Title$$  Pregled namestaja(Main window)��5   	   $	��651��60   $��907.5��6=   .��5	      �  � RowDefinitions 5
   
   �� 5      $��35 �6   �� 5      $	��465 �6   �� 5      $	��100 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��
dgNamestaj��5   a      CanUserAddRows .  $��1��6s   $��15(�6�   �� 5   
   -   5        $��Izlaz��5      $��96��6!   $��20(�6,   $��Right=�6:   $��
0,11,10,25q�6d   $��2��6x   �� 5   
   -   5        $��ObrisiNamestajButton��5   "   $��Obrisi��6   $��20(�6>    J� RenderTransformOrigin$ 	0.75,0.58&�6[   $��208,12,596,25q�6}   $��2��6�   $��Center=�6�   �� 5   
   -   5        $��IzmeniNamestajButton��5   "   $��Izmeni��6   $��20(�6>   $��110,12,697,25q�6[   $��2��6r   �� 5   
   -   5        $��DodajNamestajButton��5   !   $��Dodaj��6   $��20(�6<   $��10,12,796,25q�6X   $��2��6n   o� 5   
   $
��Left=�6   $��35��6*   $	��Top=�66   $	��900��6N   .@�5      m� -   5        $��Tipovi Namestaja��5      $��35��61   $	��100��6=   6I   m� 5      -   5         m�Name$ Namestaj��5      $��     Namestaj��6)   $��35��6@   $	��100��6L   6X   m� 5      -   5        $ AkcijskeProdaje��5      $��Akcijske Prodaje��60   $��35��6J   $	��100��6V   6b   m� 5      -	   5        $��Dodatne usluge��5      $��35��6/   $	��100��6;   6G   m� 5      -
   5        $��     Korisnici��5      $��35��6/   $	��100��6;   6G   m� 5      -   5        $��       Racuni��5      $��35��6.   $	��100��6:   6F   m� 5      -   5        $��Salon��5      $��35��6&   $	��100��62   6>   5      �� 5   
   -   5         ��Name$ btnPretrazi��5      $��Pretraga��6&   $
��Left=�69   $��
337,39,0,0q�6e   $��2��6y   $	��Top=�6�   $	��103��6�   $��37��6�   �� 5   
   -   5        $ btnSortiraj��5      $��
Sortiranje��6&   $
��Left=�6;   $��
462,39,0,0q�6g   $��2��6{   $	��Top=�6�   $	��103��6�   $��37��6�   �� 5   
   -   5         ��Name$ cbSortiranje��5      $
��Left=�6)   $��	462,9,0,0q�6D   $��2��6W   $	��Top=�6d   $	��103��6|   $��23��6�   �� 5    
   -   5        $ cbOrderHack��5       $
��Left=�6(    �� SelectedIndex$ 0��6C   $��
570,24,0,0q�6U   $��2��6i   $	��Top=�6v   $��59��6�   �� 5!   
   -   5         ��Name$ 
tbPretrazi��5!      $
��Left=�6&   $��23��6A   $��	337,9,0,0q�6M   $��2��6`   	 �� TextWrapping$
	 Wrap=�6m   $��Pretraga��6�   $	��Top=�6�   $	��103��6�   5"      5#      