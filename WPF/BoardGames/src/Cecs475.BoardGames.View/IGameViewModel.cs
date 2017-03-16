using System;

namespace Cecs475.BoardGames.View {
	public interface IGameViewModel { 
		event EventHandler GameFinished;
	}
}
