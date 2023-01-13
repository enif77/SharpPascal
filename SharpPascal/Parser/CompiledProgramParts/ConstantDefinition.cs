/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
{
    using System;


    public class ConstantDefinition
    {
        public string Name { get; private init; }
        public string TypeName { get; private init; }
        public string OutputTypeName { get; private init; }
        public int IntegerValue { get; protected init; }
        public double RealValue { get; protected init; }
        public string StringValue { get; protected init; }

        private ConstantDefinition()
        { 
        }


        public static ConstantDefinition CreateIntegerConstantDefinition(string name, int value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A constant name expected.");

            return new ConstantDefinition()
            {
                Name = name,
                TypeName = "integer",
                OutputTypeName = "int",
                IntegerValue = value
            };
        }


        public static ConstantDefinition CreateRealConstantDefinition(string name, double value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A constant name expected.");

            return new ConstantDefinition()
            {
                Name = name,
                TypeName = "real",
                OutputTypeName = "double",
                RealValue = value
            };
        }


        public static ConstantDefinition CreateStringConstantDefinition(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A constant name expected.");

            return new ConstantDefinition()
            {
                Name = name,
                TypeName = "string",
                OutputTypeName = "string",
                StringValue = value
            };
        }
    }
}
