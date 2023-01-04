/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using System;
    
    using Xunit;
    
    using SharpPascal.Tokens;
    
    
    public class SimpleTokenTests : ATokenTestsBase
    {
        [Fact]
        public void New_token_Code_contains_expected_value()
        {
            var token = CreateToken();
            
            Assert.Equal(ExpectedTokenCode, token.Code);
        }
        
        [Fact]
        public void New_token_StringValue_contains_expected_value()
        {
            var token = CreateToken();
            
            Assert.Equal(ExpectedTokenStringRepresentation, token.StringValue);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void New_token_does_not_accept_null_or_empty_identifier_name(string stringRepresentation)
        {
            Assert.Throws<ArgumentException>(() => new SimpleToken(ExpectedTokenCode, stringRepresentation, 1, 1));
        }
        
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_EQ_OP;
        protected override string ExpectedTokenStringRepresentation => "=";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new SimpleToken(ExpectedTokenCode, ExpectedTokenStringRepresentation, linePosition, line);
    }
}