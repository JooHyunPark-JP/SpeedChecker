using SpeedCheckers.Contracts.SpeedCheckers.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpeedCheckers.Contracts;

namespace SpeedCheckers.BLL.SpeedCheckersRules
{
	public class SpeedCheckersRules : IBoardGameRules<Checkers>
	{
        int whiteCheckers = 0;
        int blackCheckers = 0;
        public Checkers[,] GetInitialBoard()
		{
			return new Checkers[,] {
				{  Checkers.Black, Checkers.Empty,Checkers.Black, Checkers.Empty,Checkers.Black, },
				{  Checkers.Empty, Checkers.Black,Checkers.Empty, Checkers.Black,Checkers.Empty, },
				{  Checkers.Empty, Checkers.Empty,Checkers.Empty, Checkers.Empty,Checkers.Empty, },
				{  Checkers.Empty, Checkers.White,Checkers.Empty, Checkers.White,Checkers.Empty, },
				{  Checkers.White, Checkers.Empty,Checkers.White, Checkers.Empty,Checkers.White, },
			};
		}
        // checks for the winner by checking the pieces left on the game board
		public Guid? GetWinner(BoardGameState<Checkers> state, Guid white, Guid black)
		{
			Guid? winner = null;

			foreach (var c in state.Board)
			{
				if (c == Checkers.White || c == Checkers.WhiteKing)
				{
					whiteCheckers += 1;
				}
				if (c == Checkers.Black || c == Checkers.BlackKing)
				{
					blackCheckers += 1;
				}
			}
			if (whiteCheckers <= 1  && BoardGameMoveResult<Checkers>.ok == false)
			{
				winner = black;
			}
			if (blackCheckers <= 1 && BoardGameMoveResult<Checkers>.ok == false)
			{
				winner = white;
			}
			return winner;
		}

		public BoardGameMoveResult<Checkers> MakeMove(BoardGameState<Checkers> state, string from, string to)
		{
			var xs = "ABCDE";
			var ys = "12345";
			int fromX = xs.IndexOf(from[0]);
			int fromY = ys.IndexOf(from[1]);
			int toX = xs.IndexOf(to[0]);
			int toY = ys.IndexOf(to[1]);

			// check if checker is in "from" position, return error result if not
			var dx = toX - fromX;
			var dy = toY - fromY;

			// check if move is in list of all valid moves
			var isValidMove = GetPossibleMoves(state, fromX, fromY)
								.Where(x => x.Item1 == dx && x.Item2 == dy)
								.Count() != 0;
			// if not, do not change state and return current state
			if (!isValidMove)
			{
				return new BoardGameMoveResult<Checkers>
				{
					Ok = false,
					State = state

				};
			}
			// update state by current move

			// move checker
			var checker = state.Board[fromY, fromX];
			state.Board[fromY, fromX] = Checkers.Empty;
			state.Board[toY, toX] = checker;
			// capture if needed
			if (dx == 2 || dx == -2)
			{
				state.Board[fromY + dy / 2, fromX + dx / 2] = Checkers.Empty;
			}
			// promote to king
			if (checker == Checkers.White && toY == 0)
			{
				state.Board[toY, toX] = Checkers.WhiteKing;
			}
			if (checker == Checkers.Black && toY == 4)
			{
				state.Board[toY, toX] = Checkers.BlackKing;
			}

			return new BoardGameMoveResult<Checkers>
			{
				Ok = true,
				State = state
			};

		}

		List<Tuple<int, int>> GetPossibleMoves(BoardGameState<Checkers> state, int x, int y)
		{
			var moves = new List<Tuple<int, int>>() {
				GetValidMove(state, x, y, 1, 1),
				GetValidMove(state, x, y, 1, -1),
				GetValidMove(state, x, y, -1, -1),
				GetValidMove(state, x, y, -1, 1),
				GetValidMove(state, x, y, 2, 2),
				GetValidMove(state, x, y, 2, -2),
				GetValidMove(state, x, y, -2, -2),
				GetValidMove(state, x, y, -2, 2),
			};
			moves = moves.Where(m => m != null).ToList();
			return moves;
		}

		// returns null if move is invalid, otherwise (dx, dy) of checker
		Tuple<int, int> GetValidMove(BoardGameState<Checkers> state, int x, int y, int dx, int dy)
		{
			var checker = state.Board[y, x];
			// if no checker at position
			if (checker == Checkers.Empty) { return null; }
			//check that is moving player's own figure
			if (state.CurrentPlayer == state.WhitePlayerId && (checker == Checkers.Black || checker == Checkers.BlackKing)){
				return null;
			}
			if (state.CurrentPlayer != state.WhitePlayerId && (checker == Checkers.White || checker == Checkers.WhiteKing)){
				return null;
			}
			// if move is outside of board
			var newX = x + dx;
			if (newX > 4 || newX < 0) { return null; }
			var newY = y + dy;
			if (newY > 4 || newY < 0) { return null; }

			// black can't move up
			if (checker == Checkers.Black && dy < 0) { return null; }
			// white can't move down
			if (checker == Checkers.White && dy > 0) { return null; }

			if (state.Board[y + dy, x + dx] != Checkers.Empty)
			{
				return null;
			}
			if (dx == 2)
			{
				var mX = x + dx / 2;
				var mY = y + dy / 2;
				var otherChecker = state.Board[mY, mX];
				if (otherChecker == Checkers.Empty)
				{
					return null;
				}
				// can't capture own checkers
				if (checker == Checkers.Black || checker == Checkers.BlackKing)
				{
					if (otherChecker == Checkers.Black || otherChecker == Checkers.BlackKing)
					{
						return null;
					}
				}
				if (checker == Checkers.White || checker == Checkers.WhiteKing)
				{
					if (otherChecker == Checkers.White || otherChecker == Checkers.WhiteKing)
					{
						return null;
					}
				}
			}

			return Tuple.Create(dx, dy);
		}
	}
}