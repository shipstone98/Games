using System;

namespace Shipstone.Games.Sudoku
{
    /// <summary>
    /// Defines a method for solving a sudoku using strategy and pattern-matching.
    /// </summary>
    public class StrategicSolver : ISolver
    {
        private readonly Grid _Grid;

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
        public StrategicSolver(IReadOnlySudoku sudoku) =>
            this._Grid = new Grid(sudoku);

        bool ISolver.Solve() => throw new NotImplementedException();
    }
}
