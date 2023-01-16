using System;

namespace Shipstone.Games.Sudoku.Solvers
{
    internal abstract class StrategySolver
    {
        private protected StrategicSolver _Solver;

        private protected StrategySolver(StrategicSolver solver) =>
            this._Solver = solver;

        internal abstract bool Solve();

        internal static StrategySolver CreateInstance(StrategicSolver solver, Strategy strategy)
        {
            switch (strategy)
            {
                case Strategy.FullHouse:
                    return new FullHouseSolver(solver);
                case Strategy.NakedSingle:
                    return new NakedSingleSolver(solver);
                case Strategy.HiddenSingle:
                    return new HiddenSingleSolver(solver);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
