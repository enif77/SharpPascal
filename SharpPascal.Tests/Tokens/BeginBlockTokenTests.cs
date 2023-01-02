/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class BeginBlockTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_KEY_BEGIN;
        protected override string ExpectedTokenStringRepresentation => "BEGIN";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new BeginBlockToken(linePosition, line);
    }
}