/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using SharpPascal.Tokens;
    
    
    public class BeginVariablesDeclarationTokenTests : ATokenTestsBase
    {
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_KEY_VAR;
        protected override string ExpectedTokenStringRepresentation => "VAR";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new BeginVariablesDeclarationToken(linePosition, line);
    }
}