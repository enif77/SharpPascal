/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
{
    using System;


    public class VariableDeclaration
    {
        public string Name { get; private set; }
        public string TypeName { get; private set; }
        public string OutputTypeName { get; private set; }


        private VariableDeclaration()
        { 
        }


        public static VariableDeclaration CreateIntegerVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeName = "integer",
                OutputTypeName = "int"
            };
        }


        public static VariableDeclaration CreateRealVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeName = "real",
                OutputTypeName = "double"
            };
        }


        public static VariableDeclaration CreateCharVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeName = "char",
                OutputTypeName = "char"
            };
        }


        public static VariableDeclaration CreateBooleanVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeName = "boolean",
                OutputTypeName = "bool"
            };
        }


        public static VariableDeclaration CreateStringVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeName = "string",
                OutputTypeName = "string"
            };
        }
    }
}
