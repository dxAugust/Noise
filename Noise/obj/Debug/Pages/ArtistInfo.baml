   M S B A M L   `   `   `  ����@  <Noise, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Noise.Pages.StudioPagesNoise.Pages.StudioPages  5      (    "Noise.Pages.StudioPages.ArtistInfo   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:Noise.Pages.StudioPages     5        N� Title$  
ArtistInfo��5	      .��5
      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $
��1.5* �6   .+�5   
   �� ��5      �� 5       �� BitmapScalingMode$ HighQuality=�5       j� Stretch$
 Fill=�5      -   5        artistBanner��5      .+�5      �� $��Bottom=�5      .+�5      �� $��20q�5      ._�5      �  � Orientation$ Vertical=�5      .+�5       �� -   5         ��Name$ 
artistName��5&      &   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�5!      $��Segoe UI)�5"      $��24(�5#         PrimaryTextColor#���   5$      $
��Bold"�5%      .��5&   -   
ArtistName5(      �� 5*      -   5        $ artistDescription��5/      $ 	ClearType=�5+      $��Segoe UI)�5,      $��16(�5-        SecondaryTextColor#���  5.      .��5/   4   4AM I have to sleep51      52      53      54      55      �� 57   
   $��1��6    �� Orientation$ Vertical=�58      $��Center=�59      .+�5:      �� $ 	ClearType=�5;      $��Segoe UI)�5<      $��24(�5=      #���   5>        releaseLabel#���  5?      $
��Bold"�5@      � 5B      -   5         �Name$ lastRelease��5C      .+�5E      �� $	��100��6   $	��100��5F      	 �� CornerRadius$	 15��5G      $��20q�5H      $
��Left=�5I      ��5J      �� 5K      $ HighQuality=�5L      $
 Fill=�5M      -   5        lastReleaseImage��
 �� ImageSource$$
 /Assets/music_no_thumbnail.png��5O      5Q      � 5R      $ Vertical=�5S      $��Center=�5T      .+�5U      �� -   5        $ lastReleaseName��5[      $ 	ClearType=�5V      $��Segoe UI)�5W      $��18(�5X      #���   5Y      $
��Bold"�5Z      .��5[   2   LAST RELEASE NAME5]      �� 5_      -   5        $ lastReleaseDate��5d      $ 	ClearType=�5`      $��Segoe UI)�5a      $��16(�5b        MainTextColor#���  5c      .��5d   2   12 JUN5f      5g      5h      5i      5j      5k      