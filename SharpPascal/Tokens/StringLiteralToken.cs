/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    using System;
    
    
    public class StringLiteralToken : AToken
    {
        public StringLiteralToken(string s, int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_STR;
            StringValue = s ?? throw new ArgumentException("A string literal expected.");
        }


        public override string ToString()
        {
            return $"\"{StringValue}\"";
        }
    }
}
