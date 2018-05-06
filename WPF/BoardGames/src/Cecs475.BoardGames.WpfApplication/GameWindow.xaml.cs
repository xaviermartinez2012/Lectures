using Cecs475.BoardGames.WpfView;
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

namespace Cecs475.BoardGames.WpfApplication {
	public partial class GameWindow : Window {
		/// <summary>
		/// Creates a GameWindow containing the given game View control.
		/// </summary>
		public GameWindow(IWpfGameFactory factory) {
			// Set the DynamicResource named GameView to the given control.
			var gameView = factory.CreateGameView();
			this.Resources.Add("GameView", gameView.ViewControl);
			this.Resources.Add("ViewModel", gameView.ViewModel);
			InitializeComponent();

		}

		private void UndoButton_Click(object sender, RoutedEventArgs e) {
			(FindResource("ViewModel") as IGameViewModel).UndoMove();
		}
	}
}
