/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    public class SimpleToken : AToken
    {
        public SimpleToken(TokenCode tokenCode, int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = tokenCode;
        }
    }
}
