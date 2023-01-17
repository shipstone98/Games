namespace Shipstone.Games.Sudoku
{
    /// <summary>
    /// Represents the possible strategies that may be used by a <see cref="StrategicSolver" /> when solving a sudoku.
    /// </summary>
    public enum Strategy
    {
        /// <summary>
        /// Represents a full house. This occurs when a row, column, or block contains only a single empty cell.
        /// </summary>
        FullHouse = 1,

        /// <summary>
        /// Represents a naked single. This occurs when a cell contains only a single candidate.
        /// </summary>
        NakedSingle,

        /// <summary>
        /// Represents a hidden single. This occurs when, for a single candidate, a row, column, or block contains only a single cell with that candidate.
        /// </summary>
        HiddenSingle,

        /// <summary>
        /// Represents a pointing locked candidate. This occurs when, for a single block, a candidate appears in only either a single row or column.
        /// </summary>
        PointingCandidate
    }
}
