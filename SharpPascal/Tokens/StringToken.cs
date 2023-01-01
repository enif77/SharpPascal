/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    public class StringToken : AToken
    {
        public StringToken(string s, int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_STR;
            StringValue = s ?? string.Empty;
        }


        public override string ToString()
        {
            return $"\"{StringValue}\"";
        }
    }
}
