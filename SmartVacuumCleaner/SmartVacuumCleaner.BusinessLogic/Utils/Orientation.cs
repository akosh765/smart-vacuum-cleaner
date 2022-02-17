namespace SmartVacuumCleaner.BusinessLogic.Interfaces
{
    /// <summary>
    /// Enum to store the current orientation of the vacuum cleaner.
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// The robot faces upwards. Integer value: 0
        /// </summary>
        Upward,

        /// <summary>
        /// The robot faces right. Integer value: 1
        /// </summary>
        Right,

        /// <summary>
        /// The robot faces downward. Integer value: 2
        /// </summary>
        Downward,

        /// <summary>
        /// The robot faces left. Integer value: 3
        /// </summary>
        Left,
    }
}