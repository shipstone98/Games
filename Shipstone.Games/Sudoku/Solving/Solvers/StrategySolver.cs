using System;
using System.Collections.Generic;

namespace Shipstone.Games.Sudoku.Solving.Solvers
{
    internal abstract class StrategySolver
    {
        private protected StrategicSolver _Solver;
        private readonly Strategy _Strategy;

        private protected StrategySolver(
            StrategicSolver solver,
            Strategy strategy
        )
        {
            this._Solver = solver;
            this._Strategy = strategy;
        }   

        internal bool Solve()
        {
            HashSet<MoveLocation> locations = new HashSet<MoveLocation>();

            if (!this.SolveMove(locations))
            {
                return false;
            }

            Move move = new Move(this._Solver, this._Strategy, locations);
            this._Solver._Moves.Add(move);
            return true;
        }

        private protected abstract bool SolveMove(ISet<MoveLocation> locations);

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
                case Strategy.PointingCandidate:
                    return new PointingCandidateSolver(solver);
                case Strategy.ClaimingCandidate:
                    return new ClaimingCandidateSolver(solver);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
