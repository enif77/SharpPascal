/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
{
    using System;


    /// <summary>
    /// The writeln command.
    /// </summary>
    public class WritelnCommand : ICommand
    {
        /// <summary>
        /// The parent program block of this command.
        /// </summary>
        public IProgramBlock Parent { get; }

        /// <summary>
        /// The optional string parameter.
        /// </summary>
        public string Parameter { get; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentBlock">The parent program block of this command.</param>
        public WritelnCommand(IProgramBlock parentBlock, string parameter = null)
        {
            if (parentBlock == null) throw new ArgumentNullException(nameof(parentBlock));

            Parent = parentBlock;
            Parameter = parameter;
        }


        public string GenerateOutput()
        {
            if (string.IsNullOrEmpty(Parameter))
            {
                return "_WriteLn();";    
            }

            return $"_WriteLn(\"{Parameter.Replace("\"", "\\\"")}\");";
        }
    }
}
