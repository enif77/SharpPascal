/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal
{
    using System;


    /// <summary>
    /// Represents and error, that occurred during the execution of a program.
    /// </summary>
    public class CompilerException : Exception
    {
        public CompilerException() : base()
        {
        }

        public CompilerException(string message) : base(message)
        {
        }

        public CompilerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CompilerException(int line, int linePosition, string message)
            : base($"({line}, {linePosition}) {message}")
        {
        }

        public CompilerException(int line, int linePosition, string message, Exception innerException)
            : base(
                $"({line}, {linePosition}) {message}",
                  innerException)
        {
        }
    }
}
