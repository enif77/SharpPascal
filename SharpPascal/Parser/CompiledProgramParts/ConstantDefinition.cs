/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
{
    using System;


    public class ConstantDefinition
    {
        public string Name { get; private init; }
        public TypeDefinition TypeDefinition { get; init; }
        public int IntegerValue { get; init; }
        public double RealValue { get; init; }
        public string StringValue { get; init; }

        
        private ConstantDefinition()
        { 
        }


        public static ConstantDefinition CreateIntegerConstantDefinition(string name, int value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A constant name expected.");

            return new ConstantDefinition()
            {
                Name = name,
                TypeDefinition = TypeDefinition.CreateIntegerTypeDefinition(),
                IntegerValue = value
            };
        }


        public static ConstantDefinition CreateRealConstantDefinition(string name, double value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A constant name expected.");

            return new ConstantDefinition()
            {
                Name = name,
                TypeDefinition = TypeDefinition.CreateRealTypeDefinition(),
                RealValue = value
            };
        }


        public static ConstantDefinition CreateStringConstantDefinition(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A constant name expected.");

            return new ConstantDefinition()
            {
                Name = name,
                TypeDefinition = TypeDefinition.CreateStringTypeDefinition(),
                StringValue = value
            };
        }
    }
}
