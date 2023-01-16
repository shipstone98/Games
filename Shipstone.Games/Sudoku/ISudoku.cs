using System;

namespace Shipstone.Games.Sudoku
{
    /// <summary>
    /// Represents a read-only sudoku.
    /// </summary>
    public interface ISudoku
    {
        /// <summary>
        /// Gets a value indicating whether the <see cref="ISudoku" /> is complete.
        /// </summary>
        /// <value><c>true</c> if the <see cref="ISudoku" /> is complete; otherwise, <c>false</c>. A sudoku is complete if it contains a non-zero value in each and every cell.</value>
        bool IsComplete { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ISudoku" /> is read-only.
        /// </summary>
        /// <value><c>true</c> if the <see cref="ISudoku" /> is read-only; otherwise, <c>false</c>.</value>
        bool IsReadOnly { get; }

        /// <summary>
        /// Gets or sets the value in the cell at the specified row and column in the <see cref="ISudoku" />.
        /// </summary>
        /// <param name="row">The zero-based row index of the cell to get or set.</param>
        /// <param name="column">The zero-based column index of the cell to get or set.</param>
        /// <value>The value in the cell at the specified <c><paramref name="row" /></c> and <c><paramref name="column" /></c> in the <see cref="ISudoku" />.</value>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="row" /></c> is less than 0 (zero) -or- <c><paramref name="row" /></c> is equal to or greater than 9 (nine) -or- <c><paramref name="column" /></c> is less than 0 (zero) -or- <c><paramref name="column" /></c> is equal to or greater than 9 (nine) -or- the property is set and the value is less than 0 (zero) -or- the property is set and the value is greater than 9 (nine).</exception>
        /// <exception cref="NotSupportedException">The sudoku is read-only.</exception>
        int this[int row, int column] { get; set; }
    }
}
