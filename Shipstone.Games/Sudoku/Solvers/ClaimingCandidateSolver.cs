using System.Collections.Generic;
using System.Linq;

namespace Shipstone.Games.Sudoku.Solvers
{
    internal class ClaimingCandidateSolver : StrategySolver
    {
        internal ClaimingCandidateSolver(StrategicSolver solver)
            : base(solver, Strategy.ClaimingCandidate) { }

        private protected override bool SolveMove(out IReadOnlyCollection<MoveLocation> locations) =>
            this.SolveMoveRow(out locations) ||
            this.SolveMoveColumn(out locations);

        private bool SolveMoveColumn(out IReadOnlyCollection<MoveLocation> locations)
        {
            for (int column = 0; column < 9; column ++)
            {
                for (int n = 1; n < 10; n ++)
                {
                    ISet<int> startRows = new HashSet<int>();

                    for (int row = 0; row < 9; row ++)
                    {
                        ISet<int> candidates =
                            this._Solver._Candidates[row, column];

                        if (candidates is null || !candidates.Contains(n))
                        {
                            continue;
                        }

                        startRows.Add(row - row % 3);
                    }

                    if (startRows.Count == 1)
                    {
                        if (this.SolveMoveColumn(
                            out locations,
                            column,
                            n,
                            startRows
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

        private bool SolveMoveColumn(
            out IReadOnlyCollection<MoveLocation> locations,
            int column,
            int n,
            IEnumerable<int> startRows
        )
        {
            int startColumn = column - column % 3;
            int startRow = startRows.First();
            HashSet<MoveLocation> locationSet = new HashSet<MoveLocation>();
                
            for (int rowOffset = 0; rowOffset < 3; rowOffset ++)
            {
                int currentRow = startRow + rowOffset;

                for (int columnOffset = 0; columnOffset < 3; columnOffset ++)
                {
                    int currentColumn = startColumn + columnOffset;

                    if (currentColumn == column)
                    {
                        continue;
                    }

                    ISet<int> candidates =
                        this._Solver._Candidates[currentRow, currentColumn];

                    if (candidates is null || !candidates.Contains(n))
                    {
                        continue;
                    }

                    locationSet.Add(new MoveLocation(
                        currentRow,
                        currentColumn,
                        0,
                        n
                    ));
                }
            }

            if (locationSet.Count > 0)
            {
                locations = locationSet;
                return true;
            }

            locations = null;
            return false;
        }

        private bool SolveMoveRow(out IReadOnlyCollection<MoveLocation> locations)
        {
            for (int row = 0; row < 9; row ++)
            {
                for (int n = 1; n < 10; n ++)
                {
                    ISet<int> startColumns = new HashSet<int>();

                    for (int column = 0; column < 9; column ++)
                    {
                        ISet<int> candidates =
                            this._Solver._Candidates[row, column];

                        if (candidates is null || !candidates.Contains(n))
                        {
                            continue;
                        }

                        startColumns.Add(column - column % 3);
                    }

                    if (startColumns.Count == 1)
                    {
                        if (this.SolveMoveRow(
                            out locations,
                            row,
                            n,
                            startColumns
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

        private bool SolveMoveRow(
            out IReadOnlyCollection<MoveLocation> locations,
            int row,
            int n,
            IEnumerable<int> startColumns
        )
        {
            int startColumn = startColumns.First();
            int startRow = row - row % 3;
            HashSet<MoveLocation> locationSet = new HashSet<MoveLocation>();
                
            for (int rowOffset = 0; rowOffset < 3; rowOffset ++)
            {
                int currentRow = startRow + rowOffset;

                if (currentRow == row)
                {
                    continue;
                }

                for (int columnOffset = 0; columnOffset < 3; columnOffset ++)
                {
                    int currentColumn = startColumn + columnOffset;

                    ISet<int> candidates =
                        this._Solver._Candidates[currentRow, currentColumn];

                    if (candidates is null || !candidates.Contains(n))
                    {
                        continue;
                    }

                    locationSet.Add(new MoveLocation(
                        currentRow,
                        currentColumn,
                        0,
                        n
                    ));
                }
            }

            if (locationSet.Count > 0)
            {
                locations = locationSet;
                return true;
            }

            locations = null;
            return false;
        }
    }
}
