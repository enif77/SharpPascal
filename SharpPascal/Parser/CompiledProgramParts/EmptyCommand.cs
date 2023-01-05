/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
{
    using System;


    /// <summary>
    /// An empty command.
    /// </summary>
    public class EmptyCommand : ICommand
    {
        /// <summary>
        /// The parent program block of this command.
        /// </summary>
        public IProgramBlock Parent { get; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentBlock">The parent program block of this command.</param>
        public EmptyCommand(IProgramBlock parentBlock)
        {
            if (parentBlock == null) throw new ArgumentNullException(nameof(parentBlock));

            Parent = parentBlock;
        }


        public string GenerateOutput()
        {
            return string.Empty;
        }
    }
}
