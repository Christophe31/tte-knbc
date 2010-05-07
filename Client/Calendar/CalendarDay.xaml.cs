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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
	/// <summary>
	/// Interaction logic for CalendarDay.xaml
	/// </summary>
	public partial class CalendarDay : UserControl
	{
		public CalendarDay()
		{
			this.InitializeComponent();
		}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 24; i++)
            {
                LayoutRoot.RowDefinitions.Add(new RowDefinition());

                TextBlock text = new TextBlock();
                text.Text = i.ToString();
                Grid.SetColumn(text, 0);
                Grid.SetRow(text, i);

                LayoutRoot.Children.Add(text);
            }
        }
	}
}