/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class RightBracketTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_RIGHT_BRACKET;
        protected override string ExpectedTokenStringRepresentation => "]";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new RightBracketToken(linePosition, line);
    }
}