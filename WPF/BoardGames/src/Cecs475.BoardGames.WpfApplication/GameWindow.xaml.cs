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
		public GameWindow(Control gameControl) {
			InitializeComponent();
			// Set the DynamicResource named GameView to the given control.
			this.Resources.Add("GameView", gameControl);
		}
	}
}
