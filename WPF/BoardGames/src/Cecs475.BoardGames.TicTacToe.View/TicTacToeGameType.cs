using Cecs475.BoardGames.View;

namespace Cecs475.BoardGames.TicTacToe.View {
	public class TicTacToeGameType : IGameType {
		public string GameName {
			get {
				return "TicTacToe";
			}
		}

		public System.Windows.Controls.Control GetViewControl() {
			return new TicTacToeView();
		}
	}
}
