   M S B A M L   `   `   `  ����M  IOnlineGroceryStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OnlineGroceryStore.ViewsOnlineGroceryStore.Views  5      K*clr-namespace:OnlineGroceryStore.ConverterOnlineGroceryStore.Converter  5      '    !OnlineGroceryStore.Views.SaleView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:OnlineGroceryStore.Views     5      ;Conv*clr-namespace:OnlineGroceryStore.Converter     5        � 	Resources  5
      .  (OnlineGroceryStore.Converter.MyConverter   	Converter��%   &	          5   
   6/   5      .��5      � 
�������$��Stretch=�6"    � ColumnDefinitions 5   
   �� 5      6"   �� 5      6!   �� 5      6"   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� $
��SALE��6   $��0��6   $��0��6,   $
��Left=�6<   $
��Bold"�6^   $��30(�6p   $��10,0,0,0q�6~   6�   �� 5   
   $��Item ID��6   $��0��6"   $��1��6/   $��Center=�6@   $��20(�6d   $��
10,20,10,0q�6r   6�   �� 5    
   $��Quantity��6   $��0��6#   $��2��60   $��Center=�6@   $��20(�6d   $��
10,20,10,0q�6s   6�   �� 5"   
   $��LOG OUT��6   $��logout��6I   $��1��6c   $��0��6p   $	��Top=�6�   $��30��6�   $��80��6�   ��6#   �� *UpdateViewCommand+6�   �� 5#   
   -   5        $��txtid��5#      $��1��6   $��1��6,   $��	10,0,10,0q�6<   $��30��6O   $	��Top=�6[   $��Center=�6s   6�   �� 5$   
   -   5        $��txtqty��5$      $��1��6    $��2��6-   $��	10,0,10,0q�6=   $��30��6P   $	��Top=�6\   $��Center=�6t   6�   �� 5%   
   $��1��6-   $��3��6:   $	��Add��6J   $	��Top=�6X   $��30��6p   $
��Left=�6|   $��80��6�   ��6   �� *	addCart+��5&      `� 5'       `� 	Converter# [  6   .1�5(      ��  �� ElementName$ txtid��6    �� Path$
 Text�62   �� 5)      $ txtqty��6   $
 Text�63   5*      5,      �� 5-   
   $��FINISH��6/   $��1��6@   $��3��6M   $	��Top=�6]   $��80,0,0,0q�6u   $��30��6�   $��80��6�   ��6   �� *	chechkout+6�   �� 5/   
   $��Available ITEMS��6   $��2��6*   $��0��67   $��Right=�6G   $
��Bold"�6j   $��20(�6|   6�   �� 50   
   $
��CART��6   $��2��6   $��2��6,   $��Right=�6<   $
��Bold"�6_   $��20(�6q   6�   &   System.Windows.Controls.DataGrid  51   
      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ 0.25* 68   $��3��6L   $��0��6Y   $��4��6i   $��2��6z   $��10,-20,10,10q�6�   ��6   �� *Viewproducts+6�     52   
   $ 0.25* 68   $��3��6L   $��2��6Y   $��4��6i   $��2��6z   $��10,-20,10,10q�6�   ��6   �� *Cartproducts+6�   �� 53   
   $��0��6   $��0��6&   $��7��66   $��4��6G   ��6[   �� *SelectedViewModel+54      55      