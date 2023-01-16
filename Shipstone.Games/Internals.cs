using System;

namespace Shipstone.Games.Sudoku
{
    internal static class Internals
    {
        internal static void CheckIndices(int row, int column)
        {
            if (row < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof (row),
                    $"{nameof (row)} is less than 0 (zero)."
                );
            }

            if (row > 8)
            {
                throw new ArgumentOutOfRangeException(
                    nameof (row),
                    $"{nameof (row)} is equal to or greater than 9 (nine)."
                );
            }

            if (column < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof (column),
                    $"{nameof (column)} is less than 0 (zero)."
                );
            }

            if (column > 8)
            {
                throw new ArgumentOutOfRangeException(
                    nameof (column),
                    $"{nameof (column)} is equal to or greater than 9 (nine)."
                );
            }
        }
    }
}
