using Xunit;
using Othello.Game;
using FluentAssertions;

/// <summary>
/// A test file for the Othello model classes. 
/// </summary>
namespace Othello.Tests {
	public class Tests {
		[Fact]
		public void NewValue() {
			// New board Value is 0.
			OthelloBoard board = new OthelloBoard();
			board.CurrentAdvantage.Advantage.Should().Be(0, "New board value is 0");
		}

		[Fact]
		public void NewHistory() {
			// New board MoveHistory is empty.
			OthelloBoard board = new OthelloBoard();
			board.MoveHistory.Should().BeEmpty("New board has no history");
		}

		[Fact]
		public void ValueAfterMultiDirectionMove() {
			// Value updated correctly after doing a multi-directional move.
			OthelloBoard board = new OthelloBoard();
			board.ApplyMove(new OthelloMove(new BoardPosition(3, 2)));
			board.ApplyMove(new OthelloMove(new BoardPosition(4, 2)));
			board.CurrentAdvantage.Advantage.Should().Be(0);
			board.ApplyMove(new OthelloMove(new BoardPosition(5, 2)));
			board.CurrentAdvantage.Advantage.Should().Be(5);
		}
	}
}
