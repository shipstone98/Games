using System;

namespace Shipstone.Games.Sudoku
{
    internal static class Internals
    {
        internal static bool BlockContains(
            this IReadOnlySudoku sudoku,
            int row,
            int column,
            int val
        )
        {
            int startRow = row - row % 3;
            int startColumn = column - column % 3;

            for (int rowOffset = 0; rowOffset < 3; rowOffset ++)
            {
                row = startRow + rowOffset;

                for (int columnOffset = 0; columnOffset < 3; columnOffset ++)
                {
                    column = startColumn + columnOffset;

                    if (sudoku[row, column] == val)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

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

        internal static bool ColumnContains(
            this IReadOnlySudoku sudoku,
            int column,
            int val
        )
        {
            for (int row = 0; row < 9; row ++)
            {
                if (sudoku[row, column] == val)
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool RowContains(
            this IReadOnlySudoku sudoku,
            int row,
            int val
        )
        {
            for (int column = 0; column < 9; column ++)
            {
                if (sudoku[row, column] == val)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
