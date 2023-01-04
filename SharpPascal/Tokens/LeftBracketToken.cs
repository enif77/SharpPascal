/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// An begin array index token.
    /// </summary>
    public sealed class LeftBracketToken : AToken
    {
        public LeftBracketToken(int linePosition, int line)
            : base(linePosition, line)
        {
            Code = TokenCode.TOK_LEFT_BRACKET;
        }


        public override string ToString() => "[";
    }
}
