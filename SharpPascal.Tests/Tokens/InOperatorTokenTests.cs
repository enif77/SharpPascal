/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class InOperatorTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_KEY_IN;
        protected override string ExpectedTokenStringRepresentation => "IN";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new InOperatorToken(linePosition, line);
    }
}