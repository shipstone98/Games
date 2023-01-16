using System.Collections.Generic;
using System.Linq;

namespace Shipstone.Games.Sudoku.Solvers
{
    internal class NakedSingleSolver : StrategySolver
    {
        internal NakedSingleSolver(StrategicSolver solver) : base(solver) { }

        internal override bool Solve()
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

                    this._Solver._Grid._Cells[row, column] =
                        candidates.First();

                    this._Solver._Candidates[row, column] = null;
                    return true;
                }
            }

            return false;
        }
    }
}
