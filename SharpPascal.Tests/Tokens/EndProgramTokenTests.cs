/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class EndProgramTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_PROG_END;
        protected override string ExpectedTokenStringRepresentation => ".";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new EndProgramToken(linePosition, line);
    }
}