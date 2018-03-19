using Cecs475.BoardGames.WpfView;

namespace Cecs475.BoardGames.TicTacToe.View {
	public class TicTacToeGameFactory : IWpfGameFactory {
		public string GameName {
			get {
				return "Tic Tac Toe";
			}
		}

		public IWpfGameView CreateGameView() {
			return new TicTacToeView();
		}
	}
}
