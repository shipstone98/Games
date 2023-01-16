using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.Games.Sudoku;

namespace Shipstone.GamesTest
{
    internal static class Internals
    {
        internal static readonly Random _Random;

        static Internals() => Internals._Random = new();

        internal static void AssertEmpty(this IReadOnlySudoku sudoku)
        {
            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    Assert.AreEqual(0, sudoku[row, column]);
                }
            }
        }

        internal static void AssertEqual(
            this IReadOnlySudoku sudoku,
            int[,] cells
        )
        {
            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    Assert.AreEqual(cells[row, column], sudoku[row, column]);
                }
            }
        }

        internal static void AssertValid(this IReadOnlySudoku sudoku)
        {
            Func<ISet<int>> createCandidates = () =>
            {
                ISet<int> candidates = new HashSet<int>(9);

                for (int n = 1; n < 10; n ++)
                {
                    candidates.Add(n);
                }

                return candidates;
            };

            for (int row = 0; row < 9; row ++)
            {
                ISet<int> candidates = createCandidates();

                for (int column = 0; column < 9; column ++)
                {
                    Assert.IsTrue(candidates.Remove(sudoku[row, column]));
                }
            }

            for (int column = 0; column < 9; column ++)
            {
                ISet<int> candidates = createCandidates();

                for (int row = 0; row < 9; row ++)
                {
                    Assert.IsTrue(candidates.Remove(sudoku[row, column]));
                }
            }

            for (int startRow = 0; startRow < 9; startRow += 3)
            {
                for (int startColumn = 0; startColumn < 9; startColumn += 3)
                {
                    ISet<int> candidates = createCandidates();

                    for (int rowOffset = 0; rowOffset < 3; rowOffset ++)
                    {
                        int row = startRow + rowOffset;

                        for (int columnOffset = 0; columnOffset < 3; columnOffset ++)
                        {
                            int column = startColumn + columnOffset;
                            Assert.IsTrue(candidates.Remove(sudoku[row, column]));
                        }
                    }
                }
            }
        }

        internal static int[,] CreateCells(this IReadOnlySudoku sudoku)
        {
            int[,] cells = new int[9, 9];

            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    cells[row, column] = sudoku[row, column];
                }
            }

            return cells;
        }

        internal static Grid CreateRandomGrid(out int[,] cells)
        {
            cells = new int[9, 9];
            Grid grid = new();

            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    int n = Internals._Random.Next(1, 10);
                    cells[row, column] = n;
                    grid[row, column] = n;
                }
            }

            return grid;
        }
    }
}
