/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using Xunit;
    
    using SharpPascal.Tokens;
    
    
    public class RealNumberTokenTests : ATokenTestsBase
    {
        [Fact]
        public void New_token_RealValue_contains_expected_value()
        {
            var token = new RealNumberToken(5.6, 1, 1);
            
            Assert.Equal(5.6, token.RealValue);
        }
        
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_REAL_NUMBER;
        protected override string ExpectedTokenStringRepresentation => "3.14";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new RealNumberToken(3.14, linePosition, line);
    }
}