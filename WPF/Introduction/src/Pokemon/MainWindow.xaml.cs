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

namespace Pokemon {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
			
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			Model.Pokemon togekiss = this.FindResource("Togekiss") as Model.Pokemon;
			Model.Pokemon charmander = this.FindResource("Charmander") as Model.Pokemon;

			togekiss.AttackTarget(charmander, togekiss.Power);
			charmander.AttackTarget(togekiss, charmander.Power);
		}

		private void mImage2_MouseDown(object sender, MouseButtonEventArgs e) {
			if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2) {
				mImage2.Source = new BitmapImage(new Uri("/Resources/charizard.png", UriKind.Relative));
				Model.Pokemon charmander = this.FindResource("Charmander") as Model.Pokemon;
				charmander.HP = 150;
				charmander.Attack = 150;
				charmander.Defense = 100;
				charmander.Power = 120;

			}
		}
	}
}
