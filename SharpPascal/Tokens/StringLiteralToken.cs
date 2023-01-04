/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    using System;
    
    
    public class StringLiteralToken : AToken
    {
        public StringLiteralToken(string s, int linePosition, int line)
            : base(linePosition, line)
        {
            Code = TokenCode.TOK_STRING_LITERAL;
            StringValue = s ?? throw new ArgumentException("A string literal expected.");
        }


        public override string ToString()
        {
            return $"\"{StringValue}\"";
        }
    }
}
