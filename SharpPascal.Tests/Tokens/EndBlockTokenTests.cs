/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class EndBlockTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_KEY_END;
        protected override string ExpectedTokenStringRepresentation => "END";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new EndBlockToken(linePosition, line);
    }
}