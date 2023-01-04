/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// An end array index token.
    /// </summary>
    public sealed class RightBracketToken : AToken
    {
        public RightBracketToken(int linePosition, int line)
            : base(linePosition, line)
        {
            Code = TokenCode.TOK_RIGHT_BRACKET;
        }


        public override string ToString() => "]";
    }
}
