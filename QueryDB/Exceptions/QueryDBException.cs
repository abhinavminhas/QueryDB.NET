﻿using System;

namespace QueryDB.Exceptions
{
    /// <summary>
    /// Represents exceptions specific to QueryDB operations.
    /// </summary>
    public class QueryDBException : Exception
    {
        /// <summary>
        /// Gets the type of the error that occurred.
        /// </summary>
        public string ErrorType { get; }

        /// <summary>
        /// Gets additional information about the error, if available.
        /// </summary>
        public string AdditionalInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryDBException"/> class.
        /// </summary>
        /// <param name="message">The error message that describes the error.</param>
        /// <param name="errorType">The type of the error.</param>
        /// <param name="additionalInfo">Optional additional information about the error.</param>
        public QueryDBException(string message, string errorType, string additionalInfo = null) : base(message)
        {
            ErrorType = errorType;
            AdditionalInfo = additionalInfo;
        }

        /// <summary>
        /// Returns a string representation of the exception, including error type and additional information.
        /// </summary>
        /// <returns>A string containing the error type, message, and additional information.</returns>
        public override string ToString()
        {
            string info = string.IsNullOrEmpty(AdditionalInfo) ? "" : $", Info: {AdditionalInfo}";
            return $"Type: {ErrorType}, Message: {Message}{info}";
        }
    }

    /// <summary>
    /// Provides predefined error types, messages, and additional information for QueryDB exceptions.
    /// </summary>
    internal static class QueryDBExceptions
    {
        /// <summary>
        /// Defines standard error types for QueryDB exceptions.
        /// </summary>
        internal static class ErrorType
        {
            /// <summary>
            /// Error type indicating an unsupported command.
            /// </summary>
            internal static readonly string UnsupportedCommand = "UnsupportedCommand";
        }

        /// <summary>
        /// Defines standard error messages for QueryDB exceptions.
        /// </summary>
        internal static class ErrorMessage
        {
            /// <summary>
            /// Error message indicating that SELECT queries are only supported in 'FetchData'.
            /// </summary>
            internal static readonly string UnsupportedFetchDataCommand = "Only SELECT queries are supported here.";

            /// <summary>
            /// Error message indicating that SELECT queries are only supported in 'ExecuteScalar'.
            /// </summary>
            internal static readonly string UnsupportedExecuteScalarCommand = "Only SELECT queries are supported here.";

            /// <summary>
            /// Error message indicating that SELECT queries are not supported in 'ExecuteCommand'.
            /// </summary>
            internal static readonly string UnsupportedSelectExecuteCommand = "SELECT queries are not supported here.";

            /// <summary>
            /// Error message indicating that SELECT queries are not supported in 'ExecuteTransaction'.
            /// </summary>
            internal static readonly string UnsupportedSelectExecuteTransaction = "SELECT queries are not supported here.";
        }

        /// <summary>
        /// Defines additional information related to QueryDB exceptions.
        /// </summary>
        internal static class AdditionalInfo
        {
            /// <summary>
            /// Error message indicating that SELECT queries are only supported in 'FetchData'.
            /// </summary>
            internal static readonly string UnsupportedFetchDataCommand = "'FetchData' only supports SELECT queries that return result set.";

            /// <summary>
            /// Error message indicating that SELECT queries are only supported in 'ExecuteScalar'.
            /// </summary>
            internal static readonly string UnsupportedExecuteScalarCommand = "'ExecuteScalar' only supports SELECT queries that have a scalar (single value) return.";

            /// <summary>
            /// Additional information about unsupported SELECT queries in 'ExecuteCommand'.
            /// </summary>
            internal static readonly string UnsupportedSelectExecuteCommand = "'ExecuteCommand' doesn't support SELECT queries.";

            /// <summary>
            /// Additional information about unsupported SELECT queries in 'ExecuteTransaction'.
            /// </summary>
            internal static readonly string UnsupportedSelectExecuteTransaction = "'ExecuteTransaction' doesn't support SELECT queries.";
        }
    }
}
