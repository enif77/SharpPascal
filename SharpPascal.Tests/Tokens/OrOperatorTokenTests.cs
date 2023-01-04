/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class OrOperatorTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_KEY_OR;
        protected override string ExpectedTokenStringRepresentation => "OR";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new OrOperatorToken(linePosition, line);
    }
}