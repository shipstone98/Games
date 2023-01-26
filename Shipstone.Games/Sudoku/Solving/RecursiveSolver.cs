using System;
using System.Collections.Generic;

using Shipstone.Utilities.Collections;

namespace Shipstone.Games.Sudoku.Solving
{
    /// <summary>
    /// Defines a method for solving a sudoku using recursion (a.k.a. "brute-forcing").
    /// </summary>
    public class RecursiveSolver : ISolver
    {
        private int _Count;
        internal readonly Grid _Grid;

        /// <summary>
        /// Gets the sudoku that the <see cref="RecursiveSolver" /> is attempting to solve.
        /// </summary>
        /// <value>The sudoku that the <see cref="RecursiveSolver" /> is attempting to solve.</value>
        public IReadOnlySudoku Sudoku => this._Grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecursiveSolver" /> class that attempts to solve the specified sudoku.
        /// </summary>
        /// <param name="sudoku">The sudoku to solve.</param>
        /// <exception cref="ArgumentNullException"><c><paramref name="sudoku" /></c> is <c>null</c>.</exception>
        public RecursiveSolver(IReadOnlySudoku sudoku) =>
            this._Grid = new Grid(sudoku);

        /// <summary>
        /// Attempt to solve <see cref="RecursiveSolver.Sudoku" />.
        /// </summary>
        /// <returns><c>true</c> if <see cref="RecursiveSolver.Sudoku" /> was solved successfully; otherwise, <c>false</c>.</returns>
        public bool Solve() => this.Solve(0, 0);

        private bool Solve(int row, int column)
        {
            if (this._Grid._Cells[row, column] > 0)
            {
                return this.SolveNext(row, column);
            }

            List<int> candidates = new List<int>(9);

            for (int n = 1; n < 10; n ++)
            {
                if (!(
                    this._Grid.RowContains(row, n) ||
                    this._Grid.ColumnContains(column, n) ||
                    this._Grid.BlockContains(row, column, n)
                ))
                {
                    candidates.Add(n);
                }
            }

            candidates.Shuffle(0, candidates.Count);

            foreach (int candidate in candidates)
            {
                this._Grid._Cells[row, column] = candidate;

                if (this.SolveNext(row, column))
                {
                    return true;
                }
            }

            this._Grid._Cells[row, column] = 0;
            return false;
        }

        private bool SolveNext(int row, int column)
        {
            if (++ column == 9)
            {
                if (++ row == 9)
                {
                    ++ this._Count;
                    return true;
                }

                column = 0;
            }

            return this.Solve(row, column);
        }
    }
}
