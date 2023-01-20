using System.Collections.Generic;
using System.Linq;

namespace Shipstone.Games.Sudoku.Solvers
{
    internal class ClaimingCandidateSolver : StrategySolver
    {
        internal ClaimingCandidateSolver(StrategicSolver solver)
            : base(solver, Strategy.ClaimingCandidate) { }

        private protected override bool SolveMove(ISet<MoveLocation> locations) =>
            this.SolveMoveRow(locations) || this.SolveMoveColumn(locations);

        private bool SolveMoveColumn(ISet<MoveLocation> locations)
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
                            locations,
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

            return false;
        }

        private bool SolveMoveColumn(
            ISet<MoveLocation> locations,
            int column,
            int n,
            IEnumerable<int> startRows
        )
        {
            int startColumn = column - column % 3;
            int startRow = startRows.First();
                
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

                    locations.Add(new MoveLocation(
                        currentRow,
                        currentColumn,
                        0,
                        n
                    ));
                }
            }
            
            return locations.Count > 0;
        }

        private bool SolveMoveRow(ISet<MoveLocation> locations)
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
                            locations,
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

            return false;
        }

        private bool SolveMoveRow(
            ISet<MoveLocation> locations,
            int row,
            int n,
            IEnumerable<int> startColumns
        )
        {
            int startColumn = startColumns.First();
            int startRow = row - row % 3;
                
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

                    locations.Add(new MoveLocation(
                        currentRow,
                        currentColumn,
                        0,
                        n
                    ));
                }
            }
            
            return locations.Count > 0;
        }
    }
}
