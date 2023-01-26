namespace Shipstone.Games.Sudoku.Solving
{
    /// <summary>
    /// Defines a method for solving a sudoku.
    /// </summary>
    public interface ISolver
    {
        /// <summary>
        /// Gets the sudoku that the <see cref="ISolver" /> is attempting to solve.
        /// </summary>
        /// <value>The sudoku that the <see cref="ISolver" /> is attempting to solve.</value>
        IReadOnlySudoku Sudoku { get; }

        /// <summary>
        /// Attempt to solve <see cref="ISolver.Sudoku" />.
        /// </summary>
        /// <returns><c>true</c> if <see cref="ISolver.Sudoku" /> was solved successfully; otherwise, <c>false</c>.</returns>
        bool Solve();
    }
}
