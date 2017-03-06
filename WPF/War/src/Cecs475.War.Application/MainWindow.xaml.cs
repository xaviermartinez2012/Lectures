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

namespace Cecs475.War.Application {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		private void mDealButton_Click(object sender, RoutedEventArgs e) {
			// Retrieve the ViewModel from the window, then invoke the DealOneCard method.
			var model = this.FindResource("WarViewModel") as WarViewModel;
			model.DealOneCard();

			// NOTICE: we do not update the Images, the score labels, or any other control
			// ourself. They are all updated through data bindings to the ViewModel.
			// AMAZING.
		}
	}
}
