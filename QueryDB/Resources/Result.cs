using System;

namespace QueryDB.Resources
{
    /// <summary>
    /// Represents the outcome of an operation, indicating success or failure.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Success { get; internal set; }

        /// <summary>
        /// Gets or sets the exception that occurred during the operation, if any.
        /// </summary>
        public Exception Exception { get; internal set; }
    }
}
