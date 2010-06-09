﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;

namespace Client
{
	/// <summary>
	/// Classe de lancement et du splashscreen.
	/// </summary>
	public partial class Login : Window
	{
		protected MainWindow t;
		public Login()
		{
			InitializeComponent();
			this.Show();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			
			new Thread((ThreadStart)DelayedStart).Start();

		}
		CacheWrapper c;
		private void DelayedStart()
		{
			c = new CacheWrapper();
			this.Dispatcher.BeginInvoke((ThreadStart)Hiding);
			Thread.Sleep(9000);
		}

		private void Hiding()
		{
			t = new MainWindow();
			t.Visibility = System.Windows.Visibility.Hidden;
		}
		private void Ending()
		{
			t.Show();
			Thread.Sleep(500);
			t.Visibility = System.Windows.Visibility.Visible;
			this.Visibility = System.Windows.Visibility.Collapsed;
			//this.Close();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (c.logCacheProcess(loginBox.Text, passwordBox.Password))
				this.Dispatcher.BeginInvoke((ThreadStart)Ending);
			else
			{
				errorImage.Visibility = System.Windows.Visibility.Visible;
				errorMessage.Visibility = System.Windows.Visibility.Visible;
			}

		}

		private void loginBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				Button_Click(null, null);

		}
	}
}