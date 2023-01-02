/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// A begin-of-variables-declaration token.
    /// </summary>
    public sealed class BeginVariablesDeclarationToken : AToken
    {
        public BeginVariablesDeclarationToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_KEY_VAR;
        }


        public override string ToString() => "VAR";
    }
}
