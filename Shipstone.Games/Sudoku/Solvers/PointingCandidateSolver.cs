using System.Collections.Generic;
using System.Linq;

namespace Shipstone.Games.Sudoku.Solvers
{
    internal class PointingCandidateSolver : StrategySolver
    {
        internal PointingCandidateSolver(StrategicSolver solver)
            : base(solver, Strategy.PointingCandidate) { }

        private protected override bool SolveMove(out IReadOnlyCollection<MoveLocation> locations)
        {
            for (int startRow = 0; startRow < 9; startRow += 3)
            {
                for (int startColumn = 0; startColumn < 9; startColumn += 3)
                {
                    for (int n = 1; n < 10; n ++)
                    {
                        if (this.SolveMoveBlock(
                            out locations,
                            startRow,
                            startColumn,
                            n
                        ))
                        {
                            return true;
                        }
                    }
                }
            }

            locations = null;
            return false;
        }

        private bool SolveMoveBlock(
            out IReadOnlyCollection<MoveLocation> locations,
            int startRow,
            int startColumn,
            int n
        )
        {
            ISet<int> rows = new HashSet<int>();
            ISet<int> columns = new HashSet<int>();

            for (int rowOffset = 0; rowOffset < 3; rowOffset ++)
            {
                int row = startRow + rowOffset;

                for (int columnOffset = 0; columnOffset < 3; columnOffset ++)
                {
                    int column = startColumn + columnOffset;

                    ISet<int> candidates =
                        this._Solver._Candidates[row, column];

                    if (candidates is null || !candidates.Contains(n))
                    {
                        continue;
                    }

                    rows.Add(row);
                    columns.Add(column);
                }
            }

            if (rows.Count == 1)
            {
                int row = rows.First();
                columns.Clear();
                    
                for (int column = 0; column < 9; column ++)
                {
                    if (column == startColumn)
                    {
                        column += 2;
                        continue;
                    }

                    ISet<int> candidates =
                        this._Solver._Candidates[row, column];

                    if (candidates is null || !candidates.Contains(n))
                    {
                        continue;
                    }

                    columns.Add(column);
                }

                if (columns.Count > 0)
                {
                    HashSet<MoveLocation> locationSet =
                        new HashSet<MoveLocation>();

                    foreach (int column in columns)
                    {
                        locationSet.Add(new MoveLocation(row, column, 0, n));
                    }

                    locations = locationSet;
                    return true;
                }
            }

            if (columns.Count == 1)
            {
                int column = columns.First();
                rows.Clear();
                    
                for (int row = 0; row < 9; row ++)
                {
                    if (row == startRow)
                    {
                        row += 2;
                        continue;
                    }

                    ISet<int> candidates =
                        this._Solver._Candidates[row, column];

                    if (candidates is null || !candidates.Contains(n))
                    {
                        continue;
                    }

                    rows.Add(row);
                }

                if (rows.Count > 0)
                {
                    HashSet<MoveLocation> locationSet =
                        new HashSet<MoveLocation>();

                    foreach (int row in rows)
                    {
                        locationSet.Add(new MoveLocation(row, column, 0, n));
                    }

                    locations = locationSet;
                    return true;
                }
            }

            locations = null;
            return false;
        }
    }
}
