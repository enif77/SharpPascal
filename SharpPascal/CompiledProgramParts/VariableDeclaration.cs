/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.CompiledProgramParts
{
    using System;
    using System.Text;


    public class VariableDeclaration : ICompiledProgramPart
    {
        public string Name { get; private init; }
        public TypeDefinition TypeDefinition { get; init; }


        private VariableDeclaration()
        { 
        }


        public string GenerateOutput()
        {
            var sb = new StringBuilder();

                sb.Append(TypeDefinition.OutputType.Name);
                sb.Append(" ");
                sb.Append(Name);
                sb.AppendLine(";");

            return sb.ToString();
        }


        public static VariableDeclaration CreateIntegerVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeDefinition = TypeDefinition.CreateIntegerTypeDefinition()
            };
        }


        public static VariableDeclaration CreateRealVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeDefinition = TypeDefinition.CreateRealTypeDefinition()
            };
        }


        public static VariableDeclaration CreateCharVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeDefinition = TypeDefinition.CreateCharTypeDefinition()
            };
        }


        public static VariableDeclaration CreateStringVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeDefinition = TypeDefinition.CreateStringTypeDefinition()
            };
        }
        
        public static VariableDeclaration CreateBooleanVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeDefinition = TypeDefinition.CreateBooleanTypeDefinition()
            };
        }
    }
}
