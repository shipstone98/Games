using System.Collections.Generic;

namespace Shipstone.Games.Sudoku.Solving
{
    /// <summary>
    /// Represents a location in a <see cref="Move" />.
    /// </summary>
    public class MoveLocation
    {
        private readonly int _AddedNumber;
        private readonly int _Column;
        private readonly IReadOnlyCollection<int> _RemovedCandidates;
        private readonly int _Row;

        /// <summary>
        /// Gets the number that was added at the location.
        /// </summary>
        /// <value>The number that was added at the location, or 0 (zero) if candidates were removed instead.</value>
        public int AddedNumber => this._AddedNumber;

        /// <summary>
        /// Gets the zero-based column index of the location.
        /// </summary>
        /// <value>The zero-based column index of the location.</value>
        public int Column => this._Column;

        /// <summary>
        /// Gets a read-only collection containing candidates that were removed at the location.
        /// </summary>
        /// <value>A read-only collection containing candidates that were removed at the location. The collection is empty if a number was added instead.</value>
        public IReadOnlyCollection<int> RemovedCandidates =>
            this._RemovedCandidates;

        /// <summary>
        /// Gets the zero-based row index of the location.
        /// </summary>
        /// <value>The zero-based row index of the location.</value>
        public int Row => this._Row;

        internal MoveLocation(
            int row,
            int column,
            int addedNumber,
            params int[] removedCandidates
        )
        {
            this._AddedNumber = addedNumber;
            this._Column = column;
            this._RemovedCandidates = removedCandidates ?? new int[0];
            this._Row = row;
        }
    }
}
