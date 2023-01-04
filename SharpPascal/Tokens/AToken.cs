/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    using System;
    
    
    public abstract class AToken : IToken
    {
        public TokenCode Code { get; protected init; }
        public int IntegerValue { get; protected init; }
        public double RealValue { get; protected init; }
        public string StringValue { get; protected init; }
        public int Line { get; }
        public int LinePosition { get; }


        protected AToken(int linePosition, int line)
        {
            if (linePosition <= 0) throw new ArgumentOutOfRangeException(
                nameof(linePosition),
                "The linePosition parameter should be greater than 0.");
            
            if (line <= 0) throw new ArgumentOutOfRangeException(
                nameof(line),
                "The line parameter should be greater than 0.");

            LinePosition = linePosition;
            Line = line;
        }
    }
}
