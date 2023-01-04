/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class ModOperatorTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_KEY_MOD;
        protected override string ExpectedTokenStringRepresentation => "MOD";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new ModOperatorToken(linePosition, line);
    }
}