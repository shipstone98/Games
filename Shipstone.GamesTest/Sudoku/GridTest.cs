using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.Games.Sudoku;

namespace Shipstone.GamesTest.Sudoku
{
    [TestClass]
    public class GridTest
    {
        [TestMethod]
        public void TestIsComplete_Complete()
        {
            // Arrange
            Grid grid = Internals.CreateRandomGrid(out int[,] cells);

            // Act
            bool isComplete = grid.IsComplete;

            // Assert
            Assert.IsTrue(isComplete);
            grid.AssertEqual(cells);
        }

        [TestMethod]
        public void TestIsComplete_Empty()
        {
            // Arrange
            Grid grid = new();

            // Act
            bool isComplete = grid.IsComplete;

            // Assert
            Assert.IsFalse(isComplete);
            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestIsComplete_NotComplete()
        {
            // Arrange
            Grid grid = Internals.CreateRandomGrid(out int[,] cells);
            grid[0, 0] = 0;
            cells[0, 0] = 0;

            // Act
            bool isComplete = grid.IsComplete;

            // Assert
            Assert.IsFalse(isComplete);
            grid.AssertEqual(cells);
        }

#region Item property
#region Get accessor
        [TestMethod]
        public void TestItem_Get()
        {
            // Arrange
            Grid grid = Internals.CreateRandomGrid(out int[,] cells);

            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    // Act
                    int cell = grid[row, column];

                    // Assert
                    Assert.AreEqual(cells[row, column], cell);
                }
            }

            grid.AssertEqual(cells);
        }

        [TestMethod]
        public void TestItem_Get_ColumnGreaterThanOrEqualToNine()
        {
            // Arrange
            Grid grid = new();
            IEnumerable<int> columns = new int[] { 9, Int32.MaxValue };

            foreach (int column in columns)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        grid[0, column]);

                // Assert
                Assert.AreEqual("column", ex.ParamName);
            }

            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestItem_Get_ColumnLessThanZero()
        {
            // Arrange
            Grid grid = new();
            IEnumerable<int> columns = new int[] { Int32.MinValue, -1 };

            foreach (int column in columns)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        grid[0, column]);

                // Assert
                Assert.AreEqual("column", ex.ParamName);
            }

            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestItem_Get_RowGreaterThanOrEqualToNine()
        {
            // Arrange
            Grid grid = new();
            IEnumerable<int> rows = new int[] { 9, Int32.MaxValue };

            foreach (int row in rows)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        grid[row, 0]);

                // Assert
                Assert.AreEqual("row", ex.ParamName);
            }

            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestItem_Get_RowLessThanZero()
        {
            // Arrange
            Grid grid = new();
            IEnumerable<int> rows = new int[] { Int32.MinValue, -1 };

            foreach (int row in rows)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        grid[row, 0]);

                // Assert
                Assert.AreEqual("row", ex.ParamName);
            }

            grid.AssertEmpty();
        }
#endregion

#region Set accessor
        [TestMethod]
        public void TestItem_Set()
        {
            // Arrange
            Grid grid = new();

            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    for (int n = 9; n > -1; n --)
                    {
                        // Act
                        grid[row, column] = n;

                        // Assert
                        Assert.AreEqual(n, grid[row, column]);
                    }
                }
            }

            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestItem_Set_ColumnGreaterThanOrEqualToNine()
        {
            // Arrange
            Grid grid = new();
            IEnumerable<int> columns = new int[] { 9, Int32.MaxValue };

            foreach (int column in columns)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        grid[0, column] = 0);

                // Assert
                Assert.AreEqual("column", ex.ParamName);
            }

            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestItem_Set_ColumnLessThanZero()
        {
            // Arrange
            Grid grid = new();
            IEnumerable<int> columns = new int[] { Int32.MinValue, -1 };

            foreach (int column in columns)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        grid[0, column] = 0);

                // Assert
                Assert.AreEqual("column", ex.ParamName);
            }

            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestItem_Set_RowGreaterThanOrEqualToNine()
        {
            // Arrange
            Grid grid = new();
            IEnumerable<int> rows = new int[] { 9, Int32.MaxValue };

            foreach (int row in rows)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        grid[row, 0] = 0);

                // Assert
                Assert.AreEqual("row", ex.ParamName);
            }

            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestItem_Set_RowLessThanZero()
        {
            // Arrange
            Grid grid = new();
            IEnumerable<int> rows = new int[] { Int32.MinValue, -1 };

            foreach (int row in rows)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        grid[row, 0] = 0);

                // Assert
                Assert.AreEqual("row", ex.ParamName);
            }

            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestItem_Set_ValueGreaterThanNine()
        {
            // Arrange
            Grid grid = new();
            IEnumerable<int> values = new int[] { 10, Int32.MaxValue };

            foreach (int val in values)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        grid[0, 0] = val);

                // Assert
                Assert.AreEqual("value", ex.ParamName);
            }

            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestItem_Set_ValueLessThanZero()
        {
            // Arrange
            Grid grid = new();
            IEnumerable<int> values = new int[] { Int32.MinValue, -1 };

            foreach (int val in values)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        grid[0, 0] = val);

                // Assert
                Assert.AreEqual("value", ex.ParamName);
            }

            grid.AssertEmpty();
        }
#endregion
#endregion

#region Constructors
        [TestMethod]
        public void TestConstructor()
        {
            // Act
            Grid grid = new();

            // Assert
            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestConstructor_Sudoku_Empty()
        {
            // Arrange
            Grid original = new();

            // Act
            Grid copy = new(original);

            // Assert
            original.AssertEmpty();
            copy.AssertEmpty();
        }

        [TestMethod]
        public void TestConstructor_Sudoku_NotEmpty()
        {
            // Arrange
            Grid original = Internals.CreateRandomGrid(out int[,] cells);

            // Act
            Grid copy = new(original);

            // Assert
            original.AssertEqual(cells);
            copy.AssertEqual(cells);
        }

        [TestMethod]
        public void TestConstructor_Sudoku_Null()
        {
#pragma warning disable CS8604
            // Act
            ArgumentException ex =
                Assert.ThrowsException<ArgumentNullException>(() =>
                    new Grid(null as IReadOnlySudoku));
#pragma warning restore CS8604

            // Assert
            Assert.AreEqual("sudoku", ex.ParamName);
        }
#endregion
    }
}
