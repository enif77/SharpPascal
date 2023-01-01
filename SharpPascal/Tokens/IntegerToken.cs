/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    using System.Globalization;


    public class IntegerToken : AToken
    {
        public IntegerToken(int v, int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_INTEGER_NUMBER;
            IntegerValue = v;
        }


        public override string ToString()
        {
            return IntegerValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}
