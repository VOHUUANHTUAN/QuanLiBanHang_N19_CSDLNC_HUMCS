   M S B A M L   `   `   `  ����M  ICsdhnc_N19_BanHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Csdhnc_N19_BanHangCsdhnc_N19_BanHang  5   
   %    Csdhnc_N19_BanHang.DangKyWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   
   @d2http://schemas.microsoft.com/expression/blend/2008     5   
   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   
   2local clr-namespace:Csdhnc_N19_BanHang     5   
     � Title$  Đăng ký tài khoản��5   
    � Icon$ icon/windows.png��5	   	   $	��500��5   
   $	��800��6    � WindowStartupLocation$ CenterScreen=�5   	   $	��500��5      $	��800��6    � 	Resources 5         MyStyle_Label �� 
TargetType �� Property �� Value  MyStyle_Button  MyStyle_TextBox  MyStyle_ReadOnlyTextbox��%�  &	        &	 �     &	 q    &	 [    �� 5   
   " ��6&   .�5      ��  � ��$
 Left=�64   �� 5       � ��$	 Top=�62   �� 5       � ��$ 17(�6)   5      �� 5   
   " ��6'   .�5      ��  � ��$
 Left=�64   �� 5       � ��$ Bottom=�62   �� 5       � ��$ 17(�6)   �� 5       � ��$ 90��6&   �� 5       � ��$ 30��6'   5      �� 5    
   " ��6(   .�5!      ��  � ��$
 Left=�64   �� 5"       � ��$	 Top=�62   �� 5#       � ��$ 17(�6)   �� 5$       � ��$ 30��6'   �� 5%       �@��TextWrapping$
 Wrap=�6-   5&      �� 5(   
   " ��60   .�5)      ��  � ��$
 Left=�64   �� 5*       � ��$	 Top=�62   �� 5+       � ��$ 17(�6)   �� 5,       � ��$ 30��6'   �� 5-       � ��$ Center=�69   �� 5.       � �� �#ebecf06+   �� 5/       �@��
IsReadOnly . 6+   50      51      .��52      � .+�53   
   �  � ColumnDefinitions 55      �� 56      $��4* �6#   �� 57      $	��110 �6#   �� 58      $	��220��6#   $	��350��62   $��* �6A   �� 59      $	��110 �6#   �� 5:      $	��220��6#   $	��350��62   $��* �6A   �� 5;      $��4* �6#    � RowDefinitions 5=      �� 5>      $	��100 �6    �� 5?      $	��290 �6    �� 5@      $��40 �6    �� 5A      $	��10* �6    .+�5E      � $��2��6   $��3��6#   $��Bottom=�67   $��0,0,50,8q�6R   $��42��6d    5F      �� 5G      �� 5H      �� 5I      .+�5L      � $��0��6   .+�5M      �� -   5        $	��120��5M      
������ $��0q�6^   .��5N      �� -   5        	 ��Name$	 choose_doitac_BackGround��5N   !   ���#95DBD9$��40��6X   
 �� CornerRadius$
 15��6d   $��0q�6v   ._�5O      � $	��120��6#   $��35��60   .+�5P   "   �� $��Đối tác��6(   $��45,3,0,0q�6:   $
��Left=�6L   
�������$��SemiBold"�5Q   3   $��16(�6I   �� 5R   "   $��icon/food.png��6(   $��10,0,0,0q�6?   $��35��6Q   $��35��6]   $
��Left=�6h   5S      5T      5U      5V      � 5X      $��1��6   $	��150��6'   .+�5Y      �� -   5        
������ $��0q�5Y   U   .��5Z      �� -   5        $!	 choose_khachhang_BackGround��5Z   !   ���#79B1BB$��40��6[   $
 15��6g   $��0q�6y   ._�5[      � $	��150��6#   $��35��60   .+�5\   "   �� $��Khách hàng��6(   $��45,3,0,0q�6=   $
��Left=�6O   
�������$��SemiBold"�5]   3   $��16(�6I   �� 5_   "   $��icon/nguoidung_logo.png��6(   $��10,0,0,0q�6I   $��35��6[   $��35��6g   $
��Left=�6r   5`      5a      5b      5c      � 5e      $��2��6   $	��110��6'   .+�5f      �� -   5        
������ $��0q�5f   Q   .��5g      �� -   5        $	 choose_taixe_BackGround��5g   !   ���#79B1BB$��40��6W   $
 15��6c   $��0q�6u   ._�5h      � $	��110��6#   $��35��60   .+�5i   "   �� $��	Tài xế��6(   $��45,3,0,0q�69   $
��Left=�6K   
�������$��SemiBold"�5j   3   $��16(�6I   �� 5l   "   $��icon/taixe_logo.png��6(   $��10,0,0,0q�6E   $��35��6W   $��35��6c   $
��Left=�6n   5m      5n      5o      5p      5q      � 5s      $��2��6   $��2��6    $��2��60   .+�5t      �� -   5         ��Name$ lb_errorout��5t      $�� ��5u      $��0,0,0,0q�5v      #��[  5w      $
��Left=�5x      $��Bottom=�5y      5z      � 5|      $��1��6   $��1��6    .+�5}      �� $
��Tên��6   $��0, 0,0,0q�6&   #��[  68   �� 5~      $
��CMND��6   $��0,40,0,0q�6'   #��[  69   �� 5      $	��SDT��6   $��0,80,0,0q�6&   #��[  68   �� 5�      -   5        $ lb_custom_1��5�      $��
Tên quán��6-   $��	0,120,0,0q�6@   #��[  6S   �� 5�      -	   5        $ lb_custom_2��5�      $��Mã số thuế��6-   $��	0,160,0,0q�6B   #��[  6U   �� 5�      -
   5        $ lb_custom_3��5�      $��Loại ẩm thực��6-   $��	0,200,0,0q�6D   #��[  6W   �� 5�      -   5        $ lb_custom_4��5�      $��Biển số xe��6-   $��	0,240,0,0q�6B   #��[  6U   5�      � 5�      $��1��6   $��2��6    .+�5�      �� -   5         ��Name$ tb_ten��5�      $��0,0,0,0q�6*   $	��200��6;   #��[ 6G   �� 5�      -   5        $ tb_cmnd��5�      $��0,40,0,0q�6+   $	��200��6=   #��[ 6I   �� 5�      -   5        $ tb_sdt��5�      $��0,80,0,0q�6*   $	��200��6<   #��[ 6H   �� 5�      -   5        $ tb_custom_1��5�      $��	0,120,0,0q�6/   $	��200��6B   #��[ 6N   �� 5�      -   5        $ tb_custom_2��5�      $��	0,160,0,0q�6/   $	��200��6B   #��[ 6N   �� 5�      -   5        $ tb_custom_3��5�      $��	0,200,0,0q�6/   $	��200��6B   #��[ 6N   �� 5�      -   5        $ tb_custom_4��5�      $��	0,240,0,0q�6/   $	��200��6B   #��[ 6N   5�      � 5�      $��1��6   $��3��6    .+�5�      �� $��Username��6   $��0, 0,0,0q�6+   #��[  6=   �� 5�      $��Password��6   $��0,40,0,0q�6+   #��[  6=   �� 5�      $��Ngân hàng��6   $��0,80,0,0q�6,   #��[  6>   �� 5�      $��Số tài khoản��6   $��	0,120,0,0q�6/   #��[  6B   �� 5�      $��Số dư��6   $��	0,160,0,0q�6(   #��[  6;   �� 5�      $��Email��6   $��	0,200,0,0q�6(   #��[  6;   5�      � 5�      $��4��6   $��1��6#   .+�5�      �� -   5        $ tb_username��5�      $��0, 0,0,0q�6/   $	��200��6A   #��[ 6M   �� 5�      -   5        $ tb_password��5�      $��0,40,0,0q�6/   $	��200��6A   #��[ 6M   �� 5�      -   5        $ tb_nganhang��5�      $��0,80,0,0q�6/   $	��200��6A   #��[ 6M   �� 5�      -   5        $ tb_sotaikhoan��5�      $��	0,120,0,0q�61   $	��200��6D   #��[ 6P   �� 5�      -   5        $ tb_sodu��5�      $��	0,160,0,0q�6+   $	��200��6>   #��[ 6J   �� 5�      -   5        $ tb_email��5�      $��	0,200,0,0q�6,   $	��200��6?   #��[ 6K   5�      � 5�      $��4��6   $��2��6#   .+�5�      �� $
��Left=�6   $��Bottom=�64   $��40��6O   $	��140��6[   $��0q�6g   ���#79B1BB
�������$
 10��6�   ._�5�      �� -   5        $��
Đăng ký��5�      
�������$��SemiBold"�5�   0   $��18(�6F   
������ 
������ 5�   2   5�      �� 5�      $��Right=�6   $��Bottom=�65   $��40��6P   $��60��6\   $��0q�6g   ���#95DBD9
�������$
 10��6�   ._�5�      �� -   5        
�������$��SemiBold"�5�   0   $��18(�6F   
������ 
������ .��5�      �� $��icon/logout.png��6    $��30��69   5�      5�      5�      5�      5�      5�      