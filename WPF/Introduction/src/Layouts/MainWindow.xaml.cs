using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Layouts {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		private void mWrapBtn_Click(object sender, RoutedEventArgs e) {
			var panel = new WrapPanel();
			panel.Show();
		}

		private void mDockBtn_Click(object sender, RoutedEventArgs e) {
			var panel = new DockPanel();
			panel.Show();
		}

		private void mStackBtn_Click(object sender, RoutedEventArgs e) {
			var panel = new StackPanel();
			panel.Show();
		}

		private void mGridBtn_Click(object sender, RoutedEventArgs e) {
			var panel = new GridLayout();
			panel.Show();
		}
	}
}
