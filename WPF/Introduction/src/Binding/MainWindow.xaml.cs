using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Binding {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		// An ObservableCollection is a list that sends events when items are added or removed.
		private ObservableCollection<string> mItems = new ObservableCollection<string>();

		public MainWindow() {
			InitializeComponent();

			// Data binding allows us to set an object as the "context" for a UI control. The control can then specify
			// many of its properties in terms of the context it is bound to, such that when the context updates itself,
			// the UI automatically updates.

			// Create a Employee object and bind it to our two controls.
			Employee e = new Binding.Employee() {
				Age = 20
			};
			mSalaryLabel.DataContext = e;
			mAgeText.DataContext = e;
			// View this binding in action....




			// Create an Employee that notifies when its properties change.
			NotifyingEmployee ne = new NotifyingEmployee() {
				Age = 30
			};
			mSalary2Label.DataContext = ne;
			mAge2Text.DataContext = ne;


			// Some controls act on collections of data. If we give them an ObservableCollection, they will rebuild their
			// UI every time the collection changes.
			mList.ItemsSource = mItems;
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			// Set the first employee's age to 100, which does not update the UI.
			(mSalaryLabel.DataContext as Employee).Age = 100;
		}

		private void Button2_Click(object sender, RoutedEventArgs e) {
			// Set the second employee's age to 100. Because we have two-way binding with a model that implements
			// INotifyPropertyChanged, the UI will automatically update.
			(mSalary2Label.DataContext as NotifyingEmployee).Age = 100;
		}


		private void mAddBtn_Click(object sender, RoutedEventArgs e) {
			// Add an element to our member list, which will rebuild the ListView object.
			mItems.Add(mAddText.Text);
			mAddText.Focus();
			mAddText.SelectAll();
		}

	}
}
