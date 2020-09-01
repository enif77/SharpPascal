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
    using System;
    using System.Collections.Generic;


    public abstract class AProgramBlockBase : IProgramBlock
    {
        public IProgramBlock Parent { get; protected set; }
        public Dictionary<string, VariableDeclaration> VariableDeclarations { get; }
        public IEnumerable<ICompiledProgramPart> Children { get; protected set; }


        protected AProgramBlockBase(IProgramBlock parentBlock)
        {
            Parent = parentBlock;
            VariableDeclarations = new Dictionary<string, VariableDeclaration>();
            Children = new List<ICompiledProgramPart>();
        }


        public void AddVariableDeclaration(string name, string typeName)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");
            if (string.IsNullOrEmpty(typeName)) throw new ArgumentException($"A type name for the '{name}' variable expected.");

            if (VariableDeclarations.ContainsKey(name))
            {
                throw new CompilerException($"The '{name}' variable is already declared as the {VariableDeclarations[name].TypeName} type.");
            }

            switch (typeName)
            {
                case "INTEGER": VariableDeclarations.Add(name, VariableDeclaration.CreateIntegerVariableDeclaration(name)); break;
                case "REAL": VariableDeclarations.Add(name, VariableDeclaration.CreateRealVariableDeclaration(name)); break;
                case "CHAR": VariableDeclarations.Add(name, VariableDeclaration.CreateCharVariableDeclaration(name)); break;
                case "BOOLEAN": VariableDeclarations.Add(name, VariableDeclaration.CreateBooleanVariableDeclaration(name)); break;
                case "STRING": VariableDeclarations.Add(name, VariableDeclaration.CreateStringVariableDeclaration(name)); break;

                default:
                    throw new CompilerException($"Unknown type '{typeName}' used for the '{name}' variable declaration.");
            }
        }


        public bool IsVariableDeclared(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return VariableDeclarations.ContainsKey(name);
        }


        public virtual void AddCompiledProgramPart(ICompiledProgramPart programPart)
        {
            if (programPart == null) throw new ArgumentNullException(nameof(programPart));

            ((IList<ICompiledProgramPart>)Children).Add(programPart);
        }


        public abstract string GenerateOutput();
    }
}
