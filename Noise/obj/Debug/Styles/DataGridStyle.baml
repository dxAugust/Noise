   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   	songsGrid  �� 
TargetType&     System.Windows.Controls.DataGrid �� Property �� Value  PrimaryTextColor5   /System.Windows.Controls.DataGridLengthConverter �� 
TargetType �� Padding J� SnapsToDevicePixels ��Name � ColumnDefinitions � RowDefinitions	    SelectAllCommand
 �� 
ResourceId �� TypeInTargetAssembly!    HeadersVisibilityConverter �� 	Converter7   1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter �� RelativeSource �� AncestorTypeG   ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter  Name   Column ��Name �� CanContentScroll ��Name �� ScrollableHeight � Maximum �� Orientation �� ViewportHeight �� ViewportSize* �� #ComputedVerticalScrollBarVisibility � Value �� Mode �� ScrollableWidth  �� ViewportWidth,! �� %ComputedHorizontalScrollBarVisibility  songListRows)   #System.Windows.Controls.DataGridRow" ��Name# �� CornerRadius?   9System.Windows.Controls.Primitives.SelectiveScrollingGrid?   9System.Windows.Controls.Primitives.DataGridCellsPresenter$   
ItemsPanelA   ;System.Windows.Controls.Primitives.DataGridDetailsPresenter%   DetailsVisibility$&   SelectiveScrollingOrientation#'    RowDetailsScrollingConverter;   5System.Windows.Controls.SelectiveScrollingOrientation(   Vertical:	   4System.Windows.Controls.Primitives.DataGridRowHeader
)   Row* �� Triggers+ P� Property, P� Value  HoverDataGridItem  SelectedDataGridItem- ]� 
Conditions. �� Property/ �� Value  songListCells*
   $System.Windows.Controls.DataGridCell  songListCollumnHeader=   7System.Windows.Controls.Primitives.DataGridColumnHeader  SecondaryTextColor�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %�  &	        7  &	 ]
    7  7  7  7  7  7  &	     &	 �    7  �� 5      "    6   .�5   
   ��  � ��
 ���� 6'   �� 5   
    � ��8   6'   �� 5   
    � ��$ 0q�6,   �� 5   
     �@  RowDetailsVisibilityMode$ VisibleWhenSelected=�65   �� 5   
    � �� . 6:   �� 5	   
    �@��PanningMode$
 Both=�65   �� 5
   
    �@��IsFlicksEnabled .  63   �� 5   
    �@  AutoGenerateColumns .  60   �� 5   
    �@  HeadersVisibility$ Column=�6.   �� 5   
    � ��$ 0 10 0 0q�6#   �� 5   
    � ��$ 2��6%   �� 5   
    �@  SelectionMode$ Single=�6*   �� 5   
    �@  SelectionUnit$ FullRow=�6*   �� 5   
    �@  CanUserAddRows .  6+   �� 5   
    �@  CanUserDeleteRows .  6.   �� 5   
    �@  CanUserReorderColumns . 62   �� 5   
    �@  
IsReadOnly . 6'   �� 5   
    �@  CanUserResizeRows .  6.   �� 5   
    �@  CanUserResizeColumns .  61   �� 5   
    � ��$ 14(�6%   �� 5   
    �@  GridLinesVisibility$
 None=�60   �� 5   
    �@  ColumnWidth$ * 6(   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� #��z��6   #��z��6G   #��z��6{   # z��6�    . ._�5      �� $ DG_ScrollViewer��6'   |�.  ��5       �� 5!   "   " ��62   .R�5"   &   �  5#   *   �� 5$   .   $
��Auto �6?   �� 5%   .   $��* �6?   �� 5&   .   $
��Auto �6?    5(   *   �� 5)   .   $
��Auto �6<   �� 5*   .   $��* �6<   �� 5+   .   $
��Auto �6<   .+�5-   *   �� #��Z	 61   |�.  ��6r   C� *�� $"
 DataGridSelectAllButtonStyle��"   +y�6�   �� *HeadersVisibility+# Z # Z  �� "   ��6�  �� *CellsPanelHorizontalOffset+ �� "     5.   *   $! PART_ColumnHeadersPresenter��6I   $��1��6n   y�6~   �� *HeadersVisibility+# Z # Z  �� "   �� 5/   *   $! PART_ScrollContentPresenter��6A   $��2��6f   # z��6z   $��1��6�   �� 50   *   $ PART_VerticalScrollBar��64   $��2��6T   # z 6d   $ Vertical=�6�   $��1��6�   # z 6�   #y�z 6D   6�   �� *VerticalOffset+$ OneWay=� �� *TemplatedParent+6D  � 51   *   $��1��6/   $��2��6?    52   .   �� 53   2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "   �� 54   2   $��* �6C   .+�56   .   �� $ PART_HorizontalScrollBar��68   $��1��6Z   # z 6j   $ 
Horizontal=�6�   # z  6�   #y�z! 6?   6�   �� *HorizontalOffset+$ OneWay=� �� *TemplatedParent+6?  57   +   58   '   59   #   .��5;      �� # z 6-   5<      5=      5>      5@      5A      �� 5C      "   6!   .�5D   
   ��  � ��$ 0 4 0 0q�6#   �� 5E   
    � ��
 ���� 6'   �� 5F   
    �@J�SnapsToDevicePixels . 60   �� 5G   
    �@=�ErrorTemplate 65   Z� �� 5H   
    �@ ValidationErrorTemplate 5I      �� 5J      .R�5K      �� 
���  ��$��2,0,0,0q�61   $��!��6B   $��Center=�6K   5L      5N      �� 5O   
    � �� 5P      �� 5Q      "  6"   .R�5R      �� $" 
DGR_Border��6   $	 0 5q�61   $# 5��6?   #��z��6P   $��0q�6z    . ._�5S         5T      �� 5U   "   $
��Auto �63   �� 5V   "   $��* �63    5X      �� 5Y   "   $��* �60   �� 5Z   "   $
��Auto �60   .+�5\        $��1��65   #��z$ 6E   # z 6o     5]      $��1��67   $��1��6G   #y�z% 6k  & 6T   �� *AreRowDetailsFrozen+# Z' # Z(  �� "   6k  	  5^      $��2��60   $& Vertical=�6A   y�6�   �� *HeadersVisibility+# Z # Z)  �� "   5_      5`      5a      5c      * 5e   
   P� 5f      + �@J�IsMouseOver, . . �5g      ��  � ��8   6/   �� 5h       � ��8  6/   5i      P� 5j      + �@ 
IsSelected, . . �5k      ��  � ��8  6/   �� 5l       � ��8  6/   5m      ]� 5n      - 5o      �� 5p      . �@ 
IsSelected/ . 66   �� 5q      . �@��IsSelectionActive/ .  6F   ./�5s      ��  � ��8  6/   �� 5t       � ��8  6/   5u      5w      �� 5y      "  
 6"   .�5z   
   ��  � ��
 ���� 6'   �� 5{   
    � ��$ Center=�6.   �� 5|   
    �@�FocusVisualStyle 6-   Z� �� 5}   
    � ��$
 15 0q�6#   �� 5~   
    �@
 	IsEditing .  6&   �� 5   
    � �� 5�      �� 5�      " 
 6"   .R�5�      �� #��z��6   $��0q�6G    . ._�5�      �� $��Center=�6+   # z 6F   5�      5�      5�      5�      �� 5�      "   6*   .�5�   
   ��  � ��$ Center=�65   �� 5�   
    � ��
 ���� 6'   �� 5�   
    � ��8   6'   �� 5�   
    � ��$ 
15 0 15 10q�6#   5�      5�      