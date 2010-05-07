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
	public partial class TteSplash : Window
	{
		public TteSplash()
		{
			
			InitializeComponent();
			this.Show();
			this.BeginStoryboard((Storyboard)FindResource("Storyboard1"));
			// Insert code required on object creation below this point.
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			t = new MainWindow();
			new Thread((ThreadStart)thded).Start();
		}

		static MainWindow t;
		private void thded()
		{
			CacheWrapper c = new CacheWrapper();
			t.Dispatcher.BeginInvoke((ThreadStart)nothded);
		}
		private void nothded()
		{
			t.Show();
			this.Close();
		}

	}
}