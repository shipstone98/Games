using System.Collections.Generic;

namespace Shipstone.Games.Sudoku
{
    /// <summary>
    /// Represents a move made by a <see cref="StrategicSolver" />.
    /// </summary>
    public class Move
    {
        private readonly int _Index;
        private readonly IReadOnlyCollection<MoveLocation> _Locations;
        private readonly StrategicSolver _Solver;
        private readonly Strategy _Strategy;

        /// <summary>
        /// Gets the zero-based index in the list of moves in <see cref="Move.Solver" /> that the <see cref="Move" /> is located at.
        /// </summary>
        /// <value>The zero-based index in the list of moves in <see cref="Move.Solver" /> that the <see cref="Move" /> is located at.</value>
        public int Index => this._Index;

        /// <summary>
        /// Gets a read-only collection containing locations affected by the <see cref="Move" />.
        /// </summary>
        /// <value>A read-only collection containing locations affected by the <see cref="Move" />.</value>
        public IReadOnlyCollection<MoveLocation> Locations => this._Locations;

        /// <summary>
        /// Gets the <see cref="StrategicSolver" /> that made the <see cref="Move" />.
        /// </summary>
        /// <value>A reference to the <see cref="StrategicSolver" /> that made the <see cref="Move" />.</value>
        public StrategicSolver Solver => this._Solver;

        /// <summary>
        /// Gets the <see cref="Shipstone.Games.Sudoku.Strategy" /> that was used by <see cref="Move.Solver" /> to make the <see cref="Move" />.
        /// </summary>
        /// <value>The <see cref="Shipstone.Games.Sudoku.Strategy" /> that was used by <see cref="Move.Solver" /> to make the <see cref="Move" />.</value>
        public Strategy Strategy => this._Strategy;

        internal Move(
            StrategicSolver solver,
            Strategy strategy,
            IReadOnlyCollection<MoveLocation> locations
        )
        {
            this._Index = solver._Moves.Count;
            this._Locations = locations;
            this._Solver = solver;
            this._Strategy = strategy;
        }
    }
}
