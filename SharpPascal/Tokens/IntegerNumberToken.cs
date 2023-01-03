/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    using System.Globalization;


    public class IntegerNumberToken : AToken
    {
        public IntegerNumberToken(int v, int linePosition, int line)
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
