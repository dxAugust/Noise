﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Noise
{
    /// <summary>
    /// Логика взаимодействия для MusicPlatform.xaml
    /// </summary>
    public partial class MusicPlatform : Window
    {
        public MusicPlatform()
        {
            InitializeComponent();
        }

        private void dragAWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

            }
        }
    }
}
