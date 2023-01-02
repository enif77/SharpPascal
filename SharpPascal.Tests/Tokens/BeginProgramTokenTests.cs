/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class BeginProgramTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_KEY_PROGRAM;
        protected override string ExpectedTokenStringRepresentation => "PROGRAM";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new BeginProgramToken(linePosition, line);
    }
}