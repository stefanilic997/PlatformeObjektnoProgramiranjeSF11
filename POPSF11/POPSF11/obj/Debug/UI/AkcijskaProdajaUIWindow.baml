   M S B A M L   `   `   `  ����H  DPOP-SF-11-GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:POP_SF_11_GUI.UIPOP_SF_11_GUI.UI  5   	   .    (POP_SF_11_GUI.UI.AkcijskaProdajaUIWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:POP_SF_11_GUI.UI     5   	     � Title$  Rad sa namestajem��5   	   $	��300��6#   $	��300��60   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��233 �6   �� 5      $	��144 �6    � RowDefinitions 5   
   �� 5      $��42 �6   �� 5      $��35 �6   �� 5      $��42 �6   �� 5      $��35 �6   .+�5   
   �� $��0��6   $��0��6   $��Popust��6-   $
��Left=�6>   $	��105��6Y   $��0,0,0,10q�6e   �� 5   
   -   5        $��tbPopust��5   H   $��0��6   $��15(�6   $
��Left=�6-   $��
110,0,0,10q�6v   $��68��6�   ��6X   ��  �� Path$ Popust�6�   �� 5   
   $��Datum zavrsetka��6   $
��Left=�6+   $	��105��6F   $��2��6R   $��0,9,0,10q�6_   �� 5   
   $��Datum pocetka��6   $
��Left=�6)   $	��105��6D   $��0,0,0,10q�6P   $��1��6b   �� 5   
   -   5        $��Sacuvaj��5      $��Right=�6#   $��0,94,10,-101q�6^   $��3��6t   $��44��6�   �� 5   
   -   5        $��Izlaz��5      $��Right=�6!   $��0,94,95,-101q�6K   $��3��6a   $��42��6n   $��1��6y   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ datumPocetka��5      $
��Left=�6+   $��	110,1,0,0q�6q   $��1��6�   $	��Top=�6�   $��25��6�   $	��113��6�      DisplayDate 6G   �� $ DatumPocetka�6�     5   
   -   5        $ datumZavrsetka��5      $
��Left=�6-   $��	110,9,0,0q�6H   $��2��6�   $	��Top=�6�   $	��113��6�   $��24��6�    6[   �� $ DatumZavresetka�6�   5      5      