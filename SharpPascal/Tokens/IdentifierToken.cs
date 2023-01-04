/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    using System;
    
    
    public class IdentifierToken : AToken
    {
        public IdentifierToken(string s, int linePosition, int line)
            : base(linePosition, line)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException("An identifier name expected.");
            }

            Code = TokenCode.TOK_IDENTIFIER;
            StringValue = s;
        }


        public override string ToString()
        {
            return StringValue;
        }
    }
}
