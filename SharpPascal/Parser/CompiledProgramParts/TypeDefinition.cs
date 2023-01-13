/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
{
    using System;
    
    
    public class TypeDefinition
    {
        public string Name { get; init; }
        public Type OutputType { get; init; }


        private TypeDefinition()
        {
        }


        public static TypeDefinition CreateIntegerTypeDefinition()
        {
            return new TypeDefinition()
            {
                Name = "INTEGER",
                OutputType = typeof(int)
            };
        }
    
    
        public static TypeDefinition CreateRealTypeDefinition()
        {
            return new TypeDefinition()
            {
                Name = "REAL",
                OutputType = typeof(double)
            };
        }
    
    
        public static TypeDefinition CreateCharTypeDefinition()
        {
            return new TypeDefinition()
            {
                Name = "CHAR",
                OutputType = typeof(char)
            };
        }
    
        
        public static TypeDefinition CreateStringTypeDefinition()
        {
            return new TypeDefinition()
            {
                Name = "STRING",
                OutputType = typeof(string)
            };
        }
        
    
        public static TypeDefinition CreateBooleanTypeDefinition()
        {
            return new TypeDefinition()
            {
                Name = "BOOLEAN",
                OutputType = typeof(bool)
            };
        }
    }
}