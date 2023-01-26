using System;
using System.Collections.Generic;
using System.Linq;

namespace Shipstone.Games.Sudoku.Solving.Solvers
{
    internal class HiddenSingleSolver : StrategySolver
    {
        internal HiddenSingleSolver(StrategicSolver solver)
            : base(solver, Strategy.HiddenSingle) { }

        private protected override bool SolveMove(ISet<MoveLocation> locations) =>
            this.SolveMoveRow(locations) ||
            this.SolveMoveColumn(locations) ||
            this.SolveMoveBlock(locations);

        private bool SolveMoveBlock(ISet<MoveLocation> locations)
        {
            for (int startRow = 0; startRow < 9; startRow += 3)
            {
                for (int startColumn = 0; startColumn < 9; startColumn += 3)
                {
                    for (int n = 1; n < 10; n ++)
                    {
                        ISet<Tuple<int, int>> indices =
                            new HashSet<Tuple<int, int>>();

                        for (int rowOffset = 0; rowOffset < 3; rowOffset ++)
                        {
                            int row = startRow + rowOffset;

                            for (
                                int columnOffset = 0;
                                columnOffset < 3;
                                columnOffset ++
                            )
                            {
                                int column = startColumn + columnOffset;

                                ISet<int> candidates =
                                    this._Solver._Candidates[row, column];

                                if (
                                    candidates is null ||
                                    !candidates.Contains(n)
                                )
                                {
                                    continue;
                                }

                                indices.Add(new Tuple<int, int>(row, column));
                            }
                        }

                        if (indices.Count == 1)
                        {
                            Tuple<int, int> index = indices.First();
                            int row = index.Item1;
                            int column = index.Item2;
                            this._Solver._Candidates[row, column] = null;
                            this._Solver._Grid[row, column] = n;

                            locations.Add(new MoveLocation(
                                row,
                                column,
                                n,
                                null
                            ));

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool SolveMoveColumn(ISet<MoveLocation> locations)
        {
            for (int column = 0; column < 9; column ++)
            {
                for (int n = 1; n < 10; n ++)
                {
                    ISet<int> rows = new HashSet<int>();

                    for (int row = 0; row < 9; row ++)
                    {
                        ISet<int> candidates =
                            this._Solver._Candidates[row, column];

                        if (candidates is null || !candidates.Contains(n))
                        {
                            continue;
                        }

                        rows.Add(row);
                    }

                    if (rows.Count == 1)
                    {
                        int row = rows.First();
                        this._Solver._Candidates[row, column] = null;
                        this._Solver._Grid[row, column] = n;
                        locations.Add(new MoveLocation(row, column, n, null));
                        return true;
                    }
                }
            }

            return false;
        }

        private bool SolveMoveRow(ISet<MoveLocation> locations)
        {
            for (int row = 0; row < 9; row ++)
            {
                for (int n = 1; n < 10; n ++)
                {
                    ISet<int> columns = new HashSet<int>();

                    for (int column = 0; column < 9; column ++)
                    {
                        ISet<int> candidates =
                            this._Solver._Candidates[row, column];

                        if (candidates is null || !candidates.Contains(n))
                        {
                            continue;
                        }

                        columns.Add(column);
                    }

                    if (columns.Count == 1)
                    {
                        int column = columns.First();
                        this._Solver._Candidates[row, column] = null;
                        this._Solver._Grid[row, column] = n;
                        locations.Add(new MoveLocation(row, column, n, null));
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
