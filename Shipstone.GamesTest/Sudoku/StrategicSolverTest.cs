using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.Games.Sudoku;

namespace Shipstone.GamesTest.Sudoku
{
    [TestClass]
    public class StrategicSolverTest
    {
        [TestMethod]
        public void TestConstructor_Sudoku_Empty()
        {
            // Arrange
            Grid grid = new();

            // Act
            StrategicSolver solver = new(grid);

            // Assert
            solver.Sudoku.AssertEmpty();
            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestConstructor_Sudoku_NotEmpty()
        {
            // Arrange
            Grid grid = Internals.CreateRandomGrid(out int[,] cells);

            // Act
            StrategicSolver solver = new(grid);

            // Assert
            solver.Sudoku.AssertEqual(cells);
            grid.AssertEqual(cells);
        }

        [TestMethod]
        public void TestConstructor_Sudoku_Null()
        {
#pragma warning disable CS8604
            // Act
            ArgumentException ex =
                Assert.ThrowsException<ArgumentNullException>(() =>
                    new StrategicSolver(null as IReadOnlySudoku));
#pragma warning restore CS8604

            // Assert
            Assert.AreEqual("sudoku", ex.ParamName);
        }
    }
}
