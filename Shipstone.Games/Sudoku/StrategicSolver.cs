using System;
using System.Collections.Generic;

using Shipstone.Games.Sudoku.Solvers;

namespace Shipstone.Games.Sudoku
{
    /// <summary>
    /// Defines a method for solving a sudoku using strategy and pattern-matching.
    /// </summary>
    public class StrategicSolver : ISolver
    {
        internal readonly ISet<int>[,] _Candidates;
        internal readonly Grid _Grid;
        internal readonly List<Move> _Moves;

        /// <summary>
        /// Gets a read-only list of moves made by the <see cref="StrategicSolver" />.
        /// </summary>
        /// <value>A read-only list of moves made by the <see cref="StrategicSolver" />.</value>
        public IReadOnlyList<Move> Moves => this._Moves;

        /// <summary>
        /// Gets the sudoku that the <see cref="StrategicSolver" /> is attempting to solve.
        /// </summary>
        /// <value>The sudoku that the <see cref="StrategicSolver" /> is attempting to solve.</value>
        public IReadOnlySudoku Sudoku => this._Grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategicSolver" /> class that attempts to solve the specified sudoku.
        /// </summary>
        /// <param name="sudoku">The sudoku to solve.</param>
        /// <exception cref="ArgumentNullException"><c><paramref name="sudoku" /></c> is <c>null</c>.</exception>
        public StrategicSolver(IReadOnlySudoku sudoku)
        {
            Grid grid = new Grid(sudoku);
            ISet<int>[,] candidates = new ISet<int>[9, 9];

            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    if (sudoku[row, column] > 0)
                    {
                        continue;
                    }

                    ISet<int> cellCandidates = new HashSet<int>();

                    for (int n = 1; n < 10; n ++)
                    {
                        if (!(
                            sudoku.RowContains(row, n) ||
                            sudoku.ColumnContains(column, n) ||
                            sudoku.BlockContains(row, column, n)
                        ))
                        {
                            cellCandidates.Add(n);
                        }
                    }

                    candidates[row, column] = cellCandidates;
                }
            }

            this._Candidates = candidates;
            this._Grid = grid;
            this._Moves = new List<Move>();
        }

        bool ISolver.Solve() => throw new NotImplementedException();

        /// <summary>
        /// Attempts to make a single move using the specified <see cref="Strategy" /> to partially complete <see cref="StrategicSolver.Sudoku" />.
        /// </summary>
        /// <param name="strategy">The <see cref="Strategy" /> to use when making the move.</param>
        /// <returns><c>true</c> if <see cref="StrategicSolver.Sudoku" /> was partially completed using <c><paramref name="strategy" /></c>; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException"><c><paramref name="strategy" /></c> is not one of the <see cref="Strategy" /> values.</exception>
        public bool SolveSingle(Strategy strategy)
        {
            if (!Enum.IsDefined(typeof (Strategy), strategy))
            {
                throw new ArgumentException($"{nameof (strategy)} is not one of the Strategy values.");
            }

            StrategySolver solver =
                StrategySolver.CreateInstance(this, strategy);

            return solver.Solve();
        }
    }
}
