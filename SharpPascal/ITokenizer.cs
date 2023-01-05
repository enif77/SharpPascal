/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal
{
    /// <summary>
    /// Makes tokens from a program source.
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        /// The last character extracted from the program source.
        /// </summary>
        int CurrentChar { get; }

        /// <summary>
        /// The last token extracted from the program source.
        /// </summary>
        IToken CurrentToken { get; }

        /// <summary>
        /// The current line position (1 .. N).
        /// </summary>
        int CurrentLinePosition { get; }

        /// <summary>
        /// The current line (1 .. N).
        /// </summary>
        int CurrentLine { get; }


        /// <summary>
        /// Extracts the next token found in the program source.
        /// </summary>
        IToken NextToken();
    }
}
