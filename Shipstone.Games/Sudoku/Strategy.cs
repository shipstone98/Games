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
        FullHouse,

        /// <summary>
        /// Represents a naked single. This occurs when a cell contains only a single candidate.
        /// </summary>
        NakedSingle
    }
}
