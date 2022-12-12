/* SharpPascal - (C) 2020 Premysl Fara 
 
SharpPascal is available under the zlib license:
This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.
Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:
1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
 
 */

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
