/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal
{
    /// <summary>
    /// A handler for errors.
    /// </summary>
    public interface IErrorHandler
    {
        /// <summary>
        /// Reports an general error.
        /// </summary>
        /// <param name="message">An error message.</param>
        /// <param name="args">Error message arguments.</param>
        void NotifyError(string message, params object[] args);

        /// <summary>
        /// Gets a general error description with parameters and returns it as a throwable exception.
        /// </summary>
        /// <param name="message">An error message.</param>
        /// <param name="args">Error message arguments.</param>
        /// <returns>A general error as a throwable exception.</returns>
        CompilerException Error(string message, params object[] args);
    }
}
