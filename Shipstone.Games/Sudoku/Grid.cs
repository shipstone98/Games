using System;

namespace Shipstone.Games.Sudoku
{
    /// <summary>
    /// Represents a sudoku as a mutable grid of numbers.
    /// </summary>
    public class Grid : IReadOnlySudoku, ISudoku
    {
        private readonly int[,] _Cells;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Grid" /> is complete.
        /// </summary>
        /// <value><c>true</c> if the <see cref="Grid" /> is complete; otherwise, <c>false</c>. A grid is complete if it contains a non-zero value in each and every cell.</value>
        public bool IsComplete
        {
            get
            {
                foreach (int cell in this._Cells)
                {
                    if (cell == 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        bool ISudoku.IsReadOnly => false;

        /// <summary>
        /// Gets or sets the value in the cell at the specified row and column in the <see cref="Grid" />.
        /// </summary>
        /// <param name="row">The zero-based row index of the cell to get or set.</param>
        /// <param name="column">The zero-based column index of the cell to get or set.</param>
        /// <value>The value in the cell at the specified <c><paramref name="row" /></c> and <c><paramref name="column" /></c> in the <see cref="Grid" />.</value>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="row" /></c> is less than 0 (zero) -or- <c><paramref name="row" /></c> is equal to or greater than 9 (nine) -or- <c><paramref name="column" /></c> is less than 0 (zero) -or- <c><paramref name="column" /></c> is equal to or greater than 9 (nine) -or- the property is set and the value is less than 0 (zero) -or- the property is set and the value is greater than 9 (nine).</exception>
        public int this[int row, int column]
        {
            get
            {
                Internals.CheckIndices(row, column);
                return this._Cells[row, column];
            }

            set
            {
                Internals.CheckIndices(row, column);

                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof (value),
                        $"{nameof (value)} is less than 0 (zero)."
                    );
                }

                if (value > 9)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof (value),
                        $"{nameof (value)} is greater than 9 (nine)."
                    );
                }

                this._Cells[row, column] = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid" /> class that is empty.
        /// </summary>
        public Grid() => this._Cells = new int[9, 9];

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid" /> class that contains values copied from the specified sudoku.
        /// </summary>
        /// <param name="sudoku">The sudoku to copy values from.</param>
        /// <exception cref="ArgumentNullException"><c><paramref name="sudoku" /></c> is <c>null</c>.</exception>
        public Grid(IReadOnlySudoku sudoku) : this()
        {
            if (sudoku is null)
            {
                throw new ArgumentNullException(
                    nameof (sudoku),
                    $"{nameof (sudoku)} is null."
                );
            }

            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    this._Cells[row, column] = sudoku[row, column];
                }
            }
        }
    }
}
