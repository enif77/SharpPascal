/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.CompiledProgramParts
{
    using System.Collections.Generic;
    
    
    public interface IProgramBlock : ICompiledProgramPart
    {
        /// <summary>
        /// The parent compiled part of this program block.
        /// </summary>
        IProgramBlock Parent { get; }

        IEnumerable<ICompiledProgramPart> Children { get; }

        /// <summary>
        /// Checks, if a variable is declared in this program block.
        /// </summary>
        /// <param name="name">A variable name.</param>
        /// <returns>True, if a variable is declared in this program block.</returns>
        bool IsVariableDeclared(string name);

        /// <summary>
        /// Adds an new variable declaration to this block.
        /// </summary>
        /// <param name="name">A variable name.</param>
        /// <param name="typeName">A name of this variable type.</param>
        void AddVariableDeclaration(string name, string typeName);

        /// <summary>
        /// Adds a compiled program part to this block.
        /// </summary>
        /// <param name="programPart">A program part to be added.</param>
        void AddCompiledProgramPart(ICompiledProgramPart programPart);
    }
}
