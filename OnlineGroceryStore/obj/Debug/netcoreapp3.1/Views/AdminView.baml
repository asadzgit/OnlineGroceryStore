   M S B A M L   `   `   `  ����M  IOnlineGroceryStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OnlineGroceryStore.ViewsOnlineGroceryStore.Views  5      K*clr-namespace:OnlineGroceryStore.ConverterOnlineGroceryStore.Converter  5      (    "OnlineGroceryStore.Views.AdminView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:OnlineGroceryStore.Views     5      ;Conv*clr-namespace:OnlineGroceryStore.Converter     5        � 	Resources  5
      .  (OnlineGroceryStore.Converter.MyConverter   	Converter��%   &	          5   
   6/   5      .��5      � 
�������$��Stretch=�6"    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� $��0��6   $��0��6   $��Center=�6-   $��ADMIN DASHBOARD��6Q   $
��Bold"�6k   $��30(�6}   $��3��6�   6�   �� 5   
   $��Center=�6   $��Add New Product��62   $��20(�6L   $
��Bold"�6Z   $��1��6l   $��0��6y   6�   �� 5   
   $��Center=�6   $��Product ID:��62   $��16(�6H   $��2��6V   $��0��6c   6u   �� 5    
   $��Center=�6   $��Product Name:��62   $��16(�6J   $��3��6X   $��0��6e   6w   �� 5!   
   $��Center=�6   $��Price:��62   $��16(�6C   $��4��6Q   $��0��6^   6p   �� 5"   
   $��Center=�6   $��	Quantity:��62   $��16(�6F   $��5��6T   $��0��6a   6s   �� 5$   
   -   5        $��txtid��5$      $��2��6   $��0��6,   $��150,10,70,10q�6<   $��2��6R   $��22(�6f   $��Center=�6t   6�   �� 5%   
   -   5        $��txtname��5%      $��3��6!   $��0��6.   $��150,10,70,10q�6>   $��2��6T   $��22(�6h   $��Center=�6v   6�   �� 5&   
   -   5        $��txtprice��5&      $��4��6"   $��0��6/   $��150,10,70,10q�6?   $��2��6U   $��22(�6i   $��Center=�6w   6�   �� 5'   
   -   5        $��txtqty��5'      $��5��6    $��0��6-   $��150,10,70,10q�6=   $��2��6S   $��22(�6g   $��Center=�6u   6�   �� 5(   
   $��Add Product��6   $��6��6F   $��0��6S   $��100,10,70,10q�6c   $��14(�6y   ��6'   �� *
addProduct+��5)      `� 5*       `� 	Converter# [  6   .1�5+      ��  �� ElementName$ txtid��6    �� Path$
 Text�62   �� 5,      $ txtname��6   $
 Text�64   �� 5-      $ txtprice��6   $
 Text�65   �� 5.      $ txtqty��6   $
 Text�63   5/      51      �� 53   
   $��PRODUCTS��6   $��Products��6J   $��6��6f   $��1��6s   $��120,10,50,10q�6�   $��14(�6�   ��6$   �� *UpdateViewCommand+6�   �� 54   
   $��LOG OUT��6   $
��Main��6I   $��7��6a   $��1��6n   $��120,10,50,10q�6~   $��14(�6�   ��6#   �� *UpdateViewCommand+6�   �� 56   
   $��1��6   $��2��6   $��Delete Product��6-   $
��Bold"�6F   $��20(�6X   $��Center=�6f   6�   �� 57   
   $��2��6   $��2��6!   $ ��Enter Product ID to Delete��61   $��Center=�6S   $��20(�6n   6~   �� 58   
   $��3��6   $��2��6   $	��ID:��6-   $��Center=�6;   $��20(�6V   6f   �� 59   
   -   5        $��txtDel��59      $��3��6    $��2��6-   $��40,10q�6=   $��22(�6M   $��Center=�6[   6   �� 5:   
   $��DELETE��6   $��4��6   $��2��6�   $��80,10q�6�   $��14(�6�   ��6"   �� *deleteProduct+��6D   �� $ txtDel��$
 Text�6�   �� 5<   
   $��0��6   $��0��6&   $��8��66   $��3��6G   ��6\   �� *SelectedViewModel+5=      5>      