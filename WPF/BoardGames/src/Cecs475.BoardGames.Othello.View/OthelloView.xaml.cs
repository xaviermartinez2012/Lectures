using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Cecs475.BoardGames.Othello.View {
	/// <summary>
	/// Interaction logic for OthelloView.xaml
	/// </summary>
	public partial class OthelloView : UserControl {
		public static SolidColorBrush RED_BRUSH = new SolidColorBrush(Colors.Red);
		public static SolidColorBrush GREEN_BRUSH = new SolidColorBrush(Colors.Green);

		public OthelloView() {
			InitializeComponent();
		}

		private void Border_MouseEnter(object sender, MouseEventArgs e) {
			Border b = sender as Border;
			var square = b.DataContext as OthelloSquare;
			var vm = FindResource("vm") as OthelloViewModel;
			if (vm.PossibleMoves.Contains(square.Position)) {
				b.Background = RED_BRUSH;
			}
		}

		private void Border_MouseLeave(object sender, MouseEventArgs e) {
			Border b = sender as Border;
			b.Background = GREEN_BRUSH;
		}

		public OthelloViewModel Model {
			get { return FindResource("vm") as OthelloViewModel; }
		}

		private void Border_MouseUp(object sender, MouseButtonEventArgs e) {
			Border b = sender as Border;
			var square = b.DataContext as OthelloSquare;
			var vm = FindResource("vm") as OthelloViewModel;
			if (vm.PossibleMoves.Contains(square.Position)) {
				vm.ApplyMove(square.Position);
				b.Background = GREEN_BRUSH;
			}
		}
	}

	/// <summary>
	/// Converts from an integer player number to an Ellipse representing that player's token.
	/// </summary>
	public class OthelloSquarePlayerConverter : IValueConverter {
		private static SolidColorBrush WHITE_BRUSH = new SolidColorBrush(Colors.White);
		private static SolidColorBrush BLACK_BRUSH = new SolidColorBrush(Colors.Black);

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			int player = (int)value;
			if (player == 0) {
				return null;
			}

			Ellipse token = new Ellipse() {
				Fill = GetFillBrush(player)
			};
			return token;
		}

		private static SolidColorBrush GetFillBrush(int player) {
			if (player == 1)
				return BLACK_BRUSH;
			return WHITE_BRUSH;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
