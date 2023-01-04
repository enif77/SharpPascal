/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using System;
    
    using Xunit;
    
    using SharpPascal.Tokens;
    
    
    public class StringLiteralTokenTests : ATokenTestsBase
    {
        [Fact]
        public void New_token_StringValue_contains_expected_value()
        {
            var token = new StringLiteralToken("string-value", 1, 1);
            
            Assert.Equal("string-value", token.StringValue);
        }
        
        [Fact]
        public void New_token_does_not_accept_null_value()
        {
            Assert.Throws<ArgumentException>(() => new IdentifierToken(null, 1, 1));
        }
        
        protected override TokenCode ExpectedTokenCode => TokenCode.TOK_IDENTIFIER;
        protected override string ExpectedTokenStringRepresentation => "bla";

        protected override IToken CreateToken(int linePosition = 1, int line = 1) =>
            new IdentifierToken("bla", linePosition, line);
    }
}