using Cecs475.BoardGames.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cecs475.BoardGames.Othello.View {
	/// <summary>
	/// Represents an Othello game that can be played in a WPF app.
	/// </summary>
	public class OthelloGameType : IGameType {
		public string GameName {
			get {
				return "Othello";
			}
		}

		public Control GetViewControl() {
			return new OthelloView();
		}
	}
}
