﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Administration.xaml
    /// </summary>
    public partial class Administration : UserControl
    {
        public Administration()
        {
            InitializeComponent();
        }

        private void bPromo_AddMod_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(generalAdmin.ActualHeight + " " + generalAdmin.ActualWidth);
        }
    }
}
