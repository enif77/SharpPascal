/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    using System.Globalization;


    public class RealToken : AToken
    {
        public RealToken(double v, int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_REAL_NUMBER;
            RealValue = v;
        }


        public override string ToString()
        {
            return RealValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}
