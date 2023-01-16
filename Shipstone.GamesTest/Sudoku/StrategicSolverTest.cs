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

#region SolveSingle method
        [TestMethod]
        public void TestSolveSingle_Invalid()
        {
            // Arrange
            Strategy[] strategies = Enum.GetValues<Strategy>();
            int min = (int) strategies[0] - 1;
            int max = (int) strategies[^1] + 1;
            Grid grid = new Grid();
            StrategicSolver solver = new(grid);

            const String MESSAGE =
                "strategy is not one of the Strategy values.";

            // Act
            Exception exMin = Assert.ThrowsException<ArgumentException>(() =>
                solver.SolveSingle((Strategy) min));

            Exception exMax = Assert.ThrowsException<ArgumentException>(() =>
                solver.SolveSingle((Strategy) max));

            // Assert
            Assert.AreEqual(MESSAGE, exMin.Message);
            Assert.AreEqual(MESSAGE, exMax.Message);
        }

        [TestMethod]
        public void TestSolveSingle_FullHouse_Block()
        {
            // Arrange
            const int ROW = 8;
            const int COLUMN = 5;
            const int VALUE = 6;

            Grid grid = Internals.ParseGrid(
                "800739006370465000040182009000600040054300610060500000400853070000271064100940002",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);
            cells[ROW, COLUMN] = VALUE;

            // Act
            bool isSolved = solver.SolveSingle(Strategy.FullHouse);

            // Assert
            Assert.IsTrue(isSolved);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_FullHouse_Column()
        {
            // Arrange
            const int ROW = 7;
            const int COLUMN = 4;
            const int VALUE = 1;

            Grid grid = Internals.ParseGrid(
                "200060000083090000700821900006073000090682040000450100008935004000000290000040008",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);
            cells[ROW, COLUMN] = VALUE;

            // Act
            bool isSolved = solver.SolveSingle(Strategy.FullHouse);

            // Assert
            Assert.IsTrue(isSolved);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_FullHouse_Row()
        {
            // Arrange
            const int ROW = 4;
            const int COLUMN = 7;
            const int VALUE = 1;

            Grid grid = Internals.ParseGrid(
                "207000000080090000030600800008064900692785304001320500009001020000040090000000408",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);
            cells[ROW, COLUMN] = VALUE;

            // Act
            bool isSolved = solver.SolveSingle(Strategy.FullHouse);

            // Assert
            Assert.IsTrue(isSolved);
            solver.Sudoku.AssertEqual(cells);
        }
#endregion
    }
}
