/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.CompiledProgramParts
{
    /// <summary>
    /// Represents a compiled command.
    /// </summary>
    public interface ICommand : ICompiledProgramPart
    {
        /// <summary>
        /// The parent program block of this command.
        /// </summary>
        IProgramBlock Parent { get; }
    }
}
