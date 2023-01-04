/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class IntegerDivOperatorTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_KEY_DIV;
        protected override string ExpectedTokenStringRepresentation => "DIV";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new IntegerDivOperatorToken(linePosition, line);
    }
}