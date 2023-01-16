using System;
using System.Collections.Generic;
using System.Linq;

namespace Shipstone.Games.Sudoku.Solvers
{
    internal class FullHouseSolver : StrategySolver
    {
        internal FullHouseSolver(StrategicSolver solver) : base(solver) { }

        internal override bool Solve() =>
            this.SolveRow() || this.SolveColumn() || this.SolveBlock();

        private bool SolveBlock()
        {
            for (int startRow = 0; startRow < 9; startRow += 3)
            {
                for (int startColumn = 0; startColumn < 9; startColumn += 3)
                {
                    ISet<Tuple<int, int>> emptyIndices =
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

                            if (this._Solver._Grid._Cells[row, column] == 0)
                            {
                                emptyIndices.Add(new Tuple<int, int>(
                                    row,
                                    column
                                ));
                            }
                        }
                    }

                    if (emptyIndices.Count == 1)
                    {
                        Tuple<int, int> index = emptyIndices.First();

                        int n =
                            this._Solver._Candidates[index.Item1, index.Item2].First();

                        this._Solver._Candidates[index.Item1, index.Item2] =
                            null;

                        this._Solver._Grid[index.Item1, index.Item2] = n;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool SolveColumn()
        {
            for (int column = 0; column < 9; column ++)
            {
                ISet<int> emptyRows = new HashSet<int>();

                for (int row = 0; row < 9; row ++)
                {
                    if (this._Solver._Grid._Cells[row, column] == 0)
                    {
                        emptyRows.Add(row);
                    }
                }

                if (emptyRows.Count == 1)
                {
                    int row = emptyRows.First();
                    int n = this._Solver._Candidates[row, column].First();
                    this._Solver._Candidates[row, column] = null;
                    this._Solver._Grid[row, column] = n;
                    return true;
                }
            }

            return false;
        }

        private bool SolveRow()
        {
            for (int row = 0; row < 9; row ++)
            {
                ISet<int> emptyColumns = new HashSet<int>();

                for (int column = 0; column < 9; column ++)
                {
                    if (this._Solver._Grid._Cells[row, column] == 0)
                    {
                        emptyColumns.Add(column);
                    }
                }

                if (emptyColumns.Count == 1)
                {
                    int column = emptyColumns.First();
                    int n = this._Solver._Candidates[row, column].First();
                    this._Solver._Candidates[row, column] = null;
                    this._Solver._Grid[row, column] = n;
                    return true;
                }
            }

            return false;
        }
    }
}
