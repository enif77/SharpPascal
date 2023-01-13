/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
{
    using System;


    public class VariableDeclaration
    {
        public string Name { get; private init; }
        public TypeDefinition TypeDefinition { get; init; }


        private VariableDeclaration()
        { 
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
