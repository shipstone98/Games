using System;
using System.Collections.Generic;

using Shipstone.Games.Sudoku.Solving.Solvers;

namespace Shipstone.Games.Sudoku.Solving
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
        /// Attempt to solve <see cref="StrategicSolver.Sudoku" /> using the specified <see cref="Strategy" /> values.
        /// </summary>
        /// <param name="strategies">A collection containing the <see cref="Strategy" /> values to use when making moves.</param>
        /// <returns><c>true</c> if <see cref="StrategicSolver.Sudoku" /> was solved successfully using <c><paramref name="strategies" /></c>; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException"><c><paramref name="strategies" /></c> is empty -or- <c><paramref name="strategies" /></c> contains one or more elements that are not any of the <see cref="Strategy" /> values.</exception>
        /// <exception cref="ArgumentNullException"><c><paramref name="strategies" /></c> is <c>null</c>.</exception>
        public bool Solve(IEnumerable<Strategy> strategies)
        {
            if (strategies is null)
            {
                throw new ArgumentNullException(
                    nameof (strategies),
                    $"{nameof (strategies)} is null."
                );
            }

            IReadOnlyCollection<Strategy> sortedStrategies =
                new SortedSet<Strategy>(strategies);

            if (sortedStrategies.Count == 0)
            {
                throw new ArgumentException(
                    $"{nameof (strategies)} is empty.",
                    nameof (strategies)
                );
            }

            foreach (Strategy strategy in sortedStrategies)
            {
                if (!Enum.IsDefined(typeof (Strategy), strategy))
                {
                    throw new ArgumentException(
                        $"{nameof (strategies)} contains one or more elements that are not any of the Strategy values.",
                        nameof (strategies)
                    );
                }
            }
                
            return this.SolveUsing(sortedStrategies);
        }

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

        internal bool SolveUsing(IEnumerable<Strategy> strategies)
        {
            IEnumerator<Strategy> enumerator = strategies.GetEnumerator();

            while (enumerator.MoveNext())
            {
                StrategySolver solver =
                    StrategySolver.CreateInstance(this, enumerator.Current);

                if (solver.Solve())
                {
                    enumerator.Reset();
                }
            }

            return this._Grid.IsComplete;
        }
    }
}
