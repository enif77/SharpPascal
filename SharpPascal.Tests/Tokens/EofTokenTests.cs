/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class EofTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_EOF;
        protected override string ExpectedTokenStringRepresentation => "@EOF";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new EofToken(linePosition, line);
    }
}