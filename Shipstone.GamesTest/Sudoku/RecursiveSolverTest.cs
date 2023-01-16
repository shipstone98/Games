using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.Games.Sudoku;

namespace Shipstone.GamesTest.Sudoku
{
    [TestClass]
    public class RecursiveSolverTest
    {
        [TestMethod]
        public void TestConstructor_Sudoku_Empty()
        {
            // Arrange
            Grid grid = new();

            // Act
            RecursiveSolver solver = new(grid);

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
            RecursiveSolver solver = new(grid);

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
                    new RecursiveSolver(null as IReadOnlySudoku));
#pragma warning restore CS8604

            // Assert
            Assert.AreEqual("sudoku", ex.ParamName);
        }

        [TestMethod]
        public void TestSolve_Complete()
        {
            // Arrange
            Grid grid = new();
            RecursiveSolver solver = new(grid);
            solver.Solve();
            solver = new(solver.Sudoku);

            // Act
            bool isSolved = solver.Solve();

            // Assert
            Assert.IsTrue(isSolved);
            solver.Sudoku.AssertValid();
            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestSolve_Empty()
        {
            // Arrange
            Grid grid = new();
            RecursiveSolver solver = new(grid);

            // Act
            bool isSolved = solver.Solve();

            // Assert
            Assert.IsTrue(isSolved);
            solver.Sudoku.AssertValid();
            grid.AssertEmpty();
        }

        [TestMethod]
        public void TestSolve_NotSolvable()
        {
            // Arrange
            Grid grid = new();
            RecursiveSolver solver = new(grid);
            solver.Solve();
            grid = new(solver.Sudoku);
            grid[0, 0] = grid[0, 1];
            grid[0, 1] = 0;
            int[,] cells = grid.CreateCells();
            solver = new(grid);

            // Act
            bool isSolved = solver.Solve();

            // Assert
            Assert.IsFalse(isSolved);
            solver.Sudoku.AssertEqual(cells);
            grid.AssertEqual(cells);
        }
    }
}
