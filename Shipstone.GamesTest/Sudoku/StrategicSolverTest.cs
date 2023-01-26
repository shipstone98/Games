using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.Games.Sudoku;
using Shipstone.Games.Sudoku.Solving;

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
            Assert.AreEqual(0, solver.Moves.Count);
            solver.Sudoku.AssertEmpty();
        }

        [TestMethod]
        public void TestSolveSingle_FullHouse_ContainsBlock()
        {
            // Arrange
            const int ROW = 8;
            const int COLUMN = 5;
            const int VALUE = 6;
            const Strategy STRATEGY = Strategy.FullHouse;

            Grid grid = Internals.ParseGrid(
                "800739006370465000040182009000600040054300610060500000400853070000271064100940002",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);
            cells[ROW, COLUMN] = VALUE;

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            Move move = solver.Moves.Single();
            move.AssertEqual(1, solver, STRATEGY, 0);
            MoveLocation location = move.Locations.Single();
            location.AssertEqual(ROW, COLUMN, VALUE);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_FullHouse_ContainsColumn()
        {
            // Arrange
            const int ROW = 7;
            const int COLUMN = 4;
            const int VALUE = 1;
            const Strategy STRATEGY = Strategy.FullHouse;

            Grid grid = Internals.ParseGrid(
                "200060000083090000700821900006073000090682040000450100008935004000000290000040008",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);
            cells[ROW, COLUMN] = VALUE;

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            Move move = solver.Moves.Single();
            move.AssertEqual(1, solver, STRATEGY, 0);
            MoveLocation location = move.Locations.Single();
            location.AssertEqual(ROW, COLUMN, VALUE);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_FullHouse_ContainsRow()
        {
            // Arrange
            const int ROW = 4;
            const int COLUMN = 7;
            const int VALUE = 1;
            const Strategy STRATEGY = Strategy.FullHouse;

            Grid grid = Internals.ParseGrid(
                "207000000080090000030600800008064900692785304001320500009001020000040090000000408",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);
            cells[ROW, COLUMN] = VALUE;

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            Move move = solver.Moves.Single();
            move.AssertEqual(1, solver, STRATEGY, 0);
            MoveLocation location = move.Locations.Single();
            location.AssertEqual(ROW, COLUMN, VALUE);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_FullHouse_NotContains()
        {
            // Arrange
            Grid grid = Internals.ParseGrid(
                "412736589000000106568010370000850210100000008087090000030070865800000000000908401",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);

            // Act
            bool isSolved = solver.SolveSingle(Strategy.FullHouse);

            // Assert
            Assert.IsFalse(isSolved);
            Assert.AreEqual(0, solver.Moves.Count);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_NakedSingle_Contains()
        {
            // Arrange
            const int ROW = 5;
            const int COLUMN = 6;
            const int VALUE = 6;
            const Strategy STRATEGY = Strategy.NakedSingle;

            Grid grid = Internals.ParseGrid(
                "412736589000000106568010370000850210100000008087090000030070865800000000000908401",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);
            cells[ROW, COLUMN] = VALUE;

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            Move move = solver.Moves.Single();
            move.AssertEqual(1, solver, STRATEGY, 0);
            MoveLocation location = move.Locations.Single();
            location.AssertEqual(ROW, COLUMN, VALUE);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_NakedSingle_NotContains()
        {
            // Arrange
            Grid grid = Internals.ParseGrid(
                "984000000002500040001904002006097230003602000209035610195768423427351896638009751",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);

            // Act
            bool isSolved = solver.SolveSingle(Strategy.NakedSingle);

            // Assert
            Assert.IsFalse(isSolved);
            Assert.AreEqual(0, solver.Moves.Count);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_HiddenSingle_ContainsBlock()
        {
            // Arrange
            const int ROW = 0;
            const int COLUMN = 2;
            const int VALUE = 1;
            const Strategy STRATEGY = Strategy.HiddenSingle;

            Grid grid = Internals.ParseGrid(
                "000000403026009000005870000009032000000700000162000000010020560000900000050000107",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);
            cells[ROW, COLUMN] = VALUE;

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            Move move = solver.Moves.Single();
            move.AssertEqual(1, solver, STRATEGY, 0);
            MoveLocation location = move.Locations.Single();
            location.AssertEqual(ROW, COLUMN, VALUE);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_HiddenSingle_ContainsColumn()
        {
            // Arrange
            const int ROW = 3;
            const int COLUMN = 2;
            const int VALUE = 6;
            const Strategy STRATEGY = Strategy.HiddenSingle;

            Grid grid = Internals.ParseGrid(
                "000100200210300900860700000000270083082934760730006000008003017075000040001007000",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);
            cells[ROW, COLUMN] = VALUE;

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            Move move = solver.Moves.Single();
            move.AssertEqual(1, solver, STRATEGY, 0);
            MoveLocation location = move.Locations.Single();
            location.AssertEqual(ROW, COLUMN, VALUE);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_HiddenSingle_ContainsRow()
        {
            // Arrange
            const int ROW = 2;
            const int COLUMN = 3;
            const int VALUE = 6;
            const Strategy STRATEGY = Strategy.HiddenSingle;

            Grid grid = Internals.ParseGrid(
                "028007000016083070000020851137290000000730000000046307290070000000860140000300700",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);
            cells[ROW, COLUMN] = VALUE;

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            Move move = solver.Moves.Single();
            move.AssertEqual(1, solver, STRATEGY, 0);
            MoveLocation location = move.Locations.Single();
            location.AssertEqual(ROW, COLUMN, VALUE);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_HiddenSingle_NotContains()
        {
            // Arrange
            Grid grid = Internals.ParseGrid(
                "984000000002500040001904002006097230003602000209035610195768423427351896638009751",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);

            // Act
            bool isSolved = solver.SolveSingle(Strategy.HiddenSingle);

            // Assert
            Assert.IsFalse(isSolved);
            Assert.AreEqual(0, solver.Moves.Count);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_PointingCandidate_ContainsColumn()
        {
            // Arrange
            const Strategy STRATEGY = Strategy.PointingCandidate;
            const int VALUE = 1;

            ISet<Tuple<int, int>> indices = new HashSet<Tuple<int, int>>(6)
            {
                new(0, 6),
                new(1, 6),
                new(2, 6),
                new(6, 6),
                new(7, 6),
                new(8, 6)
            };

            Grid grid = Internals.ParseGrid(
                "300000000480090000002070000000030069003160002600042083090080007736050098000000005",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            solver.Sudoku.AssertEqual(cells);
            Move move = solver.Moves.Single();
            move.AssertEqual(6, solver, STRATEGY, 0);

            foreach (MoveLocation location in move.Locations)
            {
                Assert.AreEqual(0, location.AddedNumber);
                Tuple<int, int> index = new(location.Row, location.Column);
                Assert.IsTrue(indices.Remove(index));
                Assert.AreEqual(VALUE, location.RemovedCandidates.Single());
            }

            Assert.AreEqual(0, indices.Count);
        }

        [TestMethod]
        public void TestSolveSingle_PointingCandidate_ContainsRow()
        {
            // Arrange
            const Strategy STRATEGY = Strategy.PointingCandidate;
            const int VALUE = 1;

            ISet<Tuple<int, int>> indices = new HashSet<Tuple<int, int>>(6)
            {
                new(6, 0),
                new(6, 1),
                new(6, 2),
                new(6, 6),
                new(6, 7),
                new(6, 8)
            };

            Grid grid = Internals.ParseGrid(
                "340006070080000930002030060000010000097364850000002000000000000000608090000923785",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            solver.Sudoku.AssertEqual(cells);
            Move move = solver.Moves.Single();
            move.AssertEqual(6, solver, STRATEGY, 0);

            foreach (MoveLocation location in move.Locations)
            {
                Assert.AreEqual(0, location.AddedNumber);
                Tuple<int, int> index = new(location.Row, location.Column);
                Assert.IsTrue(indices.Remove(index));
                Assert.AreEqual(VALUE, location.RemovedCandidates.Single());
            }

            Assert.AreEqual(0, indices.Count);
        }

        [TestMethod]
        public void TestSolveSingle_PointingCandidate_NotContains()
        {
            // Arrange
            Grid grid = Internals.ParseGrid(
                "000060000000042736006730040094000068000096407607050923100000085060080271005010094",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);

            // Act
            bool isSolved = solver.SolveSingle(Strategy.PointingCandidate);

            // Assert
            Assert.IsFalse(isSolved);
            Assert.AreEqual(0, solver.Moves.Count);
            solver.Sudoku.AssertEqual(cells);
        }

        [TestMethod]
        public void TestSolveSingle_ClaimingCandidate_ContainsColumn()
        {
            // Arrange
            const Strategy STRATEGY = Strategy.ClaimingCandidate;
            const int VALUE = 4;

            ISet<Tuple<int, int>> indices = new HashSet<Tuple<int, int>>
            {
                new(0, 3),
                new(0, 4),
                new(1, 3),
                new(1, 4),
                new(2, 3),
                new(2, 4)
            };

            Grid grid = Internals.ParseGrid(
                "762008001980000006150000087478003169526009873319800425835001692297685314641932758",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            Assert.AreEqual(1, solver.Moves.Count);
            Move move = solver.Moves[0];
            move.AssertEqual(6, solver, STRATEGY, 0);
            solver.Sudoku.AssertEqual(cells);

            foreach (MoveLocation location in move.Locations)
            {
                Assert.AreEqual(0, location.AddedNumber);
                Assert.AreEqual(VALUE, location.RemovedCandidates.Single());

                Assert.IsTrue(indices.Remove(new(
                    location.Row,
                    location.Column
                )));
            }
        }

        [TestMethod]
        public void TestSolveSingle_ClaimingCandidate_ContainsRow()
        {
            // Arrange
            const Strategy STRATEGY = Strategy.ClaimingCandidate;
            const int VALUE = 4;

            ISet<Tuple<int, int>> indices = new HashSet<Tuple<int, int>>
            {
                new(3, 0),
                new(3, 1),
                new(3, 2),
                new(4, 0),
                new(4, 1),
                new(4, 2)
            };

            Grid grid = Internals.ParseGrid(
                "791453826685721394200869571000008069000000083800390152000184637008672915167935248",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);

            // Act
            bool isSolved = solver.SolveSingle(STRATEGY);

            // Assert
            Assert.IsTrue(isSolved);
            Assert.AreEqual(1, solver.Moves.Count);
            Move move = solver.Moves[0];
            move.AssertEqual(6, solver, STRATEGY, 0);
            solver.Sudoku.AssertEqual(cells);

            foreach (MoveLocation location in move.Locations)
            {
                Assert.AreEqual(0, location.AddedNumber);
                Assert.AreEqual(VALUE, location.RemovedCandidates.Single());

                Assert.IsTrue(indices.Remove(new(
                    location.Row,
                    location.Column
                )));
            }
        }

        [TestMethod]
        public void TestSolveSingle_ClaimingCandidate_NotContains()
        {
            // Arrange
            Grid grid = Internals.ParseGrid(
                "000060000000042736006730040094000068000096407607050923100000085060080271005010094",
                out int[,] cells
            );

            StrategicSolver solver = new(grid);

            // Act
            bool isSolved = solver.SolveSingle(Strategy.ClaimingCandidate);

            // Assert
            Assert.IsFalse(isSolved);
            Assert.AreEqual(0, solver.Moves.Count);
            solver.Sudoku.AssertEqual(cells);
        }
#endregion
    }
}
