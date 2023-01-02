/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests.Tokens
{
    using System;
    
    using Xunit;


    public abstract class ATokenTestsBase
    {
        [Fact]
        public void New_token_contains_expected_token_code()
        {
            var token = CreateToken();
            
            Assert.Equal(ExpectedTokenCode, token.TokenCode);
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void New_token_does_not_accept_invalid_line_positions(int linePosition)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CreateToken(linePosition));
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void New_token_does_not_accept_invalid_line_number(int line)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CreateToken(1, line));
        }
        
        [Fact]
        public void New_token_contains_expected_line_position()
        {
            var token = CreateToken(3);
            
            Assert.Equal(3, token.LinePosition);
        }
        
        [Fact]
        public void New_token_contains_expected_line_number()
        {
            var token = CreateToken(1, 2);
            
            Assert.Equal(2, token.Line);
        }
        
        [Fact]
        public void ToString_returns_expected_token_string_representation()
        {
            var token = CreateToken();
            
            Assert.Equal(ExpectedTokenStringRepresentation, token.ToString());
        }
        
        
        protected abstract TokenCode ExpectedTokenCode { get; }
        protected abstract string ExpectedTokenStringRepresentation { get; }
        
        protected abstract IToken CreateToken(int linePosition = 1, int line = 1);
    }
}