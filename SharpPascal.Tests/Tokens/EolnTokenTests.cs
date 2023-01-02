/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class EolnTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_EOLN;
        protected override string ExpectedTokenStringRepresentation => "@EOLN";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new EolnToken(linePosition, line);
    }
}