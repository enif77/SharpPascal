/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
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
