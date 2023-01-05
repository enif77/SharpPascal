/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
{
    public class Expression : ICompiledProgramPart
    {
        public IProgramBlock ParentBlock { get; }

        public string SValue { get; }


        public Expression(IProgramBlock parentBlock, string sValue)
        {
            ParentBlock = parentBlock;
            SValue = sValue;
        }


        public string GenerateOutput()
        {
            return $"\"{SValue}\"";
        }
    }
}

/*
 
    "string"
 
 */