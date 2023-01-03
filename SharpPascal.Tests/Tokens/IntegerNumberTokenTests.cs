/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using Xunit;
    
    using SharpPascal.Tokens;
    
    
    public class IntegerNumberTokenTests : ATokenTestsBase
    {
        [Fact]
        public void New_token_IntegerValue_contains_expected_value()
        {
            var token = new IntegerNumberToken(56, 1, 1);
            
            Assert.Equal(56, token.IntegerValue);
        }
        
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_INTEGER_NUMBER;
        protected override string ExpectedTokenStringRepresentation => "314";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new IntegerNumberToken(314, linePosition, line);
    }
}