/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using System;
    
    using Xunit;
    
    using SharpPascal.Tokens;
    
    
    public class IdentifierTokenTests : ATokenTestsBase
    {
        [Fact]
        public void New_token_StringValue_contains_expected_value()
        {
            var token = new IdentifierToken("identifier", 1, 1);
            
            Assert.Equal("identifier", token.StringValue);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void New_token_does_not_accept_null_or_empty_identifier_name(string identifier)
        {
            Assert.Throws<ArgumentException>(() => new IdentifierToken(identifier, 1, 1));
        }
        
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_IDENTIFIER;
        protected override string ExpectedTokenStringRepresentation => "bla";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new IdentifierToken("bla", linePosition, line);
    }
}