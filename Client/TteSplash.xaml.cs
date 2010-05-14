using System;
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
	public partial class TteSplash : Window
	{
		protected MainWindow t;
		public TteSplash()
		{
			InitializeComponent();
			this.Show();
			this.BeginStoryboard((Storyboard)FindResource("Storyboard1"));
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			new Thread((ThreadStart)DelayedStart).Start();
			t = new MainWindow();
		}
		private void DelayedStart()
		{
			CacheWrapper c = new CacheWrapper();
			this.Dispatcher.BeginInvoke((ThreadStart)Hiding);
			t.Dispatcher.BeginInvoke((ThreadStart)Ending);
		}

		private void Hiding()
		{	this.Visibility = System.Windows.Visibility.Hidden;
			t.Visibility = System.Windows.Visibility.Hidden;
		}
		private void Ending()
		{
			t.Show();
			Thread.Sleep(500);
			t.Visibility = System.Windows.Visibility.Visible;
			this.Close();
		}
	}
}