using System;

namespace Shipstone.Games.Sudoku
{
    /// <summary>
    /// Represents a read-only sudoku.
    /// </summary>
    public interface IReadOnlySudoku
    {
        /// <summary>
        /// Gets a value indicating whether the sudoku is complete.
        /// </summary>
        /// <value><c>true</c> if the sudoku is complete; otherwise, <c>false</c>. A sudoku is complete if it contains a non-zero value in each and every cell.</value>
        bool IsComplete { get; }

        /// <summary>
        /// Gets the value in the cell at the specified row and column in the sudoku.
        /// </summary>
        /// <param name="row">The zero-based row index of the cell to get.</param>
        /// <param name="column">The zero-based column index of the cell to get.</param>
        /// <value>The value in the cell at the specified <c><paramref name="row" /></c> and <c><paramref name="column" /></c> in the sudoku.</value>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="row" /></c> is less than 0 (zero) -or- <c><paramref name="row" /></c> is equal to or greater than 9 (nine) -or- <c><paramref name="column" /></c> is less than 0 (zero) -or- <c><paramref name="column" /></c> is equal to or greater than 9 (nine).</exception>
        int this[int row, int column] { get; }
    }
}
