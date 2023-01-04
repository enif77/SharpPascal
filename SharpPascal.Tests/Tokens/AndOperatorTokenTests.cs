/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class AndOperatorTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_KEY_AND;
        protected override string ExpectedTokenStringRepresentation => "AND";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new AndOperatorToken(linePosition, line);
    }
}