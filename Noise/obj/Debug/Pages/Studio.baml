   M S B A M L   `   `   `  ����@  <Noise, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Noise.MainPagesNoise.MainPages  5          Noise.MainPages.Studio   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Noise.MainPages     5        N� Title$  Studio��5	      .��5
      � -   5         �Name$ 
StudioPage��5   	    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��6* �6   .+�5   
   �� $��0��6   $��0��6"   .+�5      �� -   5        $��5q�5      $��10q�6       menuItem#���   5      
�������.��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      ��  �� BitmapScalingMode$ HighQuality=�6   $��40��6J   $��40��6U   $��/Assets/user-icon.png��5      $��0 0 10 0q�5      �� 5        editArtistCard#���  6   $
��16px(�5   $   &   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�5    $   
�������5!   9   5"      5#      5$      �� 5&   
   $��0��6   $��1��6"   .+�5'      �� -   5        $��5q�5'      $��10q�6    #���   5(      
�������.��5+      �� $ 
Horizontal=�6   .+�5,      �� $ HighQuality=�6   $��28��6J   $��28��6U   $��/Assets/icon-upload.png��5-      $��0 0 20 0q�5.      �� 5/        uploadSongButton#���  6   $
��16px(�50   $   $ 	ClearType=�51   $   
�������52   9   53      54      55      &   System.Windows.Controls.DataGrid  57   
   -   5          Name$ 	SongsList��57        	songsGrid#���  58        songListCollumnHeader   ColumnHeaderStyle# �  59        songListRows	   RowStyle#	 �  5:        songListCells
   	CellStyle#
 �  5;      $��0,10,0,0q�6?   $��1��6Q   $��2��5<   I      Columns 5=      0  *System.Windows.Controls.DataGridTextColumn  5>      ,  &System.Windows.Controls.DataGridColumn   Header$ song_id��6%      
Visibility$ Hidden=�6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   ��  �� Path$ song_id�6m   4  .System.Windows.Controls.DataGridTemplateColumn  5?        imageCollumn# �  6)      CellTemplate 5@      �� 5A      .R�5B      �� $	��100��6%   $	��100��5C       �� CornerRadius$ 15��5D      ��5E   "   �� 5F   &   $ HighQuality=�5G   %    j� Stretch$
 Fill=�5H   %    �� ImageSource 5I   %   �� $ image�5K      5L      5N        5O        nameCollumn# �  6%    6L   �� $
 name�  5P       	 playsCollumn# � 	 6%    6M   �� $ plays�  5Q       
 
pubCollumn# � 
 6%    6K   �� $ publicationDate�  5R       5S      �� 5T      .R�5U      �� $ 
Horizontal=�5V   !   .+�5W   "   �� -   5        $��5q�5W   )   $��10q�64     transparentButton#���  5X   !   
�������.��5[   &   �� $ 
Horizontal=�61   .+�5\   *   �� $ HighQuality=�60   $��28��6^   $��28��6i   $��/Assets/icon-edit.png��5]   )   $��0 0 10 0q�5^   )   5_   '   5`   #   5a      5b      5d      5f      5g      5h      