using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cecs475.BoardGames.View {
	/// <summary>
	/// Represents a type of game that can be played in a WPF application.
	/// </summary>
	public interface IGameType {
		/// <summary>
		/// The name of the game, to be displayed in a user interface.
		/// </summary>
		string GameName { get; }

		/// <summary>
		/// Creates and returns a WPF Control containing a View for the game.
		/// </summary>
		Control GetViewControl();
	}
}
