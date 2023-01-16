using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.Games.Sudoku;

namespace Shipstone.GamesTest
{
    internal static class Internals
    {
        internal static readonly Random _Random;

        static Internals() => Internals._Random = new();

        internal static void AssertEmpty(this Grid grid)
        {
            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    Assert.AreEqual(0, grid[row, column]);
                }
            }
        }

        internal static void AssertEqual(this Grid grid, int[,] cells)
        {
            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    Assert.AreEqual(cells[row, column], grid[row, column]);
                }
            }
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
