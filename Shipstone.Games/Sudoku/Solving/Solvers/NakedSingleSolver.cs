using System.Collections.Generic;
using System.Linq;

namespace Shipstone.Games.Sudoku.Solving.Solvers
{
    internal class NakedSingleSolver : StrategySolver
    {
        internal NakedSingleSolver(StrategicSolver solver)
            : base(solver, Strategy.NakedSingle) { }

        private protected override bool SolveMove(ISet<MoveLocation> locations)
        {
            for (int row = 0; row < 9; row ++)
            {
                for (int column = 0; column < 9; column ++)
                {
                    ISet<int> candidates =
                        this._Solver._Candidates[row, column];

                    if (candidates is null || candidates.Count > 1)
                    {
                        continue;
                    }

                    int n = candidates.First();
                    this._Solver._Grid._Cells[row, column] = n;
                    this._Solver._Candidates[row, column] = null;
                    locations.Add(new MoveLocation(row, column, n, null));
                    return true;
                }
            }

            return false;
        }
    }
}
